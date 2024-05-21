using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class ParticipanteSubastaRepository(SubastasContext context) : BaseRepository<ParticipantesSubasta>(context), IParticipanteSubastaRepository
    {
    }
}
