using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlParquetImporter.Domain.Interfaces.Services
{
    public interface IParquetWriterService
    {
        void WriteData(IEnumerable<PriceForecast> data, string basePath);
    }
}
