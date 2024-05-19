using Microsoft.Extensions.DependencyInjection;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Seed.Permisos
{
    public class CreatePermisos : IClassFixture<TestFixture>
    {
        private readonly IPermisoService permisoService;

        public CreatePermisos(TestFixture fixture)
        {
            permisoService = fixture.ServiceProvider.GetService<IPermisoService>();
        }

        [Fact]
        public async Task CreatePermisosSeed()
        {
            var permisos = new List<Permiso>()
            {
                await permisoService.CreateIfNotExistsAsync(new Permiso
                {
                    EstaActivo = true,
                    NombrePermiso = "Login"
                }),
                await permisoService.CreateIfNotExistsAsync(new Permiso
                {
                    EstaActivo = true,
                    NombrePermiso = "Logout"
                }),
                await permisoService.CreateIfNotExistsAsync(new Permiso
                {
                    EstaActivo = true,
                    NombrePermiso = "Create_Subastas"
                }),
                await permisoService.CreateIfNotExistsAsync(new Permiso
                {
                    EstaActivo = true,
                    NombrePermiso = "Add_Subastas"
                }),
                await permisoService.CreateIfNotExistsAsync(new Permiso
                {
                    EstaActivo = true,
                    NombrePermiso = "Edit_Subastas"
                }),
                await permisoService.CreateIfNotExistsAsync(new Permiso
                {
                    EstaActivo = true,
                    NombrePermiso = "Delete_Subastas"
                })
            };

            Assert.True(permisos.Any() && permisos.Count == 6);
        }
    }
}
