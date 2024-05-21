using Subastas.Domain;

namespace Subastas.Interfaces.Services
{
    public interface ICuentaService
    {
        Task<IEnumerable<Cuenta>> GetAllAsync();
        Task<Cuenta> CreateAsync(Cuenta newCuenta);
        Task<Cuenta> CreateIfNotExistsAsync(Cuenta newCuenta);
        Task<bool> ExistsByIdAsync(int idCuenta);
        Task<bool> ExistsByUserIdAsync(int idUsuario);
        Task<Cuenta> GetByIdAsync(int idCuenta);
        Task<Cuenta> GetByUserIdAsync(int idUsuario);
    }
}
