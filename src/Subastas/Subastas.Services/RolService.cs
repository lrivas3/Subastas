using Microsoft.EntityFrameworkCore;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class RolService(IRolRepository rolRepository) : IRolService
    {
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await rolRepository.GetAllAsync();
        }

        public async Task<Role> CreateAsync(Role newRole)
        {
            if (newRole == null)
                return null;

            await rolRepository.AddAsync(newRole);

            return newRole;
        }

        public async Task<Role> CreateIfNotExistsAsync(Role newRole)
        {
            if (newRole == null)
                return null;

            if (newRole.IdRol > 0 && await ExistsByIdAsync(newRole.IdRol))
                return await GetByIdAsync(newRole.IdRol);

            if (!string.IsNullOrEmpty(newRole.NombreRol) && await ExistsByNameAsync(newRole.NombreRol))
                return await GetByNameAsync(newRole.NombreRol);

            return await CreateAsync(newRole);
        }

        public async Task<bool> ExistsByIdAsync(int idRol)
        {
            return await rolRepository.ExistsByPredicate(r => r.IdRol.Equals(idRol));
        }

        public async Task<bool> ExistsByNameAsync(string roleName)
        {
            return await rolRepository.ExistsByPredicate(r => EF.Functions.Like(r.NombreRol, roleName));
        }

        public async Task<Role> GetByIdAsync(int idRol)
        {
            return await rolRepository.GetByIdAsync(idRol);
        }

        public async Task<Role> GetByNameAsync(string roleName)
        {
            return await rolRepository.GetByPredicate(r=> EF.Functions.Like(r.NombreRol, roleName));
        }

        public async Task<bool> DeleteById(int idRol)
        {
            try
            {
                await rolRepository.DeleteAsync(idRol);
                return true;
            }
            catch (Exception)
            {
                // TODO: SAVELOG
                return false;
            }
        }
    }
}
