using System;
using System.Collections.Generic;
using System.Text;

namespace SqlParquetImporter.Domain.Core
{
    public class ImportEvent
    {
        public int EventId { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime LastImportedDate { get; set; }
    }
}
