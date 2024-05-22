using Subastas.Domain;

namespace Subastas.Interfaces.Repositories
{
    public interface IProductoRepository : IBaseRepository<Producto>
    {
        Task SoftDeleteAsync(int id);
        Task<IEnumerable<Producto>> GetAllActiveAsync();
    }
}
