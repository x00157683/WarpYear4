using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    ConfirmPassword = table.Column<string>(type: "TEXT", nullable: true),
                    Dob = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Balance = table.Column<double>(type: "REAL", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PriceRate = table.Column<double>(type: "REAL", nullable: false)
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Reviews = table.Column<int>(type: "INTEGER", nullable: false),
                    Lat = table.Column<double>(type: "REAL", nullable: false),
                    Lng = table.Column<double>(type: "REAL", nullable: false),
                    RangeLeft = table.Column<double>(type: "REAL", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    OverallRating = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<string>(type: "TEXT", nullable: true),
                    StopTime = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Cost = table.Column<double>(type: "REAL", nullable: false),
                    IsCreated = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsComplete = table.Column<bool>(type: "INTEGER", nullable: false),
                    CarId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "PriceRate" },
                values: new object[] { 1, "Insurance Covered and up to 10 hours free driving", "Standard Tier", 0.0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "PriceRate" },
                values: new object[] { 2, "Insurance Covered and up to 20 hours free driving", "Gold Tier", 0.0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name", "PriceRate" },
                values: new object[] { 3, "Insurance Covered and up to 30 hours free driving", "Premium Tier", 0.0 });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Lat", "Lng", "Location", "Make", "Model", "OverallRating", "PricePerUnit", "Range", "RangeLeft", "Rating", "Reviews", "isLocked" },
                values: new object[] { 1, false, 1, 53.15014, -6.5661550000000002, null, "Tesla", "Model X", 0, 7.0, 250.0, 100.0, 0, 0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Lat", "Lng", "Location", "Make", "Model", "OverallRating", "PricePerUnit", "Range", "RangeLeft", "Rating", "Reviews", "isLocked" },
                values: new object[] { 2, false, 2, 53.250140000000002, -6.1661549999999998, null, "Tesla", "Model S", 0, 7.0, 200.0, 100.0, 0, 0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Lat", "Lng", "Location", "Make", "Model", "OverallRating", "PricePerUnit", "Range", "RangeLeft", "Rating", "Reviews", "isLocked" },
                values: new object[] { 3, false, 3, 53.15014, -6.6661549999999998, null, "Porsche", "Taycan", 0, 7.0, 270.0, 100.0, 0, 0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Lat", "Lng", "Location", "Make", "Model", "OverallRating", "PricePerUnit", "Range", "RangeLeft", "Rating", "Reviews", "isLocked" },
                values: new object[] { 4, false, 1, 53.15014, -6.6661549999999998, null, "Nissan", "Leaf", 0, 7.0, 150.0, 100.0, 0, 0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Lat", "Lng", "Location", "Make", "Model", "OverallRating", "PricePerUnit", "Range", "RangeLeft", "Rating", "Reviews", "isLocked" },
                values: new object[] { 5, false, 2, 53.750140000000002, -6.866155, null, "Honda", "Up!", 0, 7.0, 220.0, 100.0, 0, 0, true });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "Active", "CategoryId", "Lat", "Lng", "Location", "Make", "Model", "OverallRating", "PricePerUnit", "Range", "RangeLeft", "Rating", "Reviews", "isLocked" },
                values: new object[] { 6, false, 3, 53.450139999999998, -6.366155, null, "Toyota", "GT", 0, 7.0, 200.0, 100.0, 0, 0, true });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarId",
                table: "Bookings",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
