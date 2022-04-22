using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.DataAccess.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherData",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 4, 12, 17, 10, 33, 170, DateTimeKind.Local).AddTicks(4626));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WeatherData",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 4, 7, 14, 10, 47, 721, DateTimeKind.Local).AddTicks(6104));
        }
    }
}
