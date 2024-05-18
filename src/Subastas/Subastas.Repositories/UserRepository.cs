using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class UserRepository(SubastasContext context) : BaseRepository<Usuario>(context), IUserRepository
    {
    }
}
