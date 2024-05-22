using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subastas.Domain;
using Subastas.Interfaces;
using Subastas.Services;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductoController(IProductoService productoService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var listaSubasta = await productoService.GetAllAsync();
            return View(listaSubasta);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreProducto,DescripcionProducto,ImagenProducto,EstaActivo")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.EstaSubastado = false;
                await productoService.CreateAsync(producto);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }
    }
}
