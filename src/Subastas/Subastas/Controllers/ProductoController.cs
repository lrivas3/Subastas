using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subastas.Domain;
using Subastas.Dto.Producto;
using Subastas.Interfaces;
using Microsoft.AspNetCore.Http;
using Subastas.Interfaces.Services;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var listaProductos = await _productoService.GetAllWithImageUrlsAsync();
                return View(listaProductos);
            }
            catch (Exception e)
            {
                ViewData["Error"] = "An error occurred while trying to retrieve the products.";
                return View();
            }
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
        
        
        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _productoService.GetByIdWithImageUrlAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _productoService.SoftDeleteAsync(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Failed to delete the product.");
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var producto = await _productoService.GetByIdWithImageUrlAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        
        
        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var producto = await _productoService.GetByIdWithImageUrlAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Aquí podrías realizar alguna transformación de datos si es necesario antes de enviarlos a la vista de edición
            return View(producto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductoCreateRequest productoToUpdate, IFormFile imagen)
        {
            if (!ModelState.IsValid)
            {
                var productoToReturn = await _productoService.GetByIdAsync(id);
                if (productoToReturn == null)
                {
                    return NotFound();
                }
                // Map properties manually or use a mapper to transfer data
                productoToReturn.NombreProducto = productoToUpdate.NombreProducto;
                productoToReturn.DescripcionProducto = productoToUpdate.DescripcionProducto;
                productoToReturn.EstaActivo = productoToUpdate.EstaActivo;

                return View(productoToReturn);
            }

            try
            {
                var updatedProducto = await _productoService.UpdateWithImageAsync(id, productoToUpdate, imagen);
                if (updatedProducto == null)
                {
                    throw new Exception("Failed to update the product.");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var productoToReturn = await _productoService.GetByIdAsync(id);
                return View(productoToReturn);
            }
        }
    }
}
