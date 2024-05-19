using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class PermisoRepository(SubastasContext context) : BaseRepository<Permiso>(context), IPermisoRepository
    {
    }
}
