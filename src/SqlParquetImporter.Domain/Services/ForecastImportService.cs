using SqlParquetImporter.Domain.Interfaces.Providers;
using SqlParquetImporter.Domain.Interfaces.Repositories;
using SqlParquetImporter.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlParquetImporter.Domain.Services
{
    public class ForecastImportService : IForecastImportService
    {
        private readonly IPriceForecastRepository _priceForecastRepository;
        private readonly IImportEventRepository _eventRepository;
        private readonly IParquetWriterService _parquetWriterService;
        private readonly IConfigProvider _configProvider;
        public ForecastImportService(IPriceForecastRepository priceForecastRepository, IImportEventRepository eventRepository, IConfigProvider configProvider,  IParquetWriterService parquetWriterService)
        {
            _priceForecastRepository = priceForecastRepository;
            _eventRepository = eventRepository;
            _parquetWriterService = parquetWriterService;
            _configProvider = configProvider;
        }
        public bool ImportMissingForecasts(int paginationLimit)
        {
            DateTime startTimeStamp = DateTime.MinValue;
            var lastEvent = _eventRepository.GetLatestEvent();
         
            if (lastEvent != null)
                startTimeStamp = lastEvent.LastImportedDate;

            var forecasts = _priceForecastRepository.GetForecastAfterDate(startTimeStamp, paginationLimit);
            var lastImported = forecasts.ToArray()[forecasts.Count() - 1];//Redundant Enumeration!

            _eventRepository.AddImportEvent(new Core.ImportEvent { LastImportedDate = lastImported.ForecastDateTime });

            _parquetWriterService.WriteData(forecasts, _configProvider.BaseFilePath);//Should throw exception                 

            //Signals the end of operation by checking if there's more data to be imported
            //This solution is far from optimal, needs to be improved
            return forecasts.Count() < paginationLimit;
        }
    }
}
