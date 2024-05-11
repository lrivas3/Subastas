using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IUserService 
    {
        Task<Usuario> Create(Usuario newUsuario);
        Task<Usuario> CreateIfNotExists(Usuario newUsuario);
        Task<bool> ExistsById(int usuario);
        Task<bool> ExistsByCorreo(string correo);
        Task<Usuario> GetById(int usuario);
        Task<Usuario> GetByCorreo(string correo);
    }
}
