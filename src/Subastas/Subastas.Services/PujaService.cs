using Microsoft.EntityFrameworkCore;
using Subastas.Domain;
using Subastas.Interfaces;
using Subastas.Repositories;

namespace Subastas.Services
{
    public class PujaService(IPujaRepository pujaRepository) : IPujaService
    {
        public async Task<Puja> CreateAsync(Puja newPuja)
        {
            if (newPuja == null)
                return null;

            await pujaRepository.AddAsync(newPuja);

            return newPuja;
        }

        public async Task<Puja> CreateIfNotExistsAsync(Puja newPuja)
        {
            if (newPuja == null)
                return null;

            if (newPuja.IdSubasta > 0 && await ExistsByIdAsync(newPuja.IdSubasta))
                return await GetByIdAsync(newPuja.IdSubasta);

            if (newPuja.IdUsuario > 0 && await ExistsByDatePujaAsync(newPuja.FechaPuja))
                return await GetByDatePujaAsync(newPuja.FechaPuja);

            return await CreateAsync(newPuja);
        }

        public async Task<bool> ExistsByIdAsync(int idPuja)
        {
            return await pujaRepository.ExistsByPredicate(m => m.IdPuja.Equals(idPuja));
        }

        public async Task<bool> ExistsByDatePujaAsync(DateTime fechaPuja)
        {
            return await pujaRepository.ExistsByPredicate(m => m.FechaPuja == fechaPuja);
        }

        public async Task<IEnumerable<Puja>> GetAllAsync()
        {
            return await pujaRepository.GetAllAsync();
        }

        public async Task<Puja> GetByDatePujaAsync(DateTime fechaPuja)
        {
            return await pujaRepository.GetByPredicate(m => m.FechaPuja == fechaPuja);
        }

        public async Task<Puja> GetByIdAsync(int idPuja)
        {
            return await pujaRepository.GetByPredicate(m => m.IdPuja.Equals(idPuja));
        }

        public async Task<bool> DeleteById(int idPuja)
        {
            try
            {
                await pujaRepository.DeleteAsync(idPuja);
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
