using Microsoft.AspNetCore.Authentication.JwtBearer;
using Subastas.Dependencies;
using Subastas.Services.Shared.Logging.DbLoggerObjects;

namespace Subastas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.ConfigureServices(builder.Configuration);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie("Bearer", o =>
                {
                    o.LoginPath = "/Authentication/Login";
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    o.AccessDeniedPath = "/Authentication/Login";
                });
            builder.Logging.AddDbLogger(options =>
            {
                builder.Configuration.GetSection("Logging")
                .GetSection("Database").GetSection("Options").Bind(options);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Authentication/Login");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Subasta}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
