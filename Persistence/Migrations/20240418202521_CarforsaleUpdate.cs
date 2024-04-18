using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CarforsaleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "CarsForSale",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Engine",
                table: "CarsForSale",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingBid",
                table: "CarsForSale",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "CarsForSale",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                table: "CarsForSale");

            migrationBuilder.DropColumn(
                name: "Engine",
                table: "CarsForSale");

            migrationBuilder.DropColumn(
                name: "StartingBid",
                table: "CarsForSale");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "CarsForSale");
        }
    }
}
