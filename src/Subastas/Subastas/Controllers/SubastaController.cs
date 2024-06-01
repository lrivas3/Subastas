using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Subastas.Interfaces;
using Subastas.Interfaces.Services;
using System.Data.Common;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SubastaController(
        IProductoService productoService, 
        ISubastaService subastaService,
        IUserService userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Domain.Subasta> listaSubasta;

                if (User.IsInRole("Admin"))
                {
                    listaSubasta = await subastaService.GetAllAsync();
                }
                else
                {
                    listaSubasta = await subastaService.GetAllByPredicateAsync(s => s.EstaActivo);
                }

                listaSubasta = await subastaService.SetToListProductoWithImgPreloaded(listaSubasta.ToList());
                return View(listaSubasta);
            }
            catch (DbException ex)
            {
                // TODO EVENTLOGEOLOGEOSO PERRASSSSSS
                ModelState.AddModelError(string.Empty, "Error de base de datos: " + ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error: " + ex.Message);
                return View();
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles ="Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["IdProducto"] = new SelectList(await productoService.GetAllByPredicateAsync(p => !p.EstaSubastado && p.EstaActivo && p.Subasta.IdProducto != p.IdProducto), "IdProducto", "NombreProducto");
            return View(new Domain.Subasta());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("TituloSubasta,MontoInicial,FechaSubasta,FechaSubastaFin,IdProducto")] Domain.Subasta subasta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await subastaService.CreateAsync(subasta);
                    return RedirectToAction(nameof(Index));
                }

                ViewData["IdProducto"] = new SelectList(await productoService.GetAllByPredicateAsync(p => !p.EstaSubastado && p.EstaActivo && p.Subasta.IdProducto != p.IdProducto), "IdProducto", "NombreProducto");
            }
            catch (Exception ex)
            {
                // LogError
            }

            return View(subasta);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Details(int id)
        {
            var subasta = new Domain.Subasta();
            try
            {
                subasta = await subastaService.GetSubastaWithPujaAndUsers(id) ?? subasta;
                subasta = await subastaService.SetProductoWithImgPreloaded(subasta);

                if (subasta != null)
                {
                    decimal? maxMontoPuja = subasta.Pujas.Any() ? (decimal?)subasta.Pujas.Max(s => s.MontoPuja) : null;
                    ViewBag.MaxMontoPuja = maxMontoPuja;

                    if (maxMontoPuja.HasValue)
                    {
                        // Obtener el IdUsuario correspondiente al monto de puja más alto
                        int idUsuarioMaxPuja = subasta.Pujas.First(p => p.MontoPuja == maxMontoPuja).IdUsuario;

                        // Obtener el nombre del usuario con el IdUsuario encontrado
                        var usuarioConMaxPuja = await userService.GetByIdAsync(idUsuarioMaxPuja);
                        ViewBag.NombreUsuarioMaxPuja = usuarioConMaxPuja.NombreUsuario;
                    }
                }
                return View(subasta);
            }
            catch (Exception)
            {
                return View(subasta);
            }
        }


        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var subasta = await subastaService.GetByIdAsync(id);
            if (subasta == null)
            {
                 return RedirectToAction(nameof(Index));
            }

            ViewData["IdProducto"] = new SelectList(await productoService.GetAllByPredicateAsync(p => !p.EstaSubastado && p.EstaActivo && (p.Subasta.IdProducto != p.IdProducto || p.Subasta.IdSubasta == id)), "IdProducto", "NombreProducto", subasta.IdProducto);
            return View("Create", subasta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("IdSubasta,TituloSubasta,MontoInicial,FechaSubasta,FechaSubastaFin,IdProducto,EstaActivo,Finalizada")] Domain.Subasta subasta)
        {
            if (id != subasta.IdSubasta)
            {
                 return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await subastaService.UpdateAsync(subasta);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // LogError
                }
            }

            ViewData["IdProducto"] = new SelectList(await productoService.GetAllByPredicateAsync(p => !p.EstaSubastado && p.EstaActivo && (p.Subasta.IdProducto != p.IdProducto || p.Subasta.IdSubasta == id)), "IdProducto", "NombreProducto", subasta.IdProducto);
            return View("Create", subasta);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new { success = false, errorMessage = "" };

            try
            {
                var subasta = await subastaService.GetSubastaWithPujaAndUsers(id);

                var usersPujas = subasta.Pujas
                    .GroupBy(p => p.IdUsuario)
                    .Select(g => new
                    {
                        UserId = g.Key,
                        SumaDePujas = g.Sum(p => p.MontoPuja)
                    })
                    .ToList();

                var deleted = await subastaService.DeleteById(id);

                if (deleted)
                {
                    foreach (var user in usersPujas)
                    {
                        var usuario = await userService.GetUserWithCuentum(user.UserId);

                        usuario.Cuentum.Saldo += user.SumaDePujas;

                        await userService.UpdateAsync(usuario);
                    }

                    result = new { success = true, errorMessage = "" };
                }
                else
                {
                    result = new { success = false, errorMessage = "Parece que hubo un error" };
                }
            }
            catch (Exception ex)
            {
                result = new { success = false, errorMessage = ex.Message };
            }

            return Json(result);
        }
    }
}
