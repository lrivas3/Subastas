using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subastas.Database;
using Subastas.Interfaces;
using Subastas.Repositories;
using Subastas.Services;

namespace Subastas.Seed
{
    public class TestFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public TestFixture()
        {
            var services = new ServiceCollection();

            // Cargar la configuración
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<TestFixture>()
                .Build();

            // Configura el contexto de base de datos con la cadena de conexión real
            services.AddDbContext<SubastasContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SubastasConnectionString")));

            // Configura tus servicios aquí
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IUserRepository, UserRepository>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
