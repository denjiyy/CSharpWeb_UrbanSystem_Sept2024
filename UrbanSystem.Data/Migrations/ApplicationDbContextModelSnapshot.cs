﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrbanSystem.Data;

#nullable disable

namespace UrbanSystem.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

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

            modelBuilder.Entity("UrbanSystem.Data.Models.ApplicationUserSuggestion", b =>
                {
                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SuggestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicationUserId", "SuggestionId");

                    b.HasIndex("SuggestionId");

                    b.ToTable("UsersSuggestions");
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
                            Id = new Guid("5ca97e32-da89-434f-bd61-08bf8f3e2907"),
                            CityName = "Blagoevgrad",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg/1024px-%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("640c087f-320e-4f1d-adce-f487e5f595ee"),
                            CityName = "Burgas",
                            CityPicture = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/08/19/fe/30/getlstd-property-photo.jpg?w=1200&h=-1&s=1",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("a981a3b8-b776-4a8b-83ba-977e4cde0e86"),
                            CityName = "Dobrich",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Dobrich_Sunrise%2C_Winter_2014.JPG/1280px-Dobrich_Sunrise%2C_Winter_2014.JPG",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("b218acf5-6cbe-4b38-b8bf-8fdc917252d3"),
                            CityName = "Gabrovo",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/TownHall_Gabrovo.jpg/1280px-TownHall_Gabrovo.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("cdc3a981-f7c9-44f3-ba1a-a041eb2898bf"),
                            CityName = "Haskovo",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Haskovo2.jpg/1024px-Haskovo2.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("9d653268-1f61-4e19-9225-696c288b6fa3"),
                            CityName = "Kardzhali",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/5/5d/%D0%98%D1%81%D1%82%D0%BE%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D1%8F%D1%82_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%9A%D1%8A%D1%80%D0%B4%D0%B6%D0%B0%D0%BB%D0%B8.JPG",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("07a79723-82b6-484d-95ed-10e154e645b9"),
                            CityName = "Kyustendil",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Kyustendil_25.jpg/1024px-Kyustendil_25.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("8a1acce1-5b51-470c-9d15-8939dfd09f78"),
                            CityName = "Lovech",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/Bulgaria-Lovech-03.jpg/1024px-Bulgaria-Lovech-03.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("293314b0-6b4a-4068-9b5a-0b28ad2426b9"),
                            CityName = "Montana",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/4/47/Montana-downtown.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("aaf56dbd-9073-4561-9131-3dec8b3c6ac3"),
                            CityName = "Pazardzhik",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Pazardzhik_City_Centre.jpg/1024px-Pazardzhik_City_Centre.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("612fac39-6ffc-4106-9ae2-6ed90c8fde8e"),
                            CityName = "Pernik",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Pernik-culture-palace-left.jpg/1920px-Pernik-culture-palace-left.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("cf318f98-922f-4b1e-b0ae-f1e9fdc3429f"),
                            CityName = "Pleven",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg/1280px-%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("819aebf0-bf22-4994-8f64-60ff5c5cdeac"),
                            CityName = "Plovdiv",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg/1280px-Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("da1a7b15-0499-4cba-8a2c-363f8f10357b"),
                            CityName = "Razgrad",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg/1280px-%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("cc0d3e10-8316-4957-ba88-9527994cd32a"),
                            CityName = "Ruse",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg/1280px-%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("be915054-bbaa-4889-80ef-e4825713acb3"),
                            CityName = "Shumen",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/5/51/Shumen_chitalishte_Dobri_Voynikov.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("220b6710-70a3-4e1d-908d-0d1c9a538a97"),
                            CityName = "Silistra",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Silistra-art-gallery-Minkov.jpg/1024px-Silistra-art-gallery-Minkov.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("7453efaf-6548-4603-959d-5a0d8ae939ec"),
                            CityName = "Sliven",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Municipality_of_Sliven_Photo.jpg/1280px-Municipality_of_Sliven_Photo.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("48ddfad3-49bf-4718-b085-4e1d4ad1e8a8"),
                            CityName = "Smolyan",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg/1024px-%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("9a269cfc-561e-49a2-ab49-234d00880531"),
                            CityName = "Sofia",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg/1280px-Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("3c3aa676-55bf-4d87-8244-944e794e2eda"),
                            CityName = "Stara Zagora",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/3/33/Samarsko_Zname_Panorama.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("8c1fc7c1-05b3-4585-9323-f781d2c0f404"),
                            CityName = "Targovishte",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Targovishte-MainSquare.jpg/1280px-Targovishte-MainSquare.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("cd8f907b-8aa3-4f40-b825-64fbb208c025"),
                            CityName = "Varna",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/en/thumb/7/79/Dramatheatrevarna.jpg/1280px-Dramatheatrevarna.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("c4dddeb6-28a7-4ccd-a6c1-6d016b0a7812"),
                            CityName = "Veliko Tarnovo",
                            CityPicture = "https://traventuria.com/wp-content/uploads/2016/10/veliko-tarnovo-1.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("b52b0a66-9357-4144-a80a-c4afc11b8832"),
                            CityName = "Vidin",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/7/70/Theater_House_in_Vidin_%2827460729905%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("794ec554-aa74-450a-aa69-3310548497d0"),
                            CityName = "Vratsa",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Vratsa_12.jpg/1024px-Vratsa_12.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("bceb820c-4455-43cb-ac54-05d1c71fb8bc"),
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
                            Id = new Guid("d7a7dcce-b966-40f9-aa0d-7873c442c73c"),
                            Category = "Transport",
                            Description = "Implement more frequent bus routes during peak hours to reduce congestion.",
                            Downvotes = 2,
                            Priority = "High",
                            Status = "Pending",
                            Title = "Improve Public Transport",
                            UploadedOn = new DateTime(2024, 11, 4, 19, 57, 18, 440, DateTimeKind.Utc).AddTicks(3127),
                            Upvotes = 10
                        },
                        new
                        {
                            Id = new Guid("e3ac0c8c-736d-473d-9f6e-e253ccef8fb2"),
                            Category = "Environment",
                            Description = "Renovate the central park by adding new benches, lighting, and a playground area.",
                            Downvotes = 1,
                            Priority = "Medium",
                            Status = "Approved",
                            Title = "Park Renovation",
                            UploadedOn = new DateTime(2024, 11, 4, 19, 57, 18, 440, DateTimeKind.Utc).AddTicks(3132),
                            Upvotes = 25
                        },
                        new
                        {
                            Id = new Guid("cf06e869-d298-4301-834f-30952daa21d3"),
                            Category = "Waste Management",
                            Description = "Introduce a recycling program and increase the frequency of waste collection.",
                            Downvotes = 0,
                            Priority = "High",
                            Status = "In Review",
                            Title = "Waste Management System",
                            UploadedOn = new DateTime(2024, 11, 4, 19, 57, 18, 440, DateTimeKind.Utc).AddTicks(3140),
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

            modelBuilder.Entity("UrbanSystem.Data.Models.ApplicationUserSuggestion", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", "User")
                        .WithMany("UsersSuggestions")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UrbanSystem.Data.Models.Suggestion", "Suggestion")
                        .WithMany("UsersSuggestions")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");

                    b.Navigation("User");
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

            modelBuilder.Entity("UrbanSystem.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("UsersSuggestions");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Location", b =>
                {
                    b.Navigation("SuggestionsLocations");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Suggestion", b =>
                {
                    b.Navigation("SuggestionsLocations");

                    b.Navigation("UsersSuggestions");
                });
#pragma warning restore 612, 618
        }
    }
}
