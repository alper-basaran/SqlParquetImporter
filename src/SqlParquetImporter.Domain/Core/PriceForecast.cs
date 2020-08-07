using System;

namespace SqlParquetImporter.Domain
{
    public class PriceForecast
    {
        public DateTime ForecastDateTime { get; set; }
        public string ForecastModel { get; set; }
        public DateTime ForecastedDate { get; set; }
        public string Category { get; set; }
        public string Market { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
        public string CountryCode { get; set; }
    }
}

/*
 * ForecastDateTime (DateTime): Tahminin yapıldığı tarih
 * ForecastModel (String): Tahmin oluşturan model ismi (max 15 karakter)
 * ForecastedDate (Date): Tahmin edilen tarih
 * Category (String): Ürün kategorisi (Elektronik, Telekominikasyon, Mobilya, Ofis,
 * MutfakEşyaları, Bahçe, Kırtasiye, Otomobil, Yiyecek&İçecek, Aydınlatma)
 * Market (String): Ürünün satıldığı mağaza ismi (max 20 karakter)
 * Product (String): Max 50 karakterli ürün marka ve modeli (iPhone 11 Pro, Asus
 * X571GD-BQ524, ..)
 * Price (Double): Ürünün fiyat tahmini
 * CountryCode (String): 2 karakterli ülke kodu
 */
