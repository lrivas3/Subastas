using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class UsuarioRolRepository(SubastasContext context) : BaseRepository<UsuarioRol>(context), IUsuarioRolRepository
    {
    }
}
