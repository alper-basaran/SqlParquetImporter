using Microsoft.EntityFrameworkCore;
using SqlParquetImporter.Database;
using SqlParquetImporter.Domain.Core;
using SqlParquetImporter.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlParquetImported.Infra.Repositories
{
    public class ImportEventRepository : IImportEventRepository
    {
        private readonly PriceForecastContext _context;
        private readonly DbSet<ImportEvent> _events;
        public ImportEventRepository(PriceForecastContext context)
        {
            _context = context;
            _events = context.ImportEvents;
        }      
        public void AddImportEvent(ImportEvent importEvent)
        {
            _events.Add(importEvent);
            _context.SaveChanges();
        }

        public IEnumerable<ImportEvent> GetAllEvents(int limit)
        {
            return _events.ToArray();
        }

        public ImportEvent GetLatestEvent()
        {
            var latestEvent = _events
                .OrderByDescending(e => e.EventDate)
                .FirstOrDefault();
            return latestEvent;
        }
    }
}
