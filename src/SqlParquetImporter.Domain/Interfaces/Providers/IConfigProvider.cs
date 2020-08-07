using System;
using System.Collections.Generic;
using System.Text;

namespace SqlParquetImporter.Domain.Interfaces.Providers
{
    public interface IConfigProvider
    {
        public string BaseFilePath { get; }
    }
}
