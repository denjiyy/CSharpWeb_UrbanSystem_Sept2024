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
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
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
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuggestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CityName", "StreetName" },
                values: new object[,]
                {
                    { new Guid("164ec9c4-49c0-43ed-8c63-9a88c0591d41"), "Yambol", "Not Specified" },
                    { new Guid("202b67cc-83f5-4e4e-b4d2-69d785f62739"), "Pleven", "Not Specified" },
                    { new Guid("2b7d3f4e-b179-4d02-bc2a-3d54b4930387"), "Kyustendil", "Not Specified" },
                    { new Guid("2e6e8718-8bc3-4b26-bcf2-b7c3d5c54e5d"), "Gabrovo", "Not Specified" },
                    { new Guid("49738b4c-5c9d-4700-8aa6-89c0e62242c4"), "Razgrad", "Not Specified" },
                    { new Guid("4a4d4203-105c-4517-b375-09f64673b020"), "Lovech", "Not Specified" },
                    { new Guid("4c4b5ecf-83c7-4937-91b2-b19e9db55ac5"), "Sliven", "Not Specified" },
                    { new Guid("4d23eb75-1dc0-4373-8bc4-d229e38e7f85"), "Pazardzhik", "Not Specified" },
                    { new Guid("5a3b29b8-017e-4e57-8155-d8651da8be5e"), "Shumen", "Not Specified" },
                    { new Guid("63205d79-f5fc-4903-8458-d64f658e0ae3"), "Haskovo", "Not Specified" },
                    { new Guid("679b9b57-98d8-4fc1-bc65-bb98a5159168"), "Stara Zagora", "Not Specified" },
                    { new Guid("69c62c8d-1040-469e-b7ea-688e8c1e6587"), "Dobrich", "Not Specified" },
                    { new Guid("89ee0af6-8a02-45cf-8a01-bb8f56a94b90"), "Blagoevgrad", "Not Specified" },
                    { new Guid("9a5726aa-76a0-4db7-9d08-660ae5ebdc1a"), "Vidin", "Not Specified" },
                    { new Guid("b55cfa08-7e8e-4f1b-9f0f-dc7e4fbc6d97"), "Sofia", "Not Specified" },
                    { new Guid("b789b78d-f118-4f7a-b22d-4e165cbe5303"), "Burgas", "Not Specified" },
                    { new Guid("c86d7f51-e3c4-4f62-b557-b7bffdecbfe9"), "Plovdiv", "Not Specified" },
                    { new Guid("d09c4464-409b-41f2-b9e5-53bb459b0e25"), "Silistra", "Not Specified" },
                    { new Guid("e6c6468e-3f8b-4bc7-b0be-0d143785f637"), "Pernik", "Not Specified" },
                    { new Guid("e865e9c8-bb7c-4a0d-8715-58c4f5dcd7d0"), "Ruse", "Not Specified" },
                    { new Guid("ee8d2310-e9ca-45bc-a0be-d5a5bc57857e"), "Targovishte", "Not Specified" },
                    { new Guid("f34d29f1-df9e-45e0-bd0b-c8e2322e17b4"), "Smolyan", "Not Specified" },
                    { new Guid("f79fa82b-30e8-49ea-b1b1-df85cb202c9e"), "Varna", "Not Specified" },
                    { new Guid("f8b8c3b0-b9ab-43c4-9112-1869be6cc176"), "Montana", "Not Specified" },
                    { new Guid("ff8f7f5c-e69d-4d7b-888b-f219a9ab4c71"), "Vratsa", "Not Specified" }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "AttachmentUrl", "Category", "Description", "Downvotes", "Priority", "ProvinceId", "Status", "Title", "UploadedOn", "Upvotes" },
                values: new object[,]
                {
                    { new Guid("481da68b-ceda-4f12-9ca3-6fb0ed164fa5"), null, "Environment", "Renovate the central park by adding new benches, lighting, and a playground area.", 1, "Medium", new Guid("b55cfa08-7e8e-4f1b-9f0f-dc7e4fbc6d97"), "Approved", "Park Renovation", new DateTime(2024, 10, 29, 19, 37, 39, 336, DateTimeKind.Utc).AddTicks(3341), 25 },
                    { new Guid("8ae884a3-19ee-4140-bb7f-73d35c98904e"), null, "Waste Management", "Introduce a recycling program and increase the frequency of waste collection.", 0, "High", new Guid("c86d7f51-e3c4-4f62-b557-b7bffdecbfe9"), "In Review", "Waste Management System", new DateTime(2024, 10, 29, 19, 37, 39, 336, DateTimeKind.Utc).AddTicks(3344), 15 },
                    { new Guid("cd0189a7-22df-4c52-888e-ef7da46e322c"), null, "Transport", "Implement more frequent bus routes during peak hours to reduce congestion.", 2, "High", new Guid("b55cfa08-7e8e-4f1b-9f0f-dc7e4fbc6d97"), "Pending", "Improve Public Transport", new DateTime(2024, 10, 29, 19, 37, 39, 336, DateTimeKind.Utc).AddTicks(3328), 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SuggestionId",
                table: "Comments",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ProvinceId",
                table: "Suggestions",
                column: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
