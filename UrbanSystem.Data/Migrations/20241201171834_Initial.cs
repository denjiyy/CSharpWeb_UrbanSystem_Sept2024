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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CityPicture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meetings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FundsNeeded = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FundingDeadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuggestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "UsersSuggestions",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuggestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSuggestions", x => new { x.ApplicationUserId, x.SuggestionId });
                    table.ForeignKey(
                        name: "FK_UsersSuggestions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersSuggestions_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserMeeting",
                columns: table => new
                {
                    AttendeesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMeeting", x => new { x.AttendeesId, x.MeetingsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMeeting_AspNetUsers_AttendeesId",
                        column: x => x.AttendeesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMeeting_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "CityName", "CityPicture", "StreetName" },
                values: new object[,]
                {
                    { new Guid("0e6d45d6-60c1-4b52-85de-aeabe59a2ba0"), "Pazardzhik", "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Pazardzhik_City_Centre.jpg/1024px-Pazardzhik_City_Centre.jpg", "Main Street" },
                    { new Guid("10068c72-58a7-474d-abe4-241b47d61b73"), "Silistra", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Silistra-art-gallery-Minkov.jpg/1024px-Silistra-art-gallery-Minkov.jpg", "Main Street" },
                    { new Guid("110983af-9da5-4b2f-8e2d-694637bc8bc4"), "Shumen", "https://upload.wikimedia.org/wikipedia/commons/5/51/Shumen_chitalishte_Dobri_Voynikov.jpg", "Main Street" },
                    { new Guid("1be05a7d-a1a3-4af6-9ae7-cfec5f63d891"), "Plovdiv", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg/1280px-Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg", "Main Street" },
                    { new Guid("20b84a27-dc84-460a-a837-88a77925940d"), "Varna", "https://upload.wikimedia.org/wikipedia/en/thumb/7/79/Dramatheatrevarna.jpg/1280px-Dramatheatrevarna.jpg", "Main Street" },
                    { new Guid("225816e7-c050-47c3-98ae-2c0149929fde"), "Vratsa", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Vratsa_12.jpg/1024px-Vratsa_12.jpg", "Main Street" },
                    { new Guid("241cebd9-8c0c-4596-af4d-df0f7adf7807"), "Kardzhali", "https://upload.wikimedia.org/wikipedia/commons/5/5d/%D0%98%D1%81%D1%82%D0%BE%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D1%8F%D1%82_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%9A%D1%8A%D1%80%D0%B4%D0%B6%D0%B0%D0%BB%D0%B8.JPG", "Main Street" },
                    { new Guid("25e3cfd8-5ae8-44f4-bfa6-9f390125106f"), "Gabrovo", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/TownHall_Gabrovo.jpg/1280px-TownHall_Gabrovo.jpg", "Main Street" },
                    { new Guid("46c32d4f-a5ef-476d-9b04-65bec2bce9f4"), "Dobrich", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Dobrich_Sunrise%2C_Winter_2014.JPG/1280px-Dobrich_Sunrise%2C_Winter_2014.JPG", "Main Street" },
                    { new Guid("4e213330-cc7f-4008-9f93-69f343cbe2e3"), "Vidin", "https://upload.wikimedia.org/wikipedia/commons/7/70/Theater_House_in_Vidin_%2827460729905%29.jpg", "Main Street" },
                    { new Guid("811e1136-b232-4c8b-8482-7aaf42db27c3"), "Burgas", "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/08/19/fe/30/getlstd-property-photo.jpg?w=1200&h=-1&s=1", "Main Street" },
                    { new Guid("81207dd9-94c3-49d1-804f-8e6482a21792"), "Blagoevgrad", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg/1024px-%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg", "Main Street" },
                    { new Guid("930af827-a0ef-46aa-b71c-5fbf6d388f7d"), "Razgrad", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg/1280px-%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg", "Main Street" },
                    { new Guid("95d67887-2d9e-4867-8a93-611d9ee986e8"), "Lovech", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/Bulgaria-Lovech-03.jpg/1024px-Bulgaria-Lovech-03.jpg", "Main Street" },
                    { new Guid("96ab9ae8-7eee-4d06-b56e-0d93d5d7c02c"), "Pleven", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg/1280px-%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg", "Main Street" },
                    { new Guid("983dcc64-0973-495c-a297-efe39565d04e"), "Veliko Tarnovo", "https://traventuria.com/wp-content/uploads/2016/10/veliko-tarnovo-1.jpg", "Main Street" },
                    { new Guid("ab3203e1-43b6-4d8b-93cf-4fe83384c127"), "Ruse", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg/1280px-%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg", "Main Street" },
                    { new Guid("b84d9d74-af49-4248-8322-6bf1c7f56577"), "Montana", "https://upload.wikimedia.org/wikipedia/commons/4/47/Montana-downtown.jpg", "Main Street" },
                    { new Guid("cf9c2f5c-6cc9-47ac-bca5-2ebe15af018d"), "Kyustendil", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Kyustendil_25.jpg/1024px-Kyustendil_25.jpg", "Main Street" },
                    { new Guid("d30c2b07-f783-4b4e-85b3-977890f731ea"), "Haskovo", "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Haskovo2.jpg/1024px-Haskovo2.jpg", "Main Street" },
                    { new Guid("d48f8089-d2e0-496d-b5e3-72365424c777"), "Pernik", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Pernik-culture-palace-left.jpg/1920px-Pernik-culture-palace-left.jpg", "Main Street" },
                    { new Guid("d4927a1c-89ff-4e42-82db-27e14407dd01"), "Sliven", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Municipality_of_Sliven_Photo.jpg/1280px-Municipality_of_Sliven_Photo.jpg", "Main Street" },
                    { new Guid("e69431ec-f288-4ed1-85f1-342489bd10fb"), "Yambol", "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9d/YAMBOL_new_center.jpg/1920px-YAMBOL_new_center.jpg", "Main Street" },
                    { new Guid("ea68dd64-3b23-46aa-8725-af025e802e28"), "Sofia", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg/1280px-Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg", "Main Street" },
                    { new Guid("eed57fb7-2699-465b-91e3-fd80ee8504c3"), "Smolyan", "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg/1024px-%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg", "Main Street" },
                    { new Guid("f56f5aba-5d69-4f6a-a7af-c89f58cf7a0d"), "Stara Zagora", "https://upload.wikimedia.org/wikipedia/commons/3/33/Samarsko_Zname_Panorama.jpg", "Main Street" },
                    { new Guid("f8fe8288-0709-4bd6-b8d5-02e7bf5eb9fb"), "Targovishte", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Targovishte-MainSquare.jpg/1280px-Targovishte-MainSquare.jpg", "Main Street" }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "ApplicationUserId", "AttachmentUrl", "Category", "Description", "Priority", "Status", "Title", "UploadedOn" },
                values: new object[,]
                {
                    { new Guid("3e1556ac-3d70-4dba-81a0-b22b7d07e984"), null, null, "Transport", "Implement more frequent bus routes during peak hours to reduce congestion.", "High", "Pending", "Improve Public Transport", new DateTime(2024, 12, 1, 17, 18, 33, 386, DateTimeKind.Utc).AddTicks(3309) },
                    { new Guid("6e69f616-e74e-4804-95ac-5fa313c6f880"), null, null, "Waste Management", "Introduce a recycling program and increase the frequency of waste collection.", "High", "In Review", "Waste Management System", new DateTime(2024, 12, 1, 17, 18, 33, 386, DateTimeKind.Utc).AddTicks(3318) },
                    { new Guid("ad626249-6ec9-42d5-ad67-a53a5b562cc3"), null, null, "Environment", "Renovate the central park by adding new benches, lighting, and a playground area.", "Medium", "Approved", "Park Renovation", new DateTime(2024, 12, 1, 17, 18, 33, 386, DateTimeKind.Utc).AddTicks(3315) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMeeting_MeetingsId",
                table: "ApplicationUserMeeting",
                column: "MeetingsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SuggestionId",
                table: "Comments",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LocationId",
                table: "Meetings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_OrganizerId",
                table: "Meetings",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LocationId",
                table: "Projects",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ApplicationUserId",
                table: "Suggestions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionsLocations_LocationId",
                table: "SuggestionsLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSuggestions_SuggestionId",
                table: "UsersSuggestions",
                column: "SuggestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMeeting");

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
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "SuggestionsLocations");

            migrationBuilder.DropTable(
                name: "UsersSuggestions");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
