using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pholium.Data.Migrations
{
    public partial class Password : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 22, 36, 23, 541, DateTimeKind.Local).AddTicks(6221),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 3, 22, 30, 35, 621, DateTimeKind.Local).AddTicks(6463));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "TestePholium");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 3, 22, 30, 35, 621, DateTimeKind.Local).AddTicks(6463),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 22, 36, 23, 541, DateTimeKind.Local).AddTicks(6221));
        }
    }
}
