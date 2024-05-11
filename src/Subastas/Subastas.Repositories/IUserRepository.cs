using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class UserRepository : BaseRepository<Usuario>, IUserRepository
    {
        public UserRepository(SubastasContext context) : base(context){}
    }
}
