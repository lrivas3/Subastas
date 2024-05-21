using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class ProductoRepository(SubastasContext context) : BaseRepository<Producto>(context), IProductoRepository
    {
    }
}
