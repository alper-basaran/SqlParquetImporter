using Microsoft.EntityFrameworkCore;
using SqlParquetImporter.Domain;
using SqlParquetImporter.Domain.Core;
using System;

namespace SqlParquetImporter.Database
{
    public class PriceForecastContext : DbContext
    {
        public DbSet<PriceForecast> PriceForecasts { get; set; }
        public DbSet<ImportEvent> ImportEvents { get; set; }

        public PriceForecastContext(DbContextOptions<PriceForecastContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PriceForecast>(e =>
            {
                //Creating a composite key based on educated guess of likelihood uniqueness
                e.HasKey(p => new { p.ForecastDateTime, p.ForecastModel, p.Market, p.Product, p.CountryCode});
                //Marking all the properties as required
                e.Property(x => x.ForecastedDate).IsRequired();
                e.Property(x => x.ForecastModel).IsRequired();
                e.Property(x => x.ForecastedDate).IsRequired();
                e.Property(x => x.Category).IsRequired();
                e.Property(x => x.Market).IsRequired();
                e.Property(x => x.Product).IsRequired();
                e.Property(x => x.Price).IsRequired();
                e.Property(x => x.CountryCode).IsRequired();
            });

            modelBuilder.Entity<ImportEvent>(e => 
            {
                e.HasKey(x => x.EventId);
                e.Property(x => x.EventDate).ValueGeneratedOnAdd().HasDefaultValueSql("GETDATE()");
                e.Property(x => x.LastImportedDate).IsRequired();
            });

        }
    }
}
