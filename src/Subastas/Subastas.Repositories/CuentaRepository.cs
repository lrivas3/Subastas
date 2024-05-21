using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class CuentaRepository(SubastasContext context) : BaseRepository<Cuenta>(context), ICuentaRepository
    {
    }
}
