using SqlParquetImporter.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlParquetImporter.Domain.Interfaces.Repositories
{
    public interface IImportEventRepository
    {
        void AddImportEvent(ImportEvent importEvent);
        ImportEvent GetLatestEvent();
        IEnumerable<ImportEvent> GetAllEvents(int limit);
    }
}
