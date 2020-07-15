using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CarsIsland.Catalog.Infrastructure.Migrations
{
    public partial class addcatalogdbinitialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    PricePerDay = table.Column<decimal>(nullable: false),
                    AvailableForRent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "AvailableForRent", "Brand", "Model", "PricePerDay" },
                values: new object[,]
                {
                    { new Guid("2605c956-1ab4-4ce9-ad90-559978d754dd"), true, "BMW", "320", 200m },
                    { new Guid("8907d3d0-d16f-49e1-8ae6-570cd3d20c84"), true, "Audi", "A1", 120m },
                    { new Guid("9d121cd4-28a9-4ccf-94fd-d04041c81988"), true, "Mercedes", "E200", 250m },
                    { new Guid("e0ea016f-9c7a-4bc9-86e1-8fdae0b334f0"), true, "Ford", "Focus", 90m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
