using System;
using System.Collections.Generic;
using System.Text;
using Parquet.Data;

namespace SqlParquetImported.Infra.Helpers
{
    public static class ParquetSchemaHelper
    {

        public static readonly DataField<string> ForecastDateField = new DataField<string>("ForecastDate");
        public static readonly DataField<string> ForecastModelField = new DataField<string>("ForecastModel");
        public static readonly DataField<string> MarketField = new DataField<string>("Market");
        public static readonly DataField<string> ProductField = new DataField<string>("Product");
        public static readonly DataField<string> CountryField = new DataField<string>("CountryCode");
        public static readonly DataField<string> ForecastedDateField = new DataField<string>("ForecastedDate");
        public static readonly DataField<string> CategoryField = new DataField<string>("Category");
        public static readonly DataField<double> PriceField = new DataField<double>("Price");

    }
}
