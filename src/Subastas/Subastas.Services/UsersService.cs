using Microsoft.EntityFrameworkCore;
using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class UsersService(IUserRepository userRepo, IEncryptionService encrypManager, IUsuarioRolService usuarioRolService) : IUserService
    {
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await userRepo.GetAllAsync();
        }

        public async Task<Usuario> CreateAsync(Usuario newUsuario)
        {
            if (newUsuario == null)
                return null;


            await userRepo.AddAsync(newUsuario);

            return newUsuario;
        }

        public async Task<Usuario> CreateIfNotExistsAsync(Usuario newUsuario)
        {
            if (newUsuario == null)
                return null;

            if (!string.IsNullOrEmpty(newUsuario.CorreoUsuario) && await ExistsByCorreoAsync(newUsuario.CorreoUsuario))
                return await GetByCorreoAsync(newUsuario.CorreoUsuario);

            if (newUsuario.IdUsuario > 0 && await ExistsByIdAsync(newUsuario.IdUsuario))
                return await GetByIdAsync(newUsuario.IdUsuario);

            return await CreateAsync(newUsuario);
        }

        public async Task<bool> ExistsByCorreoAsync(string correo)
        {
            return await userRepo.ExistsByPredicate(u => EF.Functions.Like(u.CorreoUsuario, correo));
        }

        public async Task<bool> ExistsByIdAsync(int usuario)
        {
            return await userRepo.ExistsByPredicate(u => u.IdUsuario.Equals(usuario));
        }

        public async Task<Usuario> GetByCorreoAsync(string correo)
        {
            return await userRepo.GetByPredicate(u => EF.Functions.Like(u.CorreoUsuario, correo));
        }

        public async Task<Usuario> GetByIdAsync(int usuario)
        {
            return await userRepo.GetByIdAsync(usuario);
        }

        public async Task<bool> DeleteById(int idUsuario)
        {
            try
            {
                await userRepo.DeleteAsync(idUsuario);
                return true;
            }
            catch (Exception)
            {
                // TODO: SAVELOG
                return false;
            }
        }

        public async Task<Usuario> GetUserAndRoleByLogin(string correo, string password)
        {
            var user = await userRepo.GetByPredicate(u
                => EF.Functions.Like(u.CorreoUsuario, correo)
                && EF.Functions.Like(u.Password, encrypManager.Encrypt(password)));

            if (user == null || user.IdUsuario <= 0)
                return null;

            user.UsuarioRols = await usuarioRolService.GetRolesByUserId(user.IdUsuario);

            return user;
        }

        public async Task<Usuario> GetUserWithCuentum(int idUser)
        {
            var user = await userRepo.GetUserWithCuentum(idUser);

            if (user == null || user.IdUsuario <= 0)
                return null;

            user.UsuarioRols = await usuarioRolService.GetRolesByUserId(user.IdUsuario);

            return user;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await userRepo.UpdateAsync(usuario);
        }
    }
}
