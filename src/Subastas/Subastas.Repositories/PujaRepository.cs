using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class PujaRepository(SubastasContext context) : BaseRepository<Puja>(context), IPujaRepository
    {
    }
}
