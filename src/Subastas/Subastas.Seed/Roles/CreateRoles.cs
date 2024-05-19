using Microsoft.Extensions.DependencyInjection;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Seed.Roles
{
    public class CreateRoles : IClassFixture<TestFixture>
    {
        private readonly IRolService rolService;

        public CreateRoles(TestFixture fixture)
        {
            rolService = fixture.ServiceProvider.GetService<IRolService>();
        }

        [Fact]
        public async Task CreateRolesSeed()
        {
            var admin = await rolService.CreateIfNotExistsAsync(new Role
            {
                EstaActivo = true,
                NombreRol = "Admin"
            });

            var user = await rolService.CreateIfNotExistsAsync(new Role
            {
                EstaActivo = true,
                NombreRol = "User"
            });

            var roles = new Role[] { admin, user }; 

            Assert.True(roles.Any() && roles.Length == 2);
        }
    }
}
