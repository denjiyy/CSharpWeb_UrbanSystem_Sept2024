using UrbanSystem.Services.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using static UrbanSystem.Web.Infrastructure.Extensions.ApplicationBuilderExtensions;
using UrbanSystem.Web.ViewModels;
using System.Reflection;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Data.Repository;
using UrbanSystem.Web.Infrastructure.Extensions;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Services.Data;

namespace UrbanSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => {
                ConfigureIdentity(options, builder);
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddRoles<IdentityRole<Guid>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
            });

            builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(ISuggestionService).Assembly);

            // Register services
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IMeetingService, MeetingService>();

            // Register repositories
            builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
            builder.Services.AddScoped<IRepository<CommentVote, object>, BaseRepository<CommentVote, object>>();
            builder.Services.AddScoped<IRepository<Meeting, Guid>, BaseRepository<Meeting, Guid>>();
            builder.Services.AddScoped<IRepository<ApplicationUser, Guid>, BaseRepository<ApplicationUser, Guid>>();

            // Add MVC services
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure AutoMapper
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.ApplyMigrations();
            app.Run();
        }

        private static void ConfigureIdentity(IdentityOptions options, WebApplicationBuilder builder)
        {
            options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
            options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            options.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueChars");

            options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            options.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}