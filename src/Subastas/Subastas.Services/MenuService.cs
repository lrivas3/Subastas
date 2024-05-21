using Subastas.Domain;
using Subastas.Interfaces;
using Subastas.Repositories;

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
            return await menuRepository.ExistsByPredicate(r => r.IdMenu.Equals(idMenu));
        }

        public async Task<bool> ExistsByNameAsync(string MenuName)
        {
            return await menuRepository.ExistsByPredicate(r => r.NombreMenu == MenuName);
        }

        public async Task<Menu> GetByIdAsync(int idMenu)
        {
            return await menuRepository.GetByIdAsync(idMenu);
        }

        public async Task<Menu> GetByNameAsync(string MenuName)
        {
            return await menuRepository.GetByPredicate(r => r.NombreMenu == MenuName);
        }
    }
}
