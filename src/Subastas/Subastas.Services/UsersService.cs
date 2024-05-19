﻿using Subastas.Domain;
using Subastas.Interfaces;

namespace Subastas.Services
{
    public class UsersService(IUserRepository userRepo) : IUserService
    {
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await userRepo.GetAllAsync();
        }

        public async Task<Usuario> Create(Usuario newUsuario)
        {
            if (newUsuario == null)
                return null;

            await userRepo.AddAsync(newUsuario);

            return newUsuario;
        }

        public async Task<Usuario> CreateIfNotExists(Usuario newUsuario)
        {
            if (newUsuario == null)
                return null;

            if (!string.IsNullOrEmpty(newUsuario.CorreoUsuario) && await ExistsByCorreo(newUsuario.CorreoUsuario))
                return await GetByCorreo(newUsuario.CorreoUsuario);

            if (newUsuario.IdUsuario > 0 && await ExistsById(newUsuario.IdUsuario))
                return await GetById(newUsuario.IdUsuario);

            return await Create(newUsuario);
        }

        public async Task<bool> ExistsByCorreo(string correo)
        {
            return await userRepo.ExistsByPredicate(u => u.CorreoUsuario.Equals(correo, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<bool> ExistsById(int usuario)
        {
            return await userRepo.ExistsByPredicate(u => u.IdUsuario.Equals(usuario));
        }

        public async Task<Usuario> GetByCorreo(string correo)
        {
            return await userRepo.GetByPredicate(u => u.CorreoUsuario.Equals(correo, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<Usuario> GetById(int usuario)
        {
            return await userRepo.GetByIdAsync(usuario);
        }
    }
}
