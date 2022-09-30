using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pholium.Data.Migrations
{
    public partial class UpdatingDefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("b25fca61-2f00-472a-bcb0-b87e7080b255"),
                column: "DateCreated",
                value: new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("b25fca61-2f00-472a-bcb0-b87e7080b255"),
                column: "DateCreated",
                value: null);
        }
    }
}
