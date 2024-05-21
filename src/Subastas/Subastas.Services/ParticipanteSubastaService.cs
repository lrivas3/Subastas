using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class ParticipanteSubastaService(IParticipanteSubastaRepository participanteSubastaRepository) : IParticipanteSubastaService
    {
        public async Task<IEnumerable<ParticipantesSubasta>> GetAllAsync()
        {
            return await participanteSubastaRepository.GetAllAsync();
        }

        public async Task<ParticipantesSubasta> CreateAsync(ParticipantesSubasta newParticipantesSubasta)
        {
            if (newParticipantesSubasta == null)
                return null;

            await participanteSubastaRepository.AddAsync(newParticipantesSubasta);

            return newParticipantesSubasta;
        }

        public async Task<ParticipantesSubasta> CreateIfNotExistsAsync(ParticipantesSubasta newParticipantesSubasta)
        {
            if (newParticipantesSubasta == null)
                return null;

            if (newParticipantesSubasta.IdUsuario > 0 && newParticipantesSubasta.IdSubasta > 0 
                && await ExistsBySubastaIdAndUserIdAsync(newParticipantesSubasta.IdSubasta, newParticipantesSubasta.IdUsuario))
                return await GetBySubastaIdAndUserIdAsync(newParticipantesSubasta.IdSubasta, newParticipantesSubasta.IdUsuario);

            return await CreateAsync(newParticipantesSubasta);
        }

        public async Task<bool> ExistsBySubastaIdAndUserIdAsync(int idSubasta, int idUsuario)
        {
            return await participanteSubastaRepository.ExistsByPredicate(pS => pS.IdSubasta == idSubasta && pS.IdUsuario == idUsuario);
        }

        public async Task<ParticipantesSubasta> GetBySubastaIdAndUserIdAsync(int idSubasta, int idUsuario)
        {
            return await participanteSubastaRepository.GetByPredicate(pS => pS.IdSubasta == idSubasta && pS.IdUsuario == idUsuario);
        }
    }
}
