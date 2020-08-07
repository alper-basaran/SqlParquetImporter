using System;
using System.Collections.Generic;
using System.Text;

namespace SqlParquetImporter.Domain.Interfaces.Services
{
    public interface IForecastImportService
    {
        bool ImportMissingForecasts(int paginationLimit);
    }
}
