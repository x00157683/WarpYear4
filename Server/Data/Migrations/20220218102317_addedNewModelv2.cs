using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class addedNewModelv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2022, 2, 18, 10, 23, 16, 572, DateTimeKind.Local).AddTicks(8404));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2022, 2, 18, 10, 23, 16, 572, DateTimeKind.Local).AddTicks(8500));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2022, 2, 17, 21, 12, 11, 350, DateTimeKind.Local).AddTicks(467));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2022, 2, 17, 21, 12, 11, 350, DateTimeKind.Local).AddTicks(535));
        }
    }
}
