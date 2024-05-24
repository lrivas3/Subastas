using Microsoft.EntityFrameworkCore;
using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;
using System.Linq.Expressions;

namespace Subastas.Repositories
{
    public class SubastaRepository(SubastasContext context) : BaseRepository<Subasta>(context), ISubastaRepository
    {
        public async Task<List<Subasta>> GetSubastasWithPujaAndUsers(Expression<Func<Subasta, bool>>? predicate = null)
        {
            var query = context.Subastas.Include(s => s.Pujas).ThenInclude(s => s.IdUsuarioNavigation);

            if (predicate != null)
                return query.Where(predicate).ToList();

            return query.ToList();
        }

        public async Task<Subasta> GetSubastaWithPujaAndUsers(int idSubasta)
        {
            return await context.Subastas.Include(x => x.IdProductoNavigation).Include(x=>x.ParticipantesSubasta).Include(s => s.Pujas).ThenInclude(s => s.IdUsuarioNavigation).FirstOrDefaultAsync(s => s.IdSubasta == idSubasta);
        }
    }
}
