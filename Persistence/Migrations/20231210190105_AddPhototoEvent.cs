using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPhototoEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_PhotoId",
                table: "Events",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Photos_PhotoId",
                table: "Events",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Photos_PhotoId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_PhotoId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Events");
        }
    }
}
