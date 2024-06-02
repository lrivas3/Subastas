using Subastas.Domain;

namespace Subastas.Interfaces
{
    public interface IUsuarioRolService
    {
        Task<List<UsuarioRol>> GetRolesByUserId(int idUsuario);
    }
}
