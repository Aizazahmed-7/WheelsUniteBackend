using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Events_Id",
                table: "Location");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Location",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Location_EventId",
                table: "Location",
                column: "EventId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Events_EventId",
                table: "Location",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Events_EventId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_EventId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Location");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Events_Id",
                table: "Location",
                column: "Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
