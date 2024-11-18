using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subastas.Domain;
using Subastas.Dto.Producto;
using Subastas.Interfaces;
using Microsoft.AspNetCore.Http;
using Subastas.Interfaces.Services;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using static Amazon.Runtime.Internal.Settings.SettingsCollection;
using System.Drawing.Printing;
using System.Text;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ILogger<ProductoController> _logger;

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
            catch (Exception ex)
            {
                ViewData["Error"] = "An error occurred while trying to retrieve the products.";
                _logger.LogError(ex, "An error occurred while trying to retrieve the products.");
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

        public async Task<IActionResult> Export(string format)
        {
            List<ProductoExportModel> productos;

            try
            {
                productos = (await _productoService.GetAllAsync())
                                .Where(p => p.EstaActivo)
                                .Select(p => new ProductoExportModel
                                {
                                    Nombre = p.NombreProducto,
                                    Descripcion = p.DescripcionProducto,
                                    Activo = p.EstaActivo ? "Sí" : "No",
                                    Subastado = p.EstaSubastado ? "Sí" : "No"
                                })
                                .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                
                return StatusCode(500, "Error al obtener los productos");
            }

            if (productos == null || !productos.Any())
            {
                return NotFound("No hay productos para exportar.");
            }

            if (format == "xlsx")
            {
                byte[] fileContents;
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Productos");
                        worksheet.Cell(1, 1).Value = "Nombre";
                        worksheet.Cell(1, 2).Value = "Descripción";
                        worksheet.Cell(1, 3).Value = "Activo";
                        worksheet.Cell(1, 4).Value = "Subastado";

                        int row = 2;
                        foreach (var producto in productos)
                        {
                            worksheet.Cell(row, 1).Value = producto.Nombre;
                            worksheet.Cell(row, 2).Value = producto.Descripcion;
                            worksheet.Cell(row, 3).Value = producto.Activo;
                            worksheet.Cell(row, 4).Value = producto.Subastado;
                            row++;
                        }

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            fileContents = stream.ToArray();
                        }
                    }

                    return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "productos.xlsx");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al generar el archivo Excel: {ex.Message}");
                    
                    return StatusCode(500, "Error al generar el archivo Excel");
                }
            }
            else if (format == "json")
            {
                try
                {
                    var json = JsonConvert.SerializeObject(productos, Newtonsoft.Json.Formatting.Indented);
                    var jsonBytes = Encoding.UTF8.GetBytes(json);

                    return File(jsonBytes, "application/json", "productos.json");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al generar el archivo JSON: {ex.Message}");
                    
                    return StatusCode(500, "Error al generar el archivo JSON");
                }
            }

            return NotFound();
        }

        public class ProductoExportModel
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Activo { get; set; }
            public string Subastado { get; set; }
        }

    }
}