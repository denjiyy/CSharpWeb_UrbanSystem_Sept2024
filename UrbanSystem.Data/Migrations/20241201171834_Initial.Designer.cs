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
    [Migration("20241201171834_Initial")]
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

            modelBuilder.Entity("ApplicationUserMeeting", b =>
                {
                    b.Property<Guid>("AttendeesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MeetingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AttendeesId", "MeetingsId");

                    b.HasIndex("MeetingsId");

                    b.ToTable("ApplicationUserMeeting");
                });

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

            modelBuilder.Entity("UrbanSystem.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<Guid>("SuggestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("SuggestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
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
                            Id = new Guid("81207dd9-94c3-49d1-804f-8e6482a21792"),
                            CityName = "Blagoevgrad",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg/1024px-%D0%91%D0%BB%D0%B0%D0%B3%D0%BE%D0%B5%D0%B2%D0%B3%D1%80%D0%B0%D0%B4_-_panoramio_%2826%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("811e1136-b232-4c8b-8482-7aaf42db27c3"),
                            CityName = "Burgas",
                            CityPicture = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/08/19/fe/30/getlstd-property-photo.jpg?w=1200&h=-1&s=1",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("46c32d4f-a5ef-476d-9b04-65bec2bce9f4"),
                            CityName = "Dobrich",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Dobrich_Sunrise%2C_Winter_2014.JPG/1280px-Dobrich_Sunrise%2C_Winter_2014.JPG",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("25e3cfd8-5ae8-44f4-bfa6-9f390125106f"),
                            CityName = "Gabrovo",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/TownHall_Gabrovo.jpg/1280px-TownHall_Gabrovo.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("d30c2b07-f783-4b4e-85b3-977890f731ea"),
                            CityName = "Haskovo",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Haskovo2.jpg/1024px-Haskovo2.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("241cebd9-8c0c-4596-af4d-df0f7adf7807"),
                            CityName = "Kardzhali",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/5/5d/%D0%98%D1%81%D1%82%D0%BE%D1%80%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%B8%D1%8F%D1%82_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%9A%D1%8A%D1%80%D0%B4%D0%B6%D0%B0%D0%BB%D0%B8.JPG",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("cf9c2f5c-6cc9-47ac-bca5-2ebe15af018d"),
                            CityName = "Kyustendil",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/09/Kyustendil_25.jpg/1024px-Kyustendil_25.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("95d67887-2d9e-4867-8a93-611d9ee986e8"),
                            CityName = "Lovech",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3b/Bulgaria-Lovech-03.jpg/1024px-Bulgaria-Lovech-03.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("b84d9d74-af49-4248-8322-6bf1c7f56577"),
                            CityName = "Montana",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/4/47/Montana-downtown.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("0e6d45d6-60c1-4b52-85de-aeabe59a2ba0"),
                            CityName = "Pazardzhik",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Pazardzhik_City_Centre.jpg/1024px-Pazardzhik_City_Centre.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("d48f8089-d2e0-496d-b5e3-72365424c777"),
                            CityName = "Pernik",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Pernik-culture-palace-left.jpg/1920px-Pernik-culture-palace-left.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("96ab9ae8-7eee-4d06-b56e-0d93d5d7c02c"),
                            CityName = "Pleven",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/91/%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg/1280px-%D0%9F%D0%BB%D0%B5%D0%B2%D0%B5%D0%BD_%D0%BC%D0%B0%D1%80%D1%82_2014_-_panoramio_%281%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("1be05a7d-a1a3-4af6-9ae7-cfec5f63d891"),
                            CityName = "Plovdiv",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg/1280px-Bulgaria_Bulgaria-0785_-_Roman_Theatre_of_Philippopolis_%287432772486%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("930af827-a0ef-46aa-b71c-5fbf6d388f7d"),
                            CityName = "Razgrad",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d4/%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg/1280px-%D0%95%D1%82%D0%BD%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D1%81%D0%BA%D0%B8_%D0%BC%D1%83%D0%B7%D0%B5%D0%B9_%D0%B2_%D0%B3%D1%80%D0%B0%D0%B4_%D0%A0%D0%B0%D0%B7%D0%B3%D1%80%D0%B0%D0%B4.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("ab3203e1-43b6-4d8b-93cf-4fe83384c127"),
                            CityName = "Ruse",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg/1280px-%D0%9E%D0%BF%D0%B5%D1%80%D0%B0%D1%82%D0%B0_%D0%B2_%D0%A0%D1%83%D1%81%D0%B5.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("110983af-9da5-4b2f-8e2d-694637bc8bc4"),
                            CityName = "Shumen",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/5/51/Shumen_chitalishte_Dobri_Voynikov.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("10068c72-58a7-474d-abe4-241b47d61b73"),
                            CityName = "Silistra",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Silistra-art-gallery-Minkov.jpg/1024px-Silistra-art-gallery-Minkov.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("d4927a1c-89ff-4e42-82db-27e14407dd01"),
                            CityName = "Sliven",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Municipality_of_Sliven_Photo.jpg/1280px-Municipality_of_Sliven_Photo.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("eed57fb7-2699-465b-91e3-fd80ee8504c3"),
                            CityName = "Smolyan",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg/1024px-%D0%A1%D0%BC%D0%BE%D0%BB%D1%8F%D0%BD_2691396959_f63b323fab_o.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("ea68dd64-3b23-46aa-8725-af025e802e28"),
                            CityName = "Sofia",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg/1280px-Catedral_de_Alejandro_Nevski_--_2019_--_Sof%C3%ADa%2C_Bulgaria.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("f56f5aba-5d69-4f6a-a7af-c89f58cf7a0d"),
                            CityName = "Stara Zagora",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/3/33/Samarsko_Zname_Panorama.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("f8fe8288-0709-4bd6-b8d5-02e7bf5eb9fb"),
                            CityName = "Targovishte",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5c/Targovishte-MainSquare.jpg/1280px-Targovishte-MainSquare.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("20b84a27-dc84-460a-a837-88a77925940d"),
                            CityName = "Varna",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/en/thumb/7/79/Dramatheatrevarna.jpg/1280px-Dramatheatrevarna.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("983dcc64-0973-495c-a297-efe39565d04e"),
                            CityName = "Veliko Tarnovo",
                            CityPicture = "https://traventuria.com/wp-content/uploads/2016/10/veliko-tarnovo-1.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("4e213330-cc7f-4008-9f93-69f343cbe2e3"),
                            CityName = "Vidin",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/7/70/Theater_House_in_Vidin_%2827460729905%29.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("225816e7-c050-47c3-98ae-2c0149929fde"),
                            CityName = "Vratsa",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Vratsa_12.jpg/1024px-Vratsa_12.jpg",
                            StreetName = "Main Street"
                        },
                        new
                        {
                            Id = new Guid("e69431ec-f288-4ed1-85f1-342489bd10fb"),
                            CityName = "Yambol",
                            CityPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9d/YAMBOL_new_center.jpg/1920px-YAMBOL_new_center.jpg",
                            StreetName = "Main Street"
                        });
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Meeting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("FundingDeadline")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("FundsNeeded")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<bool>("IsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Suggestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApplicationUserId")
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

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Suggestions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3e1556ac-3d70-4dba-81a0-b22b7d07e984"),
                            Category = "Transport",
                            Description = "Implement more frequent bus routes during peak hours to reduce congestion.",
                            Priority = "High",
                            Status = "Pending",
                            Title = "Improve Public Transport",
                            UploadedOn = new DateTime(2024, 12, 1, 17, 18, 33, 386, DateTimeKind.Utc).AddTicks(3309)
                        },
                        new
                        {
                            Id = new Guid("ad626249-6ec9-42d5-ad67-a53a5b562cc3"),
                            Category = "Environment",
                            Description = "Renovate the central park by adding new benches, lighting, and a playground area.",
                            Priority = "Medium",
                            Status = "Approved",
                            Title = "Park Renovation",
                            UploadedOn = new DateTime(2024, 12, 1, 17, 18, 33, 386, DateTimeKind.Utc).AddTicks(3315)
                        },
                        new
                        {
                            Id = new Guid("6e69f616-e74e-4804-95ac-5fa313c6f880"),
                            Category = "Waste Management",
                            Description = "Introduce a recycling program and increase the frequency of waste collection.",
                            Priority = "High",
                            Status = "In Review",
                            Title = "Waste Management System",
                            UploadedOn = new DateTime(2024, 12, 1, 17, 18, 33, 386, DateTimeKind.Utc).AddTicks(3318)
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

            modelBuilder.Entity("ApplicationUserMeeting", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("AttendeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UrbanSystem.Data.Models.Meeting", null)
                        .WithMany()
                        .HasForeignKey("MeetingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .HasForeignKey("ApplicationUserId")
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

            modelBuilder.Entity("UrbanSystem.Data.Models.Comment", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", null)
                        .WithMany("Comments")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("UrbanSystem.Data.Models.Suggestion", "Suggestion")
                        .WithMany("Comments")
                        .HasForeignKey("SuggestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Suggestion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Meeting", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", "Organizer")
                        .WithMany("OrganizedMeetings")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Project", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.Location", "Location")
                        .WithMany("Projects")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Suggestion", b =>
                {
                    b.HasOne("UrbanSystem.Data.Models.ApplicationUser", null)
                        .WithMany("Suggestions")
                        .HasForeignKey("ApplicationUserId");
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
                    b.Navigation("Comments");

                    b.Navigation("OrganizedMeetings");

                    b.Navigation("Suggestions");

                    b.Navigation("UsersSuggestions");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Location", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("SuggestionsLocations");
                });

            modelBuilder.Entity("UrbanSystem.Data.Models.Suggestion", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("SuggestionsLocations");

                    b.Navigation("UsersSuggestions");
                });
#pragma warning restore 612, 618
        }
    }
}