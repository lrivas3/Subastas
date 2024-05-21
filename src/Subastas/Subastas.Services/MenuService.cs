using Microsoft.EntityFrameworkCore;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class MenuService(IMenuRepository menuRepository) : IMenuService
    {
        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await menuRepository.GetAllAsync();
        }

        public async Task<Menu> CreateAsync(Menu newMenu)
        {
            if (newMenu == null)
                return null;

            await menuRepository.AddAsync(newMenu);

            return newMenu;
        }

        public async Task<Menu> CreateIfNotExistsAsync(Menu newMenu)
        {
            if (newMenu == null)
                return null;

            if (newMenu.IdMenu > 0 && await ExistsByIdAsync(newMenu.IdMenu))
                return await GetByIdAsync(newMenu.IdMenu);

            if (!string.IsNullOrEmpty(newMenu.NombreMenu) && await ExistsByNameAsync(newMenu.NombreMenu))
                return await GetByNameAsync(newMenu.NombreMenu);

            return await CreateAsync(newMenu);
        }

        public async Task<bool> ExistsByIdAsync(int idMenu)
        {
            return await menuRepository.ExistsByPredicate(m => m.IdMenu.Equals(idMenu));
        }

        public async Task<bool> ExistsByNameAsync(string MenuName)
        {
            return await menuRepository.ExistsByPredicate(m => EF.Functions.Like(m.NombreMenu, MenuName));
        }

        public async Task<Menu> GetByIdAsync(int idMenu)
        {
            return await menuRepository.GetByIdAsync(idMenu);
        }

        public async Task<Menu> GetByNameAsync(string MenuName)
        {
            return await menuRepository.GetByPredicate(m => EF.Functions.Like(m.NombreMenu, MenuName));
        }

        public async Task<bool> DeleteById(int idMenu)
        {
            try
            {
                await menuRepository.DeleteAsync(idMenu);
                return true;
            }
            catch (Exception)
            {
                // TODO: SAVELOG
                return false;
            }
        }
    }
}
