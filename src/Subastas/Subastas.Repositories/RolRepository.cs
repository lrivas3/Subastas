using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class RolRepository(SubastasContext context) : BaseRepository<Role>(context), IRolRepository
    {

    }
}
