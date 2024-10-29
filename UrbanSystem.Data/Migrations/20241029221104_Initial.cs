using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UrbanSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upvotes = table.Column<int>(type: "int", nullable: false),
                    Downvotes = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "AttachmentUrl", "Category", "CityName", "Description", "Downvotes", "Priority", "Status", "StreetName", "Title", "UploadedOn", "Upvotes" },
                values: new object[,]
                {
                    { new Guid("6242e74d-bca9-4d79-946b-cea802088b7e"), null, "Waste Management", "Blagoevgrad", "Introduce a recycling program and increase the frequency of waste collection.", 0, "High", "In Review", "Polk. Drangov 4", "Waste Management System", new DateTime(2024, 10, 29, 22, 11, 3, 792, DateTimeKind.Utc).AddTicks(4116), 15 },
                    { new Guid("6dcc9c93-f8e3-4c85-83cd-c18c1ea58b7a"), null, "Transport", "Sofia", "Implement more frequent bus routes during peak hours to reduce congestion.", 2, "High", "Pending", "Ne znam", "Improve Public Transport", new DateTime(2024, 10, 29, 22, 11, 3, 792, DateTimeKind.Utc).AddTicks(4108), 10 },
                    { new Guid("c852ed10-70b7-4d9e-9fdd-6cb4ab407155"), null, "Environment", "Sofia", "Renovate the central park by adding new benches, lighting, and a playground area.", 1, "Medium", "Approved", "Ne znam", "Park Renovation", new DateTime(2024, 10, 29, 22, 11, 3, 792, DateTimeKind.Utc).AddTicks(4113), 25 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suggestions");
        }
    }
}
