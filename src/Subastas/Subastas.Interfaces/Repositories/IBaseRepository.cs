using System.Linq.Expressions;

namespace Subastas.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> ExistsByPredicate(Expression<Func<T, bool>> predicate);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetCollectionByPredicate(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> GetWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetCollectionByPredicateWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}
