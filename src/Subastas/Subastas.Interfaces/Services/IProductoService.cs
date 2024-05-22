using Subastas.Domain;
using System.Linq.Expressions;

namespace Subastas.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<IEnumerable<Producto>> GetAllByPredicateAsync(Expression<Func<Producto, bool>> predicate);
        Task<Producto> CreateAsync(Producto newProducto);
        Task<Producto> CreateIfNotExistsAsync(Producto newProducto);
        Task<bool> ExistsByIdAsync(int idProducto);
        Task<bool> ExistsByNameAsync(string ProductoName);
        Task<Producto> GetByIdAsync(int idProducto);
        Task<Producto> GetByNameAsync(string ProductoName);
        Task<bool> DeleteById(int idProducto);
    }
}
