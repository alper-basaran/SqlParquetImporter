using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlParquetImporter.Database.Migrations
{
    public partial class changedeventschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportEvents_PriceForecasts_LastImportedForecastDateTime_LastImportedForecastModel_LastImportedMarket_LastImportedProduct_La~",
                table: "ImportEvents");

            migrationBuilder.DropIndex(
                name: "IX_ImportEvents_LastImportedForecastDateTime_LastImportedForecastModel_LastImportedMarket_LastImportedProduct_LastImportedCount~",
                table: "ImportEvents");

            migrationBuilder.DropColumn(
                name: "LastImportedCountryCode",
                table: "ImportEvents");

            migrationBuilder.DropColumn(
                name: "LastImportedForecastDateTime",
                table: "ImportEvents");

            migrationBuilder.DropColumn(
                name: "LastImportedForecastModel",
                table: "ImportEvents");

            migrationBuilder.DropColumn(
                name: "LastImportedMarket",
                table: "ImportEvents");

            migrationBuilder.DropColumn(
                name: "LastImportedProduct",
                table: "ImportEvents");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastImportedDate",
                table: "ImportEvents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastImportedDate",
                table: "ImportEvents");

            migrationBuilder.AddColumn<string>(
                name: "LastImportedCountryCode",
                table: "ImportEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastImportedForecastDateTime",
                table: "ImportEvents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastImportedForecastModel",
                table: "ImportEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastImportedMarket",
                table: "ImportEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastImportedProduct",
                table: "ImportEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImportEvents_LastImportedForecastDateTime_LastImportedForecastModel_LastImportedMarket_LastImportedProduct_LastImportedCount~",
                table: "ImportEvents",
                columns: new[] { "LastImportedForecastDateTime", "LastImportedForecastModel", "LastImportedMarket", "LastImportedProduct", "LastImportedCountryCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_ImportEvents_PriceForecasts_LastImportedForecastDateTime_LastImportedForecastModel_LastImportedMarket_LastImportedProduct_La~",
                table: "ImportEvents",
                columns: new[] { "LastImportedForecastDateTime", "LastImportedForecastModel", "LastImportedMarket", "LastImportedProduct", "LastImportedCountryCode" },
                principalTable: "PriceForecasts",
                principalColumns: new[] { "ForecastDateTime", "ForecastModel", "Market", "Product", "CountryCode" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
