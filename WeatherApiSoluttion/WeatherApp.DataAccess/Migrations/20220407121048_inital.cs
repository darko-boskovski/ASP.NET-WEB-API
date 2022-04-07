using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.DataAccess.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Long = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeekDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MkName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EngName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false),
                    TemperatureC = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    TemperatureMinC = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    TemperatureMaxC = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    WeatherDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Precipitation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    WindSpeed = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    WeekDayId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherData_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherData_WeekDays_WeekDayId",
                        column: x => x.WeekDayId,
                        principalTable: "WeekDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Lat", "Long", "Name" },
                values: new object[] { 1, "42.002", "21.5122", "Skopje" });

            migrationBuilder.InsertData(
                table: "WeekDays",
                columns: new[] { "Id", "EngName", "MkName" },
                values: new object[,]
                {
                    { 1, "Monday", "Понеделник" },
                    { 2, "Tuesday", "Вторник" },
                    { 3, "Wednesday", "Среда" },
                    { 4, "Thursday", "Четврток" },
                    { 5, "Friday", "Петок" },
                    { 6, "Saturday", "Сабота" },
                    { 7, "Sunday", "Недела" }
                });

            migrationBuilder.InsertData(
                table: "WeatherData",
                columns: new[] { "Id", "CityId", "Date", "Icon", "Language", "Precipitation", "TemperatureC", "TemperatureMaxC", "TemperatureMinC", "WeatherDescription", "WeekDayId", "WindSpeed" },
                values: new object[] { 1, 1, new DateTime(2022, 4, 7, 14, 10, 47, 721, DateTimeKind.Local).AddTicks(6104), "09d", "eng", "48", 13, 13, 5, "Light Rain", 1, "5.93m/s" });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_CityId",
                table: "WeatherData",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_WeekDayId",
                table: "WeatherData",
                column: "WeekDayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherData");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "WeekDays");
        }
    }
}
