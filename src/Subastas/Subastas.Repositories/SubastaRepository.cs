using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class SubastaRepository(SubastasContext context) : BaseRepository<Subasta>(context), ISubastaRepository
    {
    }
}
