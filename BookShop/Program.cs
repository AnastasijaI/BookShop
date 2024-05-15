using BookShop.Data.Services;
using BookShop.Data;
using Microsoft.EntityFrameworkCore;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BookShop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnectionString")
            ));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            builder.Services.AddScoped<IAuthorsService, AuthorsService>();
            builder.Services.AddScoped<IBooksService, BooksService>();            
            builder.Services.AddScoped<IGenresService, GenresService>();
            builder.Services.AddScoped<IReviewsService, ReviewsService>();
            builder.Services.AddScoped<IBooksGenresService, BooksGenresService>();

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                await AppDbInitializer.SeedUsersAndRolesAsync(app);
                //AppDbInitializer.Seed(app);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            AppDbInitializer.Seed(app);

            app.Run();
        }
    }
}
