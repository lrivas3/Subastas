using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Subastas.Interfaces;
using System.Data.Common;
using Subastas.Interfaces.Services;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SubastaController(
        IProductoService productoService, 
        ISubastaService subastaService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var listaSubasta = await subastaService.GetAllByPredicateAsync(s => s.EstaActivo);
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
            ViewData["IdProducto"] = new SelectList(await productoService.GetAllByPredicateAsync(p => !p.EstaSubastado && p.EstaActivo), "IdProducto", "NombreProducto");
            return View();
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

                ViewData["IdProducto"] = new SelectList(await productoService.GetAllByPredicateAsync(p => !p.EstaSubastado && p.EstaActivo), "IdProducto", "NombreProducto");
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
                    decimal priceNow = subasta.MontoInicial;
                    subasta.MontoInicial = subasta.Pujas.Max(s => s.MontoPuja);
                }
                return View(subasta);
            }
            catch (Exception)
            {
                return View(subasta);
            }
        }
    }
}
