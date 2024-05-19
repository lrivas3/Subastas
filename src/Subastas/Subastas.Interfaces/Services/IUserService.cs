using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> CreateAsync(Usuario newUsuario);
        Task<Usuario> CreateIfNotExistsAsync(Usuario newUsuario);
        Task<bool> ExistsByIdAsync(int usuario);
        Task<bool> ExistsByCorreoAsync(string correo);
        Task<Usuario> GetByIdAsync(int usuario);
        Task<Usuario> GetByCorreoAsync(string correo);
    }
}
