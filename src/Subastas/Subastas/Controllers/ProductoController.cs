using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subastas.Domain;
using Subastas.Dto.Producto;
using Subastas.Interfaces;
using Microsoft.AspNetCore.Http;
using Subastas.Interfaces.Services;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task<IActionResult> Index()
        {
            var listaProductos = await _productoService.GetAllWithImageUrlsAsync();
            return View(listaProductos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreProducto,DescripcionProducto,EstaActivo")] ProductoCreateRequest productoRequest, IFormFile imagen)
        {
            if (!ModelState.IsValid)
            {
                return View(productoRequest);
            }

            try
            {
                var newProducto = await _productoService.CreateWithImageAsync(productoRequest, imagen);
                if (newProducto == null)
                {
                    throw new Exception("Product creation failed.");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(productoRequest);
            }
        }
    }
}
