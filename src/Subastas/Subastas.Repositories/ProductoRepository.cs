using Microsoft.EntityFrameworkCore;
using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;
using Subastas.Interfaces.Repositories;

namespace Subastas.Repositories
{
    public class ProductoRepository(SubastasContext context) : BaseRepository<Producto>(context), IProductoRepository
    {
        public async Task SoftDeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                producto.EstaActivo = false;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontro el id {id}");
            }
        }
        
        
        public async Task<IEnumerable<Producto>> GetAllActiveAsync()
        {
            return await _context.Productos.Where(x => x.EstaActivo).ToListAsync();
        }
    }
}
