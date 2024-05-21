using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IParticipanteSubastaService
    {
        Task<IEnumerable<ParticipantesSubasta>> GetAllAsync();
        Task<ParticipantesSubasta> CreateAsync(ParticipantesSubasta newParticipantesSubasta);
        Task<ParticipantesSubasta> CreateIfNotExistsAsync(ParticipantesSubasta newParticipantesSubasta);
        Task<bool> ExistsBySubastaIdAndUserIdAsync(int idSubasta, int idUsuario);
        Task<ParticipantesSubasta> GetBySubastaIdAndUserIdAsync(int idSubasta, int idUsuario);
    }
}
