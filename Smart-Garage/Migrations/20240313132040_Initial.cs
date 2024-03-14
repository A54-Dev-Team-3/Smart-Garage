using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smart_Garage.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationYear = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MechanicVisit",
                columns: table => new
                {
                    MechanicId = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicVisit", x => new { x.MechanicId, x.VisitId });
                    table.ForeignKey(
                        name: "FK_MechanicVisit_Mechanics_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "Mechanics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MechanicVisit_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartVisit",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartVisit", x => new { x.PartId, x.VisitId });
                    table.ForeignKey(
                        name: "FK_PartVisit_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartVisit_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceVisit",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceVisit", x => new { x.ServiceId, x.VisitId });
                    table.ForeignKey(
                        name: "FK_ServiceVisit_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceVisit_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Mercedes-Benz" },
                    { 2, false, "BMW" },
                    { 3, false, "Skoda" }
                });

            migrationBuilder.InsertData(
                table: "Mechanics",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Jackson Brooks" },
                    { 2, false, "Liam Thompson" },
                    { 3, false, "Mason Harper" }
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "Id", "IsDeleted", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, false, "Timing Belt SKF VKMA 91401", 15.199999999999999 },
                    { 2, false, "Water Pump Meyle 12-34 500 6646", 228.0 },
                    { 3, false, "Oil Mercedes-Benz 5W-30, 229.51, 5L.", 84.0 },
                    { 4, false, "Spark Plug NGK IZFR6H11", 95.0 },
                    { 5, false, "Gasket Elring 538.980", 90.0 },
                    { 6, false, "Brakepads FEBI Bilstein 16666", 22.120000000000001 },
                    { 7, false, "Coolant HEPU P999 G12 2L.", 311.88999999999999 },
                    { 8, false, "Shock Absorber RIDEX 854S0086", 170.84 },
                    { 9, false, "Oxygen Sensor Bosch 0258005726", 16.0 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { 1, false, "Change Timing Belt", 25.0 },
                    { 2, false, "Change Water Pump", 100.0 },
                    { 3, false, "Change Oil", 20.0 },
                    { 4, false, "Change Spark Plugs", 15.0 },
                    { 5, false, "Change Gasket", 125.0 },
                    { 6, false, "Change Brakepads", 30.0 },
                    { 7, false, "Change Coolant", 5.0 },
                    { 8, false, "Change Shock Absorber", 50.0 },
                    { 9, false, "Change Oxygen Sensor", 10.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "IsDeleted", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "admin", true, false, "admin", new byte[] { 108, 213, 86, 105, 75, 115, 157, 237, 105, 45, 250, 169, 143, 181, 192, 10, 54, 161, 127, 60, 199, 176, 32, 27, 27, 95, 231, 226, 156, 47, 107, 60, 102, 154, 94, 59, 168, 135, 119, 37, 121, 19, 11, 16, 181, 129, 217, 231, 151, 63, 110, 234, 10, 35, 194, 163, 77, 239, 221, 172, 123, 244, 11, 200 }, new byte[] { 139, 65, 228, 102, 109, 255, 168, 153, 67, 198, 232, 194, 92, 221, 174, 203, 168, 149, 9, 1, 51, 126, 157, 223, 125, 118, 18, 205, 10, 111, 23, 108, 132, 50, 76, 215, 215, 40, 215, 28, 229, 56, 135, 116, 54, 251, 17, 17, 219, 111, 60, 211, 131, 65, 135, 182, 225, 72, 254, 3, 30, 176, 210, 59, 46, 215, 133, 2, 111, 76, 166, 35, 246, 135, 157, 2, 38, 142, 16, 192, 101, 207, 119, 43, 175, 89, 169, 24, 0, 111, 179, 198, 26, 188, 67, 189, 24, 108, 135, 4, 193, 240, 66, 124, 155, 237, 99, 206, 212, 148, 240, 116, 85, 47, 190, 145, 60, 92, 26, 223, 121, 113, 3, 9, 2, 106, 1, 87 }, "1234567890", "admin" },
                    { 2, "george@gmail.com", "George", false, false, "Smith", new byte[] { 147, 90, 57, 254, 14, 120, 124, 155, 213, 146, 80, 211, 44, 188, 77, 101, 166, 186, 15, 95, 47, 176, 0, 141, 173, 108, 234, 166, 197, 49, 7, 165, 72, 61, 229, 134, 182, 50, 47, 74, 53, 9, 132, 52, 1, 35, 132, 177, 125, 69, 75, 28, 251, 4, 141, 81, 250, 123, 193, 121, 186, 155, 127, 14 }, new byte[] { 179, 47, 16, 165, 120, 253, 5, 199, 23, 248, 171, 211, 231, 180, 39, 170, 93, 221, 112, 244, 169, 83, 91, 240, 88, 138, 56, 81, 254, 200, 160, 73, 179, 63, 183, 81, 204, 132, 14, 91, 213, 124, 54, 189, 187, 93, 232, 108, 211, 72, 244, 69, 219, 8, 244, 36, 40, 222, 17, 251, 40, 172, 83, 199, 190, 3, 166, 77, 21, 33, 82, 56, 57, 56, 239, 179, 159, 176, 39, 224, 186, 55, 231, 110, 3, 239, 149, 141, 49, 11, 158, 188, 34, 66, 92, 186, 245, 25, 158, 80, 229, 212, 6, 187, 160, 138, 45, 210, 90, 107, 251, 128, 208, 135, 186, 118, 201, 233, 58, 65, 219, 86, 219, 222, 80, 75, 74, 223 }, "1234567891", "george123" },
                    { 3, "alexander@gmail.com", "Alexander", false, false, "Parker", new byte[] { 28, 234, 244, 15, 223, 125, 252, 251, 98, 253, 11, 227, 255, 24, 99, 217, 242, 5, 136, 216, 122, 174, 199, 79, 201, 145, 120, 251, 197, 154, 22, 194, 22, 209, 190, 98, 170, 252, 121, 162, 173, 57, 141, 27, 185, 27, 209, 47, 77, 51, 12, 60, 244, 185, 188, 194, 173, 163, 30, 20, 65, 216, 205, 237 }, new byte[] { 20, 125, 163, 75, 6, 111, 69, 248, 218, 191, 162, 39, 224, 148, 122, 244, 93, 93, 249, 124, 58, 66, 64, 249, 200, 242, 206, 166, 214, 123, 130, 103, 37, 150, 148, 91, 114, 207, 192, 36, 140, 1, 59, 93, 27, 0, 132, 143, 95, 103, 149, 77, 121, 214, 170, 52, 31, 97, 214, 55, 19, 133, 121, 193, 79, 92, 245, 14, 24, 27, 5, 78, 255, 228, 113, 61, 194, 101, 123, 234, 202, 98, 19, 194, 193, 44, 168, 244, 111, 151, 16, 239, 9, 194, 69, 143, 67, 196, 223, 53, 56, 248, 29, 58, 188, 144, 188, 219, 254, 174, 215, 233, 41, 146, 41, 69, 10, 89, 37, 111, 115, 187, 190, 82, 79, 96, 15, 209 }, "1234567892", "alexander99" },
                    { 4, "hayes@gmail.com", "Benjamin", false, false, "Hayes", new byte[] { 93, 185, 106, 184, 245, 250, 246, 206, 10, 244, 235, 187, 136, 37, 21, 201, 108, 228, 239, 145, 124, 139, 103, 113, 177, 251, 216, 69, 141, 237, 81, 70, 4, 118, 213, 21, 94, 2, 64, 153, 251, 173, 98, 114, 226, 122, 128, 187, 117, 168, 158, 187, 54, 105, 176, 65, 142, 58, 11, 179, 117, 240, 212, 147 }, new byte[] { 189, 157, 120, 176, 82, 4, 56, 229, 17, 193, 20, 242, 175, 16, 250, 193, 6, 12, 81, 29, 64, 67, 141, 10, 126, 87, 180, 32, 181, 236, 222, 141, 148, 76, 192, 59, 156, 83, 230, 141, 77, 92, 38, 9, 237, 249, 153, 221, 235, 153, 82, 27, 222, 162, 34, 250, 235, 231, 9, 45, 57, 71, 151, 39, 155, 165, 141, 22, 18, 132, 153, 148, 210, 186, 205, 194, 62, 212, 58, 114, 151, 73, 26, 102, 188, 33, 23, 242, 110, 248, 81, 103, 234, 142, 155, 89, 109, 68, 77, 146, 120, 67, 195, 25, 9, 73, 165, 3, 167, 144, 19, 19, 214, 48, 4, 156, 72, 116, 219, 86, 253, 66, 130, 221, 37, 183, 33, 36 }, "1234567893", "ben" },
                    { 5, "mitchell@gmail.com", "Ethan", false, false, "Mitchell", new byte[] { 180, 55, 120, 209, 122, 39, 14, 186, 241, 159, 12, 195, 234, 239, 90, 119, 215, 80, 214, 17, 158, 95, 238, 236, 231, 71, 218, 210, 14, 159, 40, 222, 112, 76, 148, 149, 77, 202, 19, 142, 199, 105, 73, 162, 95, 240, 113, 251, 31, 168, 154, 6, 211, 131, 160, 63, 182, 204, 73, 115, 82, 50, 76, 186 }, new byte[] { 31, 198, 92, 220, 175, 63, 102, 215, 5, 70, 27, 186, 97, 61, 174, 244, 133, 98, 68, 124, 106, 227, 126, 177, 19, 220, 198, 29, 111, 250, 168, 164, 71, 126, 51, 252, 209, 183, 3, 79, 33, 242, 41, 112, 211, 103, 20, 179, 73, 199, 108, 205, 24, 87, 99, 133, 125, 58, 144, 221, 169, 129, 34, 195, 250, 149, 43, 160, 97, 243, 128, 249, 66, 158, 58, 186, 178, 115, 180, 41, 189, 207, 133, 13, 183, 160, 59, 52, 48, 78, 0, 149, 53, 228, 168, 50, 21, 186, 210, 175, 226, 28, 246, 195, 246, 228, 106, 208, 27, 105, 254, 45, 114, 252, 207, 221, 227, 219, 38, 30, 77, 45, 71, 117, 167, 135, 157, 23 }, "1234567894", "ethan Mit" },
                    { 6, "reynolds@gmail.com", "Oliver", false, false, "Reynolds", new byte[] { 138, 57, 14, 12, 252, 192, 103, 161, 244, 55, 103, 117, 90, 194, 13, 148, 14, 148, 1, 1, 21, 157, 39, 91, 107, 112, 16, 239, 255, 84, 131, 18, 103, 238, 182, 56, 144, 253, 231, 1, 166, 131, 107, 14, 100, 28, 85, 46, 174, 234, 111, 40, 166, 25, 204, 45, 9, 18, 84, 165, 100, 50, 47, 28 }, new byte[] { 177, 139, 105, 134, 53, 130, 16, 231, 121, 71, 48, 33, 214, 12, 77, 202, 61, 126, 162, 43, 202, 70, 52, 203, 137, 40, 148, 183, 128, 97, 113, 14, 118, 139, 143, 112, 32, 27, 245, 127, 31, 92, 34, 163, 241, 128, 251, 7, 74, 153, 174, 27, 63, 49, 198, 174, 22, 206, 206, 12, 249, 186, 249, 219, 221, 224, 202, 188, 91, 221, 65, 23, 233, 85, 216, 24, 0, 3, 14, 188, 118, 155, 196, 141, 42, 171, 170, 116, 82, 97, 136, 74, 216, 99, 248, 250, 126, 99, 170, 102, 178, 102, 155, 243, 41, 249, 226, 128, 154, 152, 49, 77, 50, 59, 47, 93, 12, 70, 105, 86, 168, 16, 54, 46, 184, 240, 153, 241 }, "1234567895", "The Oliver_6" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "BrandId", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, "E 270" },
                    { 2, 1, false, "W223" },
                    { 3, 1, false, "GLA 200" },
                    { 4, 2, false, "x7 m60i" },
                    { 5, 2, false, "i5 m60" },
                    { 6, 2, false, "330e" },
                    { 7, 3, false, "Octavia 2.0" },
                    { 8, 3, false, "Superb 2.0" },
                    { 9, 3, false, "Kodiaq 2.0" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "CreationYear", "IsDeleted", "LicensePlate", "ModelId", "UserId", "VIN" },
                values: new object[,]
                {
                    { 1, 2005, false, "CA1234HH", 1, 2, "12345678901234567" },
                    { 2, 2008, false, "A1500AA", 2, 2, "52345678901234567" },
                    { 3, 2010, false, "BH7895AA", 3, 3, "42344678961234567" },
                    { 4, 2015, false, "C7894AA", 4, 4, "92345678991234557" },
                    { 5, 2013, false, "CA9635HH", 5, 5, "76543210987654321" },
                    { 6, 2017, false, "OB4579AA", 6, 5, "45646512637564321" },
                    { 7, 2009, false, "X1597AA", 7, 5, "96385274136925845" },
                    { 8, 2011, false, "PA4379HH", 8, 6, "14725836925836914" },
                    { 9, 2020, false, "TX0598AA", 9, 6, "15948726326159487" }
                });

            migrationBuilder.InsertData(
                table: "Visits",
                columns: new[] { "Id", "Date", "IsDeleted", "VehicleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 },
                    { 2, new DateTime(2017, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 },
                    { 3, new DateTime(2021, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 4, new DateTime(2015, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 5, new DateTime(2018, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3 },
                    { 6, new DateTime(2016, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3 },
                    { 7, new DateTime(2022, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4 },
                    { 8, new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4 },
                    { 9, new DateTime(2017, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5 },
                    { 10, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5 },
                    { 11, new DateTime(2019, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6 },
                    { 12, new DateTime(2017, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6 },
                    { 13, new DateTime(2021, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 7 },
                    { 14, new DateTime(2015, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 7 },
                    { 15, new DateTime(2018, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 8 },
                    { 16, new DateTime(2016, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 8 },
                    { 17, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 9 },
                    { 18, new DateTime(2020, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 9 }
                });

            migrationBuilder.InsertData(
                table: "MechanicVisit",
                columns: new[] { "MechanicId", "VisitId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 6 },
                    { 1, 8 },
                    { 2, 2 },
                    { 2, 4 },
                    { 2, 7 },
                    { 2, 10 },
                    { 3, 3 },
                    { 3, 5 },
                    { 3, 9 }
                });

            migrationBuilder.InsertData(
                table: "PartVisit",
                columns: new[] { "PartId", "VisitId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 10 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 }
                });

            migrationBuilder.InsertData(
                table: "ServiceVisit",
                columns: new[] { "ServiceId", "VisitId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 10 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MechanicVisit_VisitId",
                table: "MechanicVisit",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PartVisit_VisitId",
                table: "PartVisit",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceVisit_VisitId",
                table: "ServiceVisit",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelId",
                table: "Vehicles",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VehicleId",
                table: "Visits",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MechanicVisit");

            migrationBuilder.DropTable(
                name: "PartVisit");

            migrationBuilder.DropTable(
                name: "ServiceVisit");

            migrationBuilder.DropTable(
                name: "Mechanics");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
