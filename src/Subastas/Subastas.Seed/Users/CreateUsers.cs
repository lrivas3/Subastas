using Microsoft.Extensions.DependencyInjection;
using Subastas.Interfaces;
using Subastas.Domain;
using System.Collections.ObjectModel;

namespace Subastas.Seed.Users
{
    public class CreateUsers : IClassFixture<TestFixture>
    {
        private readonly IUserService userService;
        private readonly IEncryptionService encrypManager;

        public CreateUsers(TestFixture fixture)
        {
            userService = fixture.ServiceProvider.GetService<IUserService>();
            encrypManager = fixture.ServiceProvider.GetService<IEncryptionService>();
        }

        [Fact]
        public async Task CreateUsersSeed()
        {
            var Alexis = await userService.CreateIfNotExistsAsync(new Usuario
            {
                CorreoUsuario = "ia.alexismartinez@ufg.edu.sv",
                NombreUsuario = "Alexis",
                ApellidoUsuario = "Martínez",
                Cuentum = new Cuentum
                {
                    Saldo = 1000.00M,
                    EstaActivo = true
                },
                EstaActivo = true,
                Password = encrypManager.Encrypt("MiContraseñaEnEntronoDev"),
            });

            Assert.NotNull(Alexis);
        }
    }
}
