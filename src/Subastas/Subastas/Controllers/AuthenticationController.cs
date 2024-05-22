using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Subastas.Managers;
using Microsoft.AspNetCore.Authorization;
using Subastas.Interfaces;

namespace Subastas.Controllers
{
    public class AuthenticationController(IUserService userService) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await userService.GetUserAndRoleByLogin(email, password);

            if (user != null)
            {
                var roles = user.UsuarioRols.Select(x => x.IdRolNavigation.NombreRol).ToList();
                
                var token = JwtManager.GenerateToken(user.IdUsuario.ToString(), email, roles, user.NombreUsuario, user.ApellidoUsuario);

                await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, JwtManager.GetPrincipal(token));

                return RedirectToAction("Index", "Subasta");
            }
            else
            {
                // Login fallido, mostrar un mensaje de error
                ViewData["Error"] = "Correo o contraseña incorrecta.";
                return View();
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            ViewData["Success"] = "Se cerro la sesión.";
            return View("Login");
        }
    }
}
