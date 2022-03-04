using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartTime = table.Column<string>(type: "TEXT", nullable: false),
                    StopTime = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<double>(type: "REAL", nullable: false),
                    IsCreated = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsComplete = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    LicenseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(type: "TEXT", nullable: false),
                    LName = table.Column<string>(type: "TEXT", nullable: false),
                    dob = table.Column<DateTime>(type: "TEXT", nullable: false),
                    YearsHeld = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.LicenseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Make = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Range = table.Column<double>(type: "REAL", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerUnit = table.Column<double>(type: "REAL", nullable: false),
                    isLocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    RangeLeft = table.Column<double>(type: "REAL", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "Cost", "IsComplete", "IsCreated", "StartTime", "StopTime" },
                values: new object[] { 1, 88.0, true, false, "25/02/2022 05:01", null });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "Cost", "IsComplete", "IsCreated", "StartTime", "StopTime" },
                values: new object[] { 2, 98.0, true, false, "25/02/2022 05:01", null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 1, "Description 1", "Category 1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 2, "Description 2", "Category 2" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[] { 3, "Description 3", "Category 3" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "Password" },
                values: new object[] { 1, "Email: 1", "Description 1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "Password" },
                values: new object[] { 2, "Email: 2", "Description 2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "EmailAddress", "Password" },
                values: new object[] { 3, "Email: 3", "Description 3" });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Make", "Model", "PricePerUnit", "Range", "RangeLeft", "isLocked" },
                values: new object[] { 1, false, 1, "Tesla", "Model X", 7.0, 250.0, 100.0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Make", "Model", "PricePerUnit", "Range", "RangeLeft", "isLocked" },
                values: new object[] { 2, false, 2, "Tesla", "Model S", 7.0, 200.0, 100.0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Make", "Model", "PricePerUnit", "Range", "RangeLeft", "isLocked" },
                values: new object[] { 3, false, 3, "Porsche", "Taycan", 7.0, 270.0, 100.0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Make", "Model", "PricePerUnit", "Range", "RangeLeft", "isLocked" },
                values: new object[] { 4, false, 1, "Nissan", "Leaf", 7.0, 150.0, 100.0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Make", "Model", "PricePerUnit", "Range", "RangeLeft", "isLocked" },
                values: new object[] { 5, false, 2, "Honda", "Up!", 7.0, 220.0, 100.0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Make", "Model", "PricePerUnit", "Range", "RangeLeft", "isLocked" },
                values: new object[] { 6, false, 3, "Toyota", "GT", 7.0, 200.0, 100.0, true });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
