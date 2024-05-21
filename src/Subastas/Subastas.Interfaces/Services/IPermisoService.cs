using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IPermisoService
    {
        Task<IEnumerable<Permiso>> GetAllAsync();
        Task<Permiso> CreateAsync(Permiso newPermiso);
        Task<Permiso> CreateIfNotExistsAsync(Permiso newPermiso);
        Task<bool> ExistsByIdAsync(int idPermiso);
        Task<bool> ExistsByNameAsync(string PermisoName);
        Task<Permiso> GetByIdAsync(int idPermiso);
        Task<Permiso> GetByNameAsync(string PermisoName);
        Task<bool> DeleteById(int idPermiso);
    }
}
