using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pholium.Data.Migrations
{
    public partial class InsertingDefaultUserinUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Name" },
                values: new object[] { new Guid("b25fca61-2f00-472a-bcb0-b87e7080b255"), "userdefault@template.com", "User Default" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: new Guid("b25fca61-2f00-472a-bcb0-b87e7080b255"));
        }
    }
}
