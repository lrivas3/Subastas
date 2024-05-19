using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IUserService 
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> Create(Usuario newUsuario);
        Task<Usuario> CreateIfNotExistsAsync(Usuario newUsuario);
        Task<bool> ExistsById(int usuario);
        Task<bool> ExistsByCorreo(string correo);
        Task<Usuario> GetById(int usuario);
        Task<Usuario> GetByCorreo(string correo);
    }
}
