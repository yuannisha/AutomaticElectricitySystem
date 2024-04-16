using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yuannisha.AutomaticElectricitySystem.Migrations
{
    /// <inheritdoc />
    public partial class _2024311Updatesecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingName",
                table: "GIISTRooms");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuildingName",
                table: "GIISTRooms",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
