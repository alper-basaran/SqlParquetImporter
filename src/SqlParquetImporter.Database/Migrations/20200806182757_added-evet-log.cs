using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlParquetImporter.Database.Migrations
{
    public partial class addedevetlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    LastImportedForecastDateTime = table.Column<DateTime>(nullable: true),
                    LastImportedForecastModel = table.Column<string>(nullable: true),
                    LastImportedMarket = table.Column<string>(nullable: true),
                    LastImportedProduct = table.Column<string>(nullable: true),
                    LastImportedCountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportEvents", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_ImportEvents_PriceForecasts_LastImportedForecastDateTime_LastImportedForecastModel_LastImportedMarket_LastImportedProduct_La~",
                        columns: x => new { x.LastImportedForecastDateTime, x.LastImportedForecastModel, x.LastImportedMarket, x.LastImportedProduct, x.LastImportedCountryCode },
                        principalTable: "PriceForecasts",
                        principalColumns: new[] { "ForecastDateTime", "ForecastModel", "Market", "Product", "CountryCode" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImportEvents_LastImportedForecastDateTime_LastImportedForecastModel_LastImportedMarket_LastImportedProduct_LastImportedCount~",
                table: "ImportEvents",
                columns: new[] { "LastImportedForecastDateTime", "LastImportedForecastModel", "LastImportedMarket", "LastImportedProduct", "LastImportedCountryCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportEvents");
        }
    }
}
