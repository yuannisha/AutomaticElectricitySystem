using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yuannisha.AutomaticElectricitySystem.Migrations
{
    /// <inheritdoc />
    public partial class _202436UpdateDadabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "GIISTRooms");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "GIISTRooms");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "GIISTRooms");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "GIISTRooms");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "GIISTRooms");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "GIISTRooms",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "GIISTRooms",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "GIISTRooms",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "GIISTRooms",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "GIISTRooms");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "GIISTRooms");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "GIISTRooms");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "GIISTRooms",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "GIISTRooms",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "GIISTRooms",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "GIISTRooms",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "GIISTRooms",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "GIISTRooms",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");
        }
    }
}
