using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class CuentaService(ICuentaRepository cuentaRepository) : ICuentaService
    {
        public async Task<IEnumerable<Cuenta>> GetAllAsync()
        {
            return await cuentaRepository.GetAllAsync();
        }

        public async Task<Cuenta> CreateAsync(Cuenta newCuenta)
        {
            if (newCuenta == null)
                return null;

            await cuentaRepository.AddAsync(newCuenta);

            return newCuenta;
        }

        public async Task<Cuenta> CreateIfNotExistsAsync(Cuenta newCuenta)
        {
            if (newCuenta == null)
                return null;

            if (newCuenta.IdCuenta > 0 && await ExistsByIdAsync(newCuenta.IdCuenta))
                return await GetByIdAsync(newCuenta.IdCuenta);

            if (newCuenta.IdUsuario > 0 && await ExistsByUserIdAsync(newCuenta.IdUsuario))
                return await GetByUserIdAsync(newCuenta.IdUsuario);

            return await CreateAsync(newCuenta);
        }

        public async Task<bool> ExistsByIdAsync(int idCuenta)
        {
            return await cuentaRepository.ExistsByPredicate(c => c.IdCuenta.Equals(idCuenta));
        }

        public async Task<Cuenta> GetByIdAsync(int idCuenta)
        {
            return await cuentaRepository.GetByIdAsync(idCuenta);
        }

        public async Task<bool> ExistsByUserIdAsync(int idUsuario)
        {
            return await cuentaRepository.ExistsByPredicate(c => c.IdUsuario.Equals(idUsuario));
        }

        public async Task<Cuenta> GetByUserIdAsync(int idUsuario)
        {
            return await cuentaRepository.GetByPredicate(c => c.IdUsuario.Equals(idUsuario));
        }
    }
}
