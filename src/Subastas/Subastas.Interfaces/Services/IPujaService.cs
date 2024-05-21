using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IPujaService
    {
        Task<IEnumerable<Puja>> GetAllAsync();
        Task<Puja> CreateAsync(Puja newPuja);
        Task<Puja> CreateIfNotExistsAsync(Puja newPuja);
        Task<bool> ExistsByIdAsync(int idPuja);
        Task<bool> ExistsByDatePujaAsync(DateOnly fechaPuja);
        Task<Puja> GetByIdAsync(int idPuja);
        Task<Puja> GetByDatePujaAsync(DateOnly fechaPuja);
        Task<bool> DeleteById(int idPuja);
    }
}
