using Microsoft.EntityFrameworkCore;
using SqlParquetImporter.Database;
using SqlParquetImporter.Domain;
using SqlParquetImporter.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlParquetImported.Infra
{
    public class PriceForecastRepository : IPriceForecastRepository
    {
        private readonly PriceForecastContext _context;
        public PriceForecastRepository(PriceForecastContext context)
        {
            _context = context;

        }
        public IQueryable<PriceForecast> GetForecastAfterDate(DateTime dateTime, int limit = 100)
        {
            var forecasts = _context.PriceForecasts
                .AsNoTracking()
                .Where(p => p.ForecastDateTime > dateTime)
                .Take(limit);
            
            return forecasts;
        }
        public PriceForecast GetFirstForecast()
        {
            return _context.PriceForecasts.FirstOrDefault();
        }
    }
}
