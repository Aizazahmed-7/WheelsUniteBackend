using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedPhotoToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "Photos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CarId",
                table: "Photos",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Cars_CarId",
                table: "Photos",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Cars_CarId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CarId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Photos");
        }
    }
}
