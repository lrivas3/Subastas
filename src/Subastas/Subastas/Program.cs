using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Subastas.Database;

namespace Subastas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddDbContext<SubastasContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SubastasConnectionString")));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie("Bearer", o =>
                {
                    o.LoginPath = "/Home/Index";
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    o.AccessDeniedPath = "/Home/Index";
                });

            var app = builder.Build();

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
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
