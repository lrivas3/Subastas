using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface ISubastaService
    {
        Task<IEnumerable<Subasta>> GetAllAsync();
        Task<Subasta> CreateAsync(Subasta newSubasta);
        Task<Subasta> CreateIfNotExistsAsync(Subasta newSubasta);
        Task<bool> ExistsByIdAsync(int idSubasta);
        Task<bool> ExistsByTituloSubastaAsync(string tituloSubasta);
        Task<Subasta> GetByIdAsync(int idSubasta);
        Task<Subasta> GetByTituloSubastaAsync(string tituloSubasta);
        Task<bool> DeleteById(int idSubasta);
    }
}
