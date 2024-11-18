using Subastas.Domain;
using System.Linq.Expressions;

namespace Subastas.Interfaces
{
    public interface IPujaService
    {
        Task<IEnumerable<Puja>> GetAllAsync();
        Task<Puja> CreateAsync(Puja newPuja);
        Task<Puja> CreateIfNotExistsAsync(Puja newPuja);
        Task<bool> ExistsByIdAsync(int idPuja);
        Task<bool> ExistsByDatePujaAsync(DateTime fechaPuja);
        Task<Puja> GetByIdAsync(int idPuja);
        Task<Puja> GetByDatePujaAsync(DateTime fechaPuja);
        Task<bool> DeleteById(int idPuja);
        Task<IEnumerable<Puja>> GetAllByPredicateAsync(Expression<Func<Puja, bool>> predicate);
    }
}
