using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlParquetImporter.Domain.Interfaces.Repositories
{
    public interface IPriceForecastRepository
    {
        IEnumerable<PriceForecast> GetForecastAfterDate(DateTime dateTime, int limit);
        PriceForecast GetFirstForecast();
    }
}
