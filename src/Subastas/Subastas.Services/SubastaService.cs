using Microsoft.EntityFrameworkCore;
using Subastas.Domain;
using Subastas.Interfaces;
using Subastas.Interfaces.Services;
using System.Linq.Expressions;

namespace Subastas.Services
{
    public class SubastaService(ISubastaRepository subastaRepository, IProductoService productoService) : ISubastaService
    {
        public async Task<Subasta> CreateAsync(Subasta newSubasta)
        {
            if (newSubasta == null)
                return null;

            await subastaRepository.AddAsync(newSubasta);

            return newSubasta;
        }

        public async Task<Subasta> CreateIfNotExistsAsync(Subasta newSubasta)
        {
            if (newSubasta == null)
                return null;

            if (newSubasta.IdSubasta > 0 && await ExistsByIdAsync(newSubasta.IdSubasta))
                return await GetByIdAsync(newSubasta.IdSubasta);

            if (newSubasta.IdUsuario > 0 && await ExistsByTituloSubastaAsync(newSubasta.TituloSubasta))
                return await GetByTituloSubastaAsync(newSubasta.TituloSubasta);

            return await CreateAsync(newSubasta);
        }

        public async Task<bool> ExistsByIdAsync(int idSubasta)
        {
            return await subastaRepository.ExistsByPredicate(m => m.IdSubasta.Equals(idSubasta));
        }

        public async Task<bool> ExistsByTituloSubastaAsync(string tituloSubasta)
        {
            return await subastaRepository.ExistsByPredicate(m => EF.Functions.Like(m.TituloSubasta, tituloSubasta));
        }

        public async Task<IEnumerable<Subasta>> GetAllAsync()
        {
            return await subastaRepository.GetAllAsync();
        }

        public async Task<Subasta> GetByIdAsync(int idSubasta)
        {
            return await subastaRepository.GetByPredicate(m => m.IdSubasta.Equals(idSubasta));
        }

        public async Task<Subasta> GetByTituloSubastaAsync(string tituloSubasta)
        {
            return await subastaRepository.GetByPredicate(m => EF.Functions.Like(m.TituloSubasta, tituloSubasta));
        }

        public async Task<bool> DeleteById(int idSubasta)
        {
            try
            {
                await subastaRepository.DeleteAsync(idSubasta);
                return true;
            }
            catch (Exception ex)
            {
                // TODO: SAVELOG
                return false;
            }
        }

        public async Task<IEnumerable<Subasta>> GetAllByPredicateAsync(Expression<Func<Subasta, bool>> predicate)
        {
            return await subastaRepository.GetCollectionByPredicate(predicate);
        }

        public async Task<IEnumerable<Subasta>> SetToListProductoWithImgPreloaded(IEnumerable<Subasta> listaSubastas)
        {
            foreach (var item in listaSubastas)
            {
                if(item.IdProducto > 0)
                {
                    item.IdProductoNavigation = await productoService.GetByIdWithImageUrlAsync(item.IdProducto);
                }
            }

            return listaSubastas;
        }

        public async Task<Subasta> SetProductoWithImgPreloaded(Subasta subasta)
        {
            if (subasta != null && subasta.IdProducto > 0)
            {
                subasta.IdProductoNavigation = await productoService.GetByIdWithImageUrlAsync(subasta.IdProducto);
            }

            return subasta;
        }

        public async Task<List<Subasta>> GetCollectionByPredicateWithIncludesAsync(Expression<Func<Subasta, bool>> predicate, params Expression<Func<Subasta, object>>[] includes)
        {
            return await subastaRepository.GetCollectionByPredicateWithIncludesAsync(predicate, includes);
        }

        public async Task<Subasta> GetWithIncludesAsync(Expression<Func<Subasta, bool>> predicate, params Expression<Func<Subasta, object>>[] includes)
        {
            return await subastaRepository.GetWithIncludesAsync(predicate, includes);
        }

        public async Task<List<Subasta>> GetSubastasWithPujaAndUsers(Expression<Func<Subasta, bool>>? predicate = null)
        {
            return await subastaRepository.GetSubastasWithPujaAndUsers(predicate);
        }

        public async Task<Subasta> GetSubastaWithPujaAndUsers(int idSubasta)
        {
            return await subastaRepository.GetSubastaWithPujaAndUsers(idSubasta);
        }
    }
}
