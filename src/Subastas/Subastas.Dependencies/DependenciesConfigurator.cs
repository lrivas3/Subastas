﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subasta.Managers;
using Subastas.Database;
using Subastas.Interfaces;
using Subastas.Repositories;
using Subastas.Services;

namespace Subastas.Dependencies
{
    public static class DependenciesConfigurator
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar el contexto de base de datos con la cadena de conexión real
            services.AddDbContext<SubastasContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SubastasConnectionString")));

            // Configurar tus servicios aquí
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEncryptionService, EncryptionService>();

            return services;
        }
    }
}
