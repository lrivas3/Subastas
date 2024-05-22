using Subastas.Domain;
using System.Linq.Expressions;

namespace Subastas.Interfaces
{
    public interface ISubastaService
    {
        Task<IEnumerable<Subasta>> GetAllAsync();
        Task<List<Subasta>> GetCollectionByPredicateWithIncludesAsync(Expression<Func<Subasta, bool>> predicate, params Expression<Func<Subasta, object>>[] includes);
        Task<Subasta> CreateAsync(Subasta newSubasta);
        Task<IEnumerable<Producto>> GetAllByPredicateAsync(Expression<Func<Subasta, bool>> predicate);
        Task<Subasta> CreateIfNotExistsAsync(Subasta newSubasta);
        Task<bool> ExistsByIdAsync(int idSubasta);
        Task<bool> ExistsByTituloSubastaAsync(string tituloSubasta);
        Task<Subasta> GetByIdAsync(int idSubasta);
        Task<Subasta> GetByTituloSubastaAsync(string tituloSubasta);
        Task<bool> DeleteById(int idSubasta);
    }
}
