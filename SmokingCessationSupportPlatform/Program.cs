using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
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
            builder.Services.AddRazorPages(
                options =>
                {
                    options.Conventions.AllowAnonymousToPage("/Account/Login");
                    options.Conventions.AllowAnonymousToPage("/Account/Register");
                    options.Conventions.AllowAnonymousToPage("/Account/ForgotPassword");
                    options.Conventions.AllowAnonymousToPage("/Account/ResetPassword");
                });
            builder.Services.AddScoped<AccountDAO>();
            builder.Services.AddScoped<UserDAO>();
            builder.Services.AddScoped<CoachDAO>();
            builder.Services.AddScoped<ConversationDAO>();
            builder.Services.AddScoped<MessageDAO>();


            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICoachRepository, CoachRepository>();
            builder.Services.AddScoped<IConservationRepository, ConversationRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IChatService, ChatService>();
            builder.Services.AddScoped<IUserAuthentification, UserAuthentification>();
            builder.Services.AddSignalR();

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyStoreContext")));
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

            app.MapRazorPages();
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
