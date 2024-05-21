using Microsoft.EntityFrameworkCore;
using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Repositories
{
    public class ParticipanteSubastaRepository(SubastasContext context) : BaseRepository<ParticipantesSubasta>(context), IParticipanteSubastaRepository
    {
        public async Task DeleteBySubastaIdAndUserIdAsync(int idSubasta, int idUsuario)
        {
            var participanteSubasta = await context.ParticipantesSubasta.FirstOrDefaultAsync(pS => pS.IdSubasta == idSubasta && pS.IdUsuario == idUsuario);

            if(participanteSubasta != null)
            {
                _dbSet.Remove(participanteSubasta);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"No se encontro el idSubasta {idSubasta} y idusuario {idUsuario}");
            }
        }
    }
}
