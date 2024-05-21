using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class SubastaService(ISubastaRepository subastaRepository) : ISubastasService
    {
        public async Task<Subasta> CreateAsync(Subasta newSubasta)
        {
            if (newSubasta == null)
                return null;

            await subastaRepository.AddAsync(newSubasta);

            return newSubasta;
        }

        public async Task<Subasta> CreateIfNotExistsAsync(Subasta newSubasta)
        {
            if (newSubasta == null)
                return null;

            if (newSubasta.IdSubasta > 0 && await ExistsByIdAsync(newSubasta.IdSubasta))
                return await GetByIdAsync(newSubasta.IdSubasta);

            if (newSubasta.IdUsuario > 0 && await ExistsByTituloSubastaAsync(newSubasta.TituloSubasta))
                return await GetByTituloSubastaAsync(newSubasta.TituloSubasta);

            return await CreateAsync(newSubasta);
        }

        public async Task<bool> ExistsByIdAsync(int idSubasta)
        {
            return await subastaRepository.ExistsByPredicate(m => m.IdSubasta.Equals(idSubasta));
        }

        public async Task<bool> ExistsByTituloSubastaAsync(string tituloSubasta)
        {
            return await subastaRepository.ExistsByPredicate(m => m.TituloSubasta.Equals(tituloSubasta));
        }

        public async Task<IEnumerable<Subasta>> GetAllAsync()
        {
            return await subastaRepository.GetAllAsync();
        }

        public async Task<Subasta> GetByIdAsync(int idSubasta)
        {
            return await subastaRepository.GetByPredicate(m => m.IdSubasta.Equals(idSubasta));
        }

        public async Task<Subasta> GetByTituloSubastaAsync(string tituloSubasta)
        {
            return await subastaRepository.GetByPredicate(m => m.TituloSubasta.Equals(tituloSubasta));
        }
    }
}
