using SqlParquetImporter.Domain;
using SqlParquetImporter.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Parquet.Data;
using System.Linq;
using System.Text.RegularExpressions;
using SqlParquetImported.Infra.Helpers;
using Parquet;
using System.IO;

namespace SqlParquetImported.Infra.Service
{
    public class ParquetWriterService : IParquetWriterService
    {
        //TODO: Implement this class from scratch by leveraging grouping queries in the DB engine
        //TODO: Unit & Integration Test
        //https://stackoverflow.com/questions/50933429/how-to-view-apache-parquet-file-in-windows
        //https://github.com/elastacloud/parquet-dotnet
        public void WriteData(IEnumerable<PriceForecast> data, string basePath)
        {
            var forecastsByCountry = data.GroupBy(f => f.CountryCode);
            foreach (var countryGroup in forecastsByCountry)
            {
                var country = countryGroup.Key;
                
                var forecastsByCategory = countryGroup.ToList().GroupBy(f => f.Category);
                foreach (var categoryGroup in forecastsByCategory)
                {
                    var category = categoryGroup.Key;
                    
                    var forecastsByYear = categoryGroup.ToList().GroupBy(f => f.ForecastedDate.Year);
                    foreach (var yearGroup in forecastsByYear)
                    {
                        var year = yearGroup.Key;

                        var forecastsByMonth = yearGroup.ToList().GroupBy(f => f.ForecastedDate.Month);
                        foreach (var monthGroup in forecastsByMonth)
                        {
                            var month = monthGroup.Key;
                            var forecasts = monthGroup.ToList();
                            var dirPath = $"{basePath}/Country={country}/Category={category}/Year={year}/Month={month}/";
                            var filePath = dirPath + "forecast.parquet";
                            
                            //TODO: automating schema generation using reflection and attributes
                            var columns = new DataColumn[] 
                            {
                                new DataColumn(ParquetSchemaHelper.ForecastDateField, forecasts.Select(f => f.ForecastDateTime.ToString()).ToArray()),
                                new DataColumn(ParquetSchemaHelper.ForecastModelField, forecasts.Select(f => f.ForecastModel).ToArray()),
                                new DataColumn(ParquetSchemaHelper.MarketField, forecasts.Select(f => f.Market).ToArray()),
                                new DataColumn(ParquetSchemaHelper.ProductField, forecasts.Select(f => f.Product).ToArray()),
                                new DataColumn(ParquetSchemaHelper.CountryField, forecasts.Select(f => f.CountryCode).ToArray()),
                                new DataColumn(ParquetSchemaHelper.ForecastedDateField, forecasts.Select(f => f.ForecastedDate.ToString()).ToArray()),
                                new DataColumn(ParquetSchemaHelper.CategoryField, forecasts.Select(f => f.Category).ToArray()),
                                new DataColumn(ParquetSchemaHelper.PriceField, forecasts.Select(f => f.Price).ToArray())
                            };
                            
                          
                            var schema = new Schema(columns.Select(c => c.Field).ToArray());
                            DirectoryInfo di = Directory.CreateDirectory(dirPath);//safe
                            using Stream fileStream = System.IO.File.OpenWrite(filePath);
                            using var parquetWriter = new ParquetWriter(schema, fileStream);
                            using (ParquetRowGroupWriter groupWriter = parquetWriter.CreateRowGroup())
                            {
                                foreach (var col in columns)
                                    groupWriter.WriteColumn(col);
                            }
                        }
                    }
                }
            }
            return;
        }
    }
}
