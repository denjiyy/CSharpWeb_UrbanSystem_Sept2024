﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrbanSystem.Data;

#nullable disable

namespace UrbanSystem.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241104125711_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CityPicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("533735bd-a096-4cc8-b68c-3f3a11c136ee"),
                            CityName = "Blagoevgrad",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg/1024px-%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("0cbe8dd8-9a99-4a52-93ae-031de0b92dbd"),
                            CityName = "Burgas",
                            CityPicture = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/08/19/fe/30/getlstd-property-photo.jpg?w=1200&h=-1&s=1",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("af80702f-26ff-4bb1-8e98-678fb30e9838"),
                            CityName = "Dobrich",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Dobrich_Sunrise%2C_Winter_2014.JPG/1280px-Dobrich_Sunrise%2C_Winter_2014.JPG",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("382ddb37-0ab9-47f8-bae1-86fc3c316168"),
                            CityName = "Gabrovo",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/TownHall_Gabrovo.jpg/1280px-TownHall_Gabrovo.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("31f0e66e-5e36-4a21-8958-40a9d314932b"),
                            CityName = "Haskovo",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Haskovo2.jpg/1024px-Haskovo2.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("6430e049-1fa1-4709-bebe-7c6bae19264c"),
                            CityName = "Kardzhali",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/5/5d/%D0%98%D1%81%D1%82%D0%BE%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D1%8F%D1%82_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%9A%D1%8A%D1%80%D0%B4%D0%B6%D0%B0%D0%BB%D0%B8.JPG",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("fd7cf26c-4f13-425a-a60d-405ac53ede4e"),
                            CityName = "Kyustendil",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Kyustendil_25.jpg/1024px-Kyustendil_25.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("e34865ee-03d7-4ae0-b232-5352349c7872"),
                            CityName = "Lovech",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/Bulgaria-Lovech-03.jpg/1024px-Bulgaria-Lovech-03.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("3a1faff9-45ac-40cd-9d1f-a8ef5da5df0b"),
                            CityName = "Montana",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/4/47/Montana-downtown.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("583db53a-5f51-4e22-ba89-e8f4c9a6703b"),
                            CityName = "Pazardzhik",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Pazardzhik_City_Centre.jpg/1024px-Pazardzhik_City_Centre.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("837b5063-f8ac-4eed-9c71-b68d471bf406"),
                            CityName = "Pernik",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Pernik-culture-palace-left.jpg/1920px-Pernik-culture-palace-left.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("f2c810d9-ec81-4a3f-90c6-3397f5b57e92"),
                            CityName = "Pleven",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg/1280px-%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("004bbeb9-d272-4605-a933-ea1d149bc50a"),
                            CityName = "Plovdiv",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg/1280px-Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("f8e3c7c2-5ee9-4349-a1c3-72a36c7b5770"),
                            CityName = "Razgrad",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg/1280px-%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("951a2d76-0411-4d66-8e35-44e29be3701f"),
                            CityName = "Ruse",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg/1280px-%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("03aca43d-2b21-4d9a-abbc-fbd33e00a0de"),
                            CityName = "Shumen",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/5/51/Shumen_chitalishte_Dobri_Voynikov.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("e213d2f4-40f3-4c5a-832c-cf73b47b99c5"),
                            CityName = "Silistra",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Silistra-art-gallery-Minkov.jpg/1024px-Silistra-art-gallery-Minkov.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("332c9c44-dc07-4f5d-81a2-4610f64653c8"),
                            CityName = "Sliven",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Municipality_of_Sliven_Photo.jpg/1280px-Municipality_of_Sliven_Photo.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("9e27b7ba-aaf6-4c48-813a-9583e9d76468"),
                            CityName = "Smolyan",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg/1024px-%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("623cb010-b1b1-4542-8afb-620955fdc787"),
                            CityName = "Sofia",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg/1280px-Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("d9d1b4e4-f7f7-4420-8a7e-76f361f7857b"),
                            CityName = "Stara Zagora",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/3/33/Samarsko_Zname_Panorama.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("daa7c335-f704-43d9-8082-ba899904451c"),
                            CityName = "Targovishte",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Targovishte-MainSquare.jpg/1280px-Targovishte-MainSquare.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("76e8abf7-9729-43e9-959b-6ad1bf11e4cd"),
                            CityName = "Varna",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/en/thumb/7/79/Dramatheatrevarna.jpg/1280px-Dramatheatrevarna.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("656b8e02-dd6f-49f6-bacd-23747498734e"),
                            CityName = "Veliko Tarnovo",
                            CityPicture = "https://traventuria.com/wp-content/uploads/2016/10/veliko-tarnovo-1.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("7e3347be-c0aa-4b98-9262-6297a38e7898"),
                            CityName = "Vidin",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/7/70/Theater_House_in_Vidin_%2827460729905%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("3596c33c-f4e8-4418-9e92-a901851b1b89"),
                            CityName = "Vratsa",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Vratsa_12.jpg/1024px-Vratsa_12.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("d99a90e4-03db-48c7-936c-c6587139ecdd"),
                            CityName = "Yambol",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9d/YAMBOL_new_center.jpg/1920px-YAMBOL_new_center.jpg",
                            StreetName = "Main Street"
                        });
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Suggestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AttachmentUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Downvotes")
                        .HasColumnType("int");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UploadedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Upvotes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Suggestions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1c05e02a-fccb-421c-9ea3-f0304655d973"),
                            Category = "Transport",
                            Description = "Implement more frequent bus routes during peak hours to reduce congestion.",
                            Downvotes = 2,
                            Priority = "High",
                            Status = "Pending",
                            Title = "Improve Public Transport",
                            UploadedOn = new DateTime(2024, 11, 4, 12, 57, 11, 269, DateTimeKind.Utc).AddTicks(3019),
                            Upvotes = 10
                        },
                        new
                        {
                            Id = new Guid("0e21d8d8-d70e-4d74-ba70-0f37ce9eb143"),
                            Category = "Environment",
                            Description = "Renovate the central park by adding new benches, lighting, and a playground area.",
                            Downvotes = 1,
                            Priority = "Medium",
                            Status = "Approved",
                            Title = "Park Renovation",
                            UploadedOn = new DateTime(2024, 11, 4, 12, 57, 11, 269, DateTimeKind.Utc).AddTicks(3083),
                            Upvotes = 25
                        },
                        new
                        {
                            Id = new Guid("9b989244-fdd0-4033-8518-cd693e9a7b35"),
                            Category = "Waste Management",
                            Description = "Introduce a recycling program and increase the frequency of waste collection.",
                            Downvotes = 0,
                            Priority = "High",
                            Status = "In Review",
                            Title = "Waste Management System",
                            UploadedOn = new DateTime(2024, 11, 4, 12, 57, 11, 269, DateTimeKind.Utc).AddTicks(3090),
                            Upvotes = 15
                        });
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.SuggestionLocation", b =>
                {
                    b.Property<Guid>("SuggestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SuggestionId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("SuggestionsLocations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.SuggestionLocation", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.Location", "Location")
                        .WithMany("SuggestionsLocations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UrbanSystem.Data.Models.Suggestion", "Suggestion")
                        .WithMany("SuggestionsLocations")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Location", b =>
                {
                    b.Navigation("SuggestionsLocations");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Suggestion", b =>
                {
                    b.Navigation("SuggestionsLocations");
                });
#pragma warning restore 612, 618
        }
    }
}