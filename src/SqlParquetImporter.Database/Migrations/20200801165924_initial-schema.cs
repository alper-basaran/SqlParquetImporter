using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SqlParquetImporter.Database.Migrations
{
    public partial class initialschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceForecasts",
                columns: table => new
                {
                    ForecastDateTime = table.Column<DateTime>(nullable: false),
                    ForecastModel = table.Column<string>(nullable: false),
                    Market = table.Column<string>(nullable: false),
                    Product = table.Column<string>(nullable: false),
                    CountryCode = table.Column<string>(nullable: false),
                    ForecastedDate = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceForecasts", x => new { x.ForecastDateTime, x.ForecastModel, x.Market, x.Product, x.CountryCode });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceForecasts");
        }
    }
}
