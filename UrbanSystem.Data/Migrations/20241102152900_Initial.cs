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
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

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
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionsLocations",
                columns: table => new
                {
                    SuggestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionsLocations", x => new { x.SuggestionId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_SuggestionsLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionsLocations_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CityName", "StreetName" },
                values: new object[,]
                {
                    { new Guid("00dd078f-af4c-427e-8e47-f29d2237f161"), "Stara Zagora", "Main Street" },
                    { new Guid("1cd66606-fc52-48e2-9159-31af3efbc579"), "Sofia", "Main Street" },
                    { new Guid("22edb1b3-0659-4eb0-b943-f502c0801889"), "Targovishte", "Main Street" },
                    { new Guid("2c3e0582-53c0-4268-b3a7-42bcf2dfff66"), "Silistra", "Main Street" },
                    { new Guid("32a4ed35-f032-4d89-88d4-88889761aa26"), "Ruse", "Main Street" },
                    { new Guid("33e5fa81-9144-45ee-9b57-a576a2bf7798"), "Smolyan", "Main Street" },
                    { new Guid("3719cc70-9372-4fef-85c0-3e718b7021a3"), "Sliven", "Main Street" },
                    { new Guid("429482ce-7fed-48c8-91b3-07ffcd0ec578"), "Pernik", "Main Street" },
                    { new Guid("4f9c66ae-e680-46e8-b4ca-7c9573189298"), "Burgas", "Main Street" },
                    { new Guid("4f9cf36a-71c8-4c77-875c-8945adadbeff"), "Plovdiv", "Main Street" },
                    { new Guid("566dd22c-7393-42c6-ad61-18c79b0aa46e"), "Lovech", "Main Street" },
                    { new Guid("5f14d044-8e5f-441d-8a08-b22ce024d208"), "Kyustendil", "Main Street" },
                    { new Guid("6a9675a2-132a-4494-b154-ae79d8b91d12"), "Haskovo", "Main Street" },
                    { new Guid("6b2c7ff2-d6a2-487f-b5e3-1ac52410d926"), "Blagoevgrad", "Main Street" },
                    { new Guid("6bfba6b5-e821-4939-9aaf-8c9fd4a5918a"), "Razgrad", "Main Street" },
                    { new Guid("7a5d9877-243d-4e64-ba63-28deaa18b1b4"), "Varna", "Main Street" },
                    { new Guid("7bc44a26-bfbd-4b6b-a4c1-76d96ca639af"), "Dobrich", "Main Street" },
                    { new Guid("8b0ab1fa-1c83-4e36-8f95-28ee6851773b"), "Pleven", "Main Street" },
                    { new Guid("8d0598da-dc4e-4e06-b2be-e7fcee8852e1"), "Veliko Tarnovo", "Main Street" },
                    { new Guid("97d10799-2116-4b2e-9e21-9f01076390e5"), "Kardzhali", "Main Street" },
                    { new Guid("a882800e-eaae-4912-aed7-4442aeaf7cf1"), "Vidin", "Main Street" },
                    { new Guid("b0225b6d-b916-46b5-a8d1-899705b56c8f"), "Gabrovo", "Main Street" },
                    { new Guid("b6403aed-f4ee-46e0-b18f-104a20cd479b"), "Montana", "Main Street" },
                    { new Guid("b7344ed3-9820-44ed-b034-2ebb3f8b0d5f"), "Yambol", "Main Street" },
                    { new Guid("c6fb5772-3761-4559-b83a-c1a15a2aff44"), "Pazardzhik", "Main Street" },
                    { new Guid("c79391d8-f3c5-4150-a399-aba2e0c29433"), "Shumen", "Main Street" },
                    { new Guid("deb6ead4-28df-441b-ae3a-21dd8c130a8a"), "Vratsa", "Main Street" }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "AttachmentUrl", "Category", "Description", "Downvotes", "Priority", "Status", "Title", "UploadedOn", "Upvotes" },
                values: new object[,]
                {
                    { new Guid("480414ea-1514-48ca-adb9-30e3c7596d5c"), null, "Waste Management", "Introduce a recycling program and increase the frequency of waste collection.", 0, "High", "In Review", "Waste Management System", new DateTime(2024, 11, 2, 15, 28, 59, 566, DateTimeKind.Utc).AddTicks(3129), 15 },
                    { new Guid("64a8408b-aa81-4f1e-aa57-e60eb3475c07"), null, "Transport", "Implement more frequent bus routes during peak hours to reduce congestion.", 2, "High", "Pending", "Improve Public Transport", new DateTime(2024, 11, 2, 15, 28, 59, 566, DateTimeKind.Utc).AddTicks(3116), 10 },
                    { new Guid("acb4679b-6143-4edb-9d35-00576d2cd7c7"), null, "Environment", "Renovate the central park by adding new benches, lighting, and a playground area.", 1, "Medium", "Approved", "Park Renovation", new DateTime(2024, 11, 2, 15, 28, 59, 566, DateTimeKind.Utc).AddTicks(3124), 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionsLocations_LocationId",
                table: "SuggestionsLocations",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuggestionsLocations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Suggestions");
        }
    }
}
