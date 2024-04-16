using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yuannisha.AutomaticElectricitySystem.Migrations
{
    /// <inheritdoc />
    public partial class _20240225AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GIISTBookingInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, comment: "学号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "学生名字")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentClass = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "学生班级")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelephoneNumber = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false, comment: "电话号码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsingClassroom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "使用教室")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsingPurpose = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "使用用途")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookingTimespan = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, comment: "预约时间段")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIISTBookingInformation", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GIISTBookingLimited",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, comment: "学号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "学生姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "预约日期")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BookedHours = table.Column<int>(type: "int", nullable: false, comment: "已预约小时数"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIISTBookingLimited", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GIISTBuildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "楼栋名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIISTBuildings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GIISTRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BuildingId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "楼栋标识", collation: "ascii_general_ci"),
                    Floor = table.Column<int>(type: "int", nullable: false, comment: "楼层标识"),
                    No = table.Column<string>(type: "longtext", nullable: true, comment: "教室号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsingOrNot = table.Column<int>(type: "int", nullable: false, comment: "是否在用"),
                    RoomType = table.Column<int>(type: "int", nullable: false, comment: "教室类型"),
                    ControlType = table.Column<int>(type: "int", nullable: false, comment: "控制类型"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIISTRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GIISTRooms_GIISTBuildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "GIISTBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GIISTPowerSwitchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoomId = table.Column<Guid>(type: "char(36)", nullable: false, comment: "教室", collation: "ascii_general_ci"),
                    SerialNumber = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false, comment: "设备序列号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ControlledMachineName = table.Column<string>(type: "longtext", nullable: true, comment: "控制器械名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsOnline = table.Column<int>(type: "int", nullable: false, comment: "是否在线"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "开关开合闸状态"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    DeletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIISTPowerSwitchs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GIISTPowerSwitchs_GIISTRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "GIISTRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GIISTPowerSwitchs_RoomId",
                table: "GIISTPowerSwitchs",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_GIISTRooms_BuildingId",
                table: "GIISTRooms",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GIISTBookingInformation");

            migrationBuilder.DropTable(
                name: "GIISTBookingLimited");

            migrationBuilder.DropTable(
                name: "GIISTPowerSwitchs");

            migrationBuilder.DropTable(
                name: "GIISTRooms");

            migrationBuilder.DropTable(
                name: "GIISTBuildings");
        }
    }
}
