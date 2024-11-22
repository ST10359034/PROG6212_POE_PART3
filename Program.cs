using Microsoft.EntityFrameworkCore;
using CMCS.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentValidation;
using System;
using CMCS.Models;
using CMCS.Services;

namespace CMCS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

           builder.Services.AddScoped<IValidator<LectureClaim>, ClaimValidator>();
            // Add session support
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout duration
                options.Cookie.HttpOnly = true; // Make the session cookie HTTP only
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            // Add authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/LecturerLogin/SignIn"; // Redirect to SignIn page for unauthenticated users
                    options.LogoutPath = "/Account/Logout"; // Redirect to logout action
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Set cookie expiration time
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Enable authentication and session middleware
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            // Configure routing


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LectureClaims}/{action=Create}/{id?}");

          

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=HR}/{action=ApprovedClaimsSummary}/{id?}");

          

            app.Run();
        }
    }
}
