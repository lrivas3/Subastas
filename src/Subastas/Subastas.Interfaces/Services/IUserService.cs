using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> CreateAsync(Usuario newUsuario);
        Task<Usuario> CreateIfNotExistsAsync(Usuario newUsuario);
        Task<bool> ExistsByIdAsync(int idUsuario);
        Task<bool> ExistsByCorreoAsync(string correo);
        Task<Usuario> GetByIdAsync(int idUsuario);
        Task<Usuario> GetByCorreoAsync(string correo);
        Task<bool> DeleteById(int idUsuario);
        Task<Usuario> GetUserAndRoleByLogin(string correo, string password);
        Task<Usuario> GetUserWithCuentum(int idUser);
        Task UpdateAsync(Usuario usuario);
    }
}
