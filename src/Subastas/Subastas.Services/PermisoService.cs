using Subastas.Domain;
using Subastas.Interfaces;
using Subastas.Repositories;

namespace Subastas.Services
{
    public class PermisoService(IPermisoRepository permisoRepository) : IPermisoService
    {
        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await permisoRepository.GetAllAsync();
        }

        public async Task<Permiso> CreateAsync(Permiso newPermiso)
        {
            if (newPermiso == null)
                return null;

            await permisoRepository.AddAsync(newPermiso);

            return newPermiso;
        }

        public async Task<Permiso> CreateIfNotExistsAsync(Permiso newPermiso)
        {
            if (newPermiso == null)
                return null;

            if (newPermiso.IdPermiso > 0 && await ExistsByIdAsync(newPermiso.IdPermiso))
                return await GetByIdAsync(newPermiso.IdPermiso);

            if (!string.IsNullOrEmpty(newPermiso.NombrePermiso) && await ExistsByNameAsync(newPermiso.NombrePermiso))
                return await GetByNameAsync(newPermiso.NombrePermiso);

            return await CreateAsync(newPermiso);
        }

        public async Task<bool> ExistsByIdAsync(int idPermiso)
        {
            return await permisoRepository.ExistsByPredicate(r => r.IdPermiso.Equals(idPermiso));
        }

        public async Task<bool> ExistsByNameAsync(string permisoName)
        {
            return await permisoRepository.ExistsByPredicate(r => r.NombrePermiso == permisoName);
        }

        public async Task<Permiso> GetByIdAsync(int idPermiso)
        {
            return await permisoRepository.GetByIdAsync(idPermiso);
        }

        public async Task<Permiso> GetByNameAsync(string permisoName)
        {
            return await permisoRepository.GetByPredicate(r => r.NombrePermiso == permisoName);
        }
    }
}
