using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class MenuRepository(SubastasContext context) : BaseRepository<Menu>(context), IMenuRepository
    {
    }
}
