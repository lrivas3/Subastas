using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<Menu> CreateAsync(Menu newMenu);
        Task<Menu> CreateIfNotExistsAsync(Menu newMenu);
        Task<bool> ExistsByIdAsync(int idMenu);
        Task<bool> ExistsByNameAsync(string menuName);
        Task<Menu> GetByIdAsync(int idMenu);
        Task<Menu> GetByNameAsync(string menuName);
    }
}
