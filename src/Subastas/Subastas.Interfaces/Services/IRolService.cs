using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> CreateAsync(Role newRole);
        Task<Role> CreateIfNotExistsAsync(Role newRole);
        Task<bool> ExistsByIdAsync(int idRol);
        Task<bool> ExistsByNameAsync(string roleName);
        Task<Role> GetByIdAsync(int idRol);
        Task<Role> GetByNameAsync(string roleName);
        Task<bool> DeleteById(int idRol);
    }
}
