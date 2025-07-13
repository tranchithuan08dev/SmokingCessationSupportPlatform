using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmokingCessationSupportPlatform.Data;
using SmokingCessationSupportPlatform.DataAccessObjects;
using SmokingCessationSupportPlatform.DataAccessObjects.Contexts;
using SmokingCessationSupportPlatform.Repositories;
using SmokingCessationSupportPlatform.Services;

namespace SmokingCessationSupportPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages(
                options =>
                {
                    options.Conventions.AllowAnonymousToPage("/Account/Login");
                    options.Conventions.AllowAnonymousToPage("/Account/Register");
                    options.Conventions.AllowAnonymousToPage("/Account/ForgotPassword");
                    options.Conventions.AllowAnonymousToPage("/Account/ResetPassword");
                });
            builder.Services.AddScoped<AccountDAO>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<ISmokingStatusRepository, SmokingStatusRepository>();
            builder.Services.AddScoped<ISmokingStatusService, SmokingStatusService>();
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            });



            builder.Services.AddDbContext<SmokingCessationSupportPlatformContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

            // Seed data - ensure database is created and seeded
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SmokingCessationSupportPlatformContext>();
                context.Database.EnsureCreated();
                SeedData.Initialize(scope.ServiceProvider);
            }

            app.Run();
        }
    }
}
