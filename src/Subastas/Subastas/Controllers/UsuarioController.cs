using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subastas.Database;
using Subastas.Domain;
using Subastas.Dto;
using Subastas.Interfaces;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class UsuarioController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptManager;
        public UsuarioController(SubastasContext context, IUserService userService,  IEncryptionService encrypManager)
        {
            _userService = userService;
            _encryptManager = encrypManager;
        }
        // GET: UsuarioController
        public async Task<IActionResult> Index()
        {
            var listaUsuario = await _userService.GetAllAsync();
            return View(listaUsuario);
        }

        // GET: UsuarioController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var usuario = await _userService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try {
                    await _userService.CreateIfNotExistsAsync(usuario);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("Error", "Error al crear el usuario");
                }
            }
            return View(usuario);
        }

        // GET: UsuarioController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var usuario = await _userService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var usuarioEditDto = new UsuarioEditDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                ApellidoUsuario = usuario.ApellidoUsuario,
                CorreoUsuario = usuario.CorreoUsuario,
                EstaActivo = usuario.EstaActivo,
            };

            return View(usuarioEditDto); 
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UsuarioEditDto usuarioForm)
        {
            
            if (string.IsNullOrEmpty(usuarioForm.NewPassword))
            {
                ModelState.Remove("NewPassword");
                ModelState.Remove("ConfirmPassword");
            }
            var usuarioDb = await _userService.GetByIdAsync(id);
            if (usuarioDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                usuarioDb.NombreUsuario = usuarioForm.NombreUsuario;
                usuarioDb.ApellidoUsuario = usuarioForm.ApellidoUsuario;
                usuarioDb.CorreoUsuario = usuarioForm.CorreoUsuario;
                usuarioDb.EstaActivo = usuarioForm.EstaActivo;

                if (!string.IsNullOrEmpty(usuarioForm.NewPassword))
                {
                    if (usuarioForm.NewPassword == usuarioForm.ConfirmPassword)
                    {
                        usuarioDb.Password = _encryptManager.Encrypt(usuarioForm.NewPassword);
                    }
                    else
                    {
                        ModelState.AddModelError("", "La confirmación de la contraseña no coincide.");
                        return View(usuarioForm);
                    }
                }

                try
                {
                    await _userService.UpdateAsync(usuarioDb);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar el usuario: " + ex.Message);
                }
            }

            return View(usuarioForm);
        }
        // GET: UsuarioController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _userService.GetByIdAsync(id);

            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var borrado = await _userService.DeleteById(id);

                if (borrado)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Error"] = "Error al eliminar el usuario";
                return View();
            }
            catch
            {
                ViewData["Error"] = "Error al eliminar el usuario";
                return View();
            }
        }
    }
}
