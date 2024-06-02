using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IParticipanteSubastaRepository : IBaseRepository<ParticipantesSubasta>
    {
        Task DeleteBySubastaIdAndUserIdAsync(int idSubasta, int idUsuario);
    }
}
