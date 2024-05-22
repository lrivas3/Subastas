using Microsoft.EntityFrameworkCore;
using Subastas.Domain;
using Subastas.Interfaces;
using Subastas.Repositories;
using System.Linq.Expressions;

namespace Subastas.Services
{
    public class ProductoService(IProductoRepository productoRepository) : IProductoService
    {
        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await productoRepository.GetAllAsync();
        }

        public async Task<Producto> CreateAsync(Producto newProducto)
        {
            if (newProducto == null)
                return null;

            await productoRepository.AddAsync(newProducto);

            return newProducto;
        }

        public async Task<Producto> CreateIfNotExistsAsync(Producto newProducto)
        {
            if (newProducto == null)
                return null;

            if (newProducto.IdProducto > 0 && await ExistsByIdAsync(newProducto.IdProducto))
                return await GetByIdAsync(newProducto.IdProducto);

            if (!string.IsNullOrEmpty(newProducto.NombreProducto) && await ExistsByNameAsync(newProducto.NombreProducto))
                return await GetByNameAsync(newProducto.NombreProducto);

            return await CreateAsync(newProducto);
        }

        public async Task<bool> ExistsByIdAsync(int idProducto)
        {
            return await productoRepository.ExistsByPredicate(p => p.IdProducto.Equals(idProducto));
        }

        public async Task<bool> ExistsByNameAsync(string ProductoName)
        {
            return await productoRepository.ExistsByPredicate(p => EF.Functions.Like(p.NombreProducto, ProductoName));
        }

        public async Task<Producto> GetByIdAsync(int idProducto)
        {
            return await productoRepository.GetByIdAsync(idProducto);
        }

        public async Task<Producto> GetByNameAsync(string ProductoName)
        {
            return await productoRepository.GetByPredicate(p => EF.Functions.Like(p.NombreProducto, ProductoName));
        }

        public async Task<bool> DeleteById(int idProducto)
        {
            try
            {
                await productoRepository.DeleteAsync(idProducto);
                return true;
            }
            catch (Exception)
            {
                // TODO: SAVELOG
                return false;
            }
        }

        public async Task<IEnumerable<Producto>> GetAllByPredicateAsync(Expression<Func<Producto, bool>> predicate)
        {
            return (IEnumerable<Producto>)await productoRepository.GetCollectionByPredicate(predicate);
        }
    }
}
