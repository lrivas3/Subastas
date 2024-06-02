using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class UsuarioRolService(IUsuarioRolRepository usuarioRolRepository) : IUsuarioRolService
    {
        public async Task<List<UsuarioRol>> GetRolesByUserId(int idUsuario)
        {
            return await usuarioRolRepository.GetCollectionByPredicateWithIncludesAsync(uR => uR.IdUsuario == idUsuario,
                uR => uR.IdRolNavigation);
        }
    }
}
