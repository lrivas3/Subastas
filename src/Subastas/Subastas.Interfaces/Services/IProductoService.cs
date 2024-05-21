using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> CreateAsync(Producto newProducto);
        Task<Producto> CreateIfNotExistsAsync(Producto newProducto);
        Task<bool> ExistsByIdAsync(int idProducto);
        Task<bool> ExistsByNameAsync(string ProductoName);
        Task<Producto> GetByIdAsync(int idProducto);
        Task<Producto> GetByNameAsync(string ProductoName);
        Task<bool> DeleteById(int idProducto);
    }
}
