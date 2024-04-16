using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yuannisha.AutomaticElectricitySystem.Migrations
{
    /// <inheritdoc />
    public partial class _2024311Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsingOrNot",
                table: "GIISTRooms");

            migrationBuilder.AlterColumn<int>(
                name: "RoomType",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                comment: "��������",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "教室类型");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "GIISTRooms",
                type: "longtext",
                nullable: true,
                comment: "���Һ�",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "教室号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Floor",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                comment: "¥���ʶ",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "楼层标识");

            migrationBuilder.AlterColumn<int>(
                name: "ControlType",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                comment: "��������",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "控制类型");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingId",
                table: "GIISTRooms",
                type: "char(36)",
                nullable: false,
                comment: "¥����ʶ",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "楼栋标识")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "BuildingName",
                table: "GIISTRooms",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "IsUsingOrNot",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "�Ƿ�����");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "GIISTPowerSwitchs",
                type: "int",
                nullable: false,
                comment: "���ؿ���բ״̬",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "开关开合闸状态");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "GIISTPowerSwitchs",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                comment: "�豸���к�",
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14,
                oldComment: "设备序列号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "GIISTPowerSwitchs",
                type: "char(36)",
                nullable: false,
                comment: "����",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "教室")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IsOnline",
                table: "GIISTPowerSwitchs",
                type: "int",
                nullable: false,
                comment: "�Ƿ�����",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "是否在线");

            migrationBuilder.AlterColumn<string>(
                name: "ControlledMachineName",
                table: "GIISTPowerSwitchs",
                type: "longtext",
                nullable: true,
                comment: "������е��",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "控制器械名")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GIISTBuildings",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "¥������",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "楼栋名称")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                table: "GIISTBuildings",
                type: "int",
                nullable: false,
                comment: "����",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "排序");

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "GIISTBookingLimited",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "ѧ������",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "学生姓名")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "GIISTBookingLimited",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                comment: "ѧ��",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldComment: "学号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "GIISTBookingLimited",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "ԤԼ����",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "预约日期")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "BookedHours",
                table: "GIISTBookingLimited",
                type: "int",
                nullable: false,
                comment: "��ԤԼСʱ��",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "已预约小时数");

            migrationBuilder.AlterColumn<string>(
                name: "UsingPurpose",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "ʹ����;",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "使用用途")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UsingClassroom",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "ʹ�ý���",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "使用教室")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TelephoneNumber",
                table: "GIISTBookingInformation",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                comment: "�绰����",
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11,
                oldComment: "电话号码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "ѧ������",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "学生名字")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "GIISTBookingInformation",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                comment: "ѧ��",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldComment: "学号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentClass",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "ѧ���༶",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "学生班级")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BookingTimespan",
                table: "GIISTBookingInformation",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                comment: "ԤԼʱ���",
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldComment: "预约时间段")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingName",
                table: "GIISTRooms");

            migrationBuilder.DropColumn(
                name: "IsUsingOrNot",
                table: "GIISTRooms");

            migrationBuilder.AlterColumn<int>(
                name: "RoomType",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                comment: "教室类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "��������");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                table: "GIISTRooms",
                type: "longtext",
                nullable: true,
                comment: "教室号",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "���Һ�")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Floor",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                comment: "楼层标识",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "¥���ʶ");

            migrationBuilder.AlterColumn<int>(
                name: "ControlType",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                comment: "控制类型",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "��������");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingId",
                table: "GIISTRooms",
                type: "char(36)",
                nullable: false,
                comment: "楼栋标识",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "¥����ʶ")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "UsingOrNot",
                table: "GIISTRooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "是否在用");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "GIISTPowerSwitchs",
                type: "int",
                nullable: false,
                comment: "开关开合闸状态",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "���ؿ���բ״̬");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "GIISTPowerSwitchs",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                comment: "设备序列号",
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldMaxLength: 14,
                oldComment: "�豸���к�")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "GIISTPowerSwitchs",
                type: "char(36)",
                nullable: false,
                comment: "教室",
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldComment: "����")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IsOnline",
                table: "GIISTPowerSwitchs",
                type: "int",
                nullable: false,
                comment: "是否在线",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "�Ƿ�����");

            migrationBuilder.AlterColumn<string>(
                name: "ControlledMachineName",
                table: "GIISTPowerSwitchs",
                type: "longtext",
                nullable: true,
                comment: "控制器械名",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "������е��")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "GIISTBuildings",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "楼栋名称",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "¥������")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                table: "GIISTBuildings",
                type: "int",
                nullable: false,
                comment: "排序",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "����");

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "GIISTBookingLimited",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "学生姓名",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "ѧ������")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "GIISTBookingLimited",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                comment: "学号",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldComment: "ѧ��")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "GIISTBookingLimited",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "预约日期",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "ԤԼ����")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "BookedHours",
                table: "GIISTBookingLimited",
                type: "int",
                nullable: false,
                comment: "已预约小时数",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "��ԤԼСʱ��");

            migrationBuilder.AlterColumn<string>(
                name: "UsingPurpose",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "使用用途",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "ʹ����;")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UsingClassroom",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "使用教室",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "ʹ�ý���")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TelephoneNumber",
                table: "GIISTBookingInformation",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                comment: "电话号码",
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldMaxLength: 11,
                oldComment: "�绰����")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "学生名字",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "ѧ������")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "GIISTBookingInformation",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                comment: "学号",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldComment: "ѧ��")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "StudentClass",
                table: "GIISTBookingInformation",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "学生班级",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "ѧ���༶")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BookingTimespan",
                table: "GIISTBookingInformation",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                comment: "预约时间段",
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80,
                oldComment: "ԤԼʱ���")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
