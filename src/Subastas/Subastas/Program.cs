using Microsoft.AspNetCore.Authentication.JwtBearer;
using Subasta.Managers;
using Subastas.Dependencies;
using Subastas.Services.Shared.Logging.DbLoggerObjects;
using System.Globalization;

namespace Subastas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var centralTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

            // Registrar la cultura global para manejar fechas con UTC-6
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-MX");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-MX");

            // Mostrar un ejemplo al iniciar la aplicación
            Console.WriteLine("Hora local (UTC-6): " + TimeZoneInfo.ConvertTime(DateTime.UtcNow, centralTimeZone));

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.ConfigureServices(builder.Configuration);
            
            builder.Services.AddSignalR();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie("Bearer", o =>
                {
                    o.LoginPath = "/Authentication/Login";
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    o.AccessDeniedPath = "/Authentication/Login";
                });
            // Configure DbLogger with connection string from the configuration
            builder.Logging.AddDbLogger(options =>
            {
                options.ConnectionString = builder.Configuration.GetConnectionString("SubastasConnectionString");
                builder.Configuration.GetSection("Logging:Database:Options").Bind(options);
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

            app.MapHub<SubastaHub>("/SubastaHub");

            app.Run();
        }
    }
}
