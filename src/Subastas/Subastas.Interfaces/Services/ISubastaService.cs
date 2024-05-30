using Subastas.Domain;
using System.Linq.Expressions;

namespace Subastas.Interfaces
{
    public interface ISubastaService
    {
        Task UpdateAsync(Subasta subasta);
        Task<IEnumerable<Subasta>> GetAllAsync();
        Task<List<Subasta>> GetCollectionByPredicateWithIncludesAsync(Expression<Func<Subasta, bool>> predicate, params Expression<Func<Subasta, object>>[] includes);
        Task<Subasta> CreateAsync(Subasta newSubasta);
        Task<IEnumerable<Subasta>> GetAllByPredicateAsync(Expression<Func<Subasta, bool>> predicate);
        Task<IEnumerable<Subasta>> SetToListProductoWithImgPreloaded(IEnumerable<Subasta> listaSubastas);
        Task<Subasta> SetProductoWithImgPreloaded(Subasta subasta);
        Task<Subasta> CreateIfNotExistsAsync(Subasta newSubasta);
        Task<bool> ExistsByIdAsync(int idSubasta);
        Task<bool> ExistsByTituloSubastaAsync(string tituloSubasta);
        Task<Subasta> GetByIdAsync(int idSubasta);
        Task<Subasta> GetByTituloSubastaAsync(string tituloSubasta);
        Task<bool> DeleteById(int idSubasta);
        Task<Subasta> GetWithIncludesAsync(Expression<Func<Subasta, bool>> predicate, params Expression<Func<Subasta, object>>[] includes);
        Task<List<Subasta>> GetSubastasWithPujaAndUsers(Expression<Func<Subasta, bool>>? predicate = null);
        Task<Subasta> GetSubastaWithPujaAndUsers(int idSubasta);
    }
}
