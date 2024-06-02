using Subastas.Domain;
using System.Linq.Expressions;

namespace Subastas.Interfaces
{
    public interface ISubastaRepository : IBaseRepository<Subasta>
    {
        Task<List<Subasta>> GetSubastasWithPujaAndUsers(Expression<Func<Subasta, bool>>? predicate = null);
        Task<Subasta> GetSubastaWithPujaAndUsers(int idSubasta);
    }
}
