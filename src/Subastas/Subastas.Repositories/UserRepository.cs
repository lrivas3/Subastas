using Microsoft.EntityFrameworkCore;
using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class UserRepository(SubastasContext context) : BaseRepository<Usuario>(context), IUserRepository
    {
        public async Task<Usuario> GetUserWithCuentum(int id)
        {
            var user = context.Usuarios.Include(u=>u.Cuentum).Where(u=>u.IdUsuario == id).SingleOrDefault();
            return user;
        }
    }
}
