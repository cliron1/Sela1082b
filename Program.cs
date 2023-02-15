using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebShortCut.Models;

namespace WebShortCut {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.ConfigureAppConfiguration((hostingContext, config) => {
				config.AddJsonFile("artur.json", optional: true, reloadOnChange: true);
			});

			builder.Services
                .AddAuthentication()
                .AddGoogle(options => {
					options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
					options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
			    }).AddFacebook(options => {
					options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"];
					options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"];
				});

			// Add services to the container.
			builder.Services.AddControllersWithViews();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.ConfigureIdentity(connectionString);

            var app = builder.Build();

            // Seed Data
            await using(var scope = app.Services.CreateAsyncScope()) {
                using var db = scope.ServiceProvider.GetService<AppDbContext>();
                await db!.Database.MigrateAsync();
            }

            // Configure the HTTP request pipeline.
            if(!app.Environment.IsDevelopment()) {
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
            app.MapRazorPages();

            app.Run();
        }
    }
}