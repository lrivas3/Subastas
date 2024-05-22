using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subastas.Domain;
using Subastas.Dto.Producto;
using Subastas.Interfaces;
using Subastas.Dto.S3;
using Subastas.Interfaces.Services;

namespace Subastas.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly IS3StorageService _s3StorageService;
        private readonly IConfiguration _config;
        private readonly string _bucketName;
        private readonly AwsCredentials _awsCredentials;

        public ProductoController(IProductoService productoService, IS3StorageService s3StorageService,
            IConfiguration config)
        {
            _productoService = productoService;
            _s3StorageService = s3StorageService;
            _config = config;
            _bucketName = _config["AwsConfiguration:SubastasBucket"];
            _awsCredentials = new AwsCredentials
            {
                AwsKey = _config["AwsConfiguration:AwsAccessKey"],
                AwsSecretKey = _config["AwsConfiguration:AwsSecretKey"]
            };
        }

        public async Task<IActionResult> Index()
        {
            var listaSubasta = await _productoService.GetAllAsync();
            return View(listaSubasta);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("NombreProducto,DescripcionProducto,EstaActivo")] ProductoCreateRequest productoRequest,
            IFormFile imagen)
        {
            if (!ModelState.IsValid)
            {
                return View(productoRequest);
            }

            // Crear una instancia de Producto desde ProductoCreateRequest
            var producto = new Producto
            {
                NombreProducto = productoRequest.NombreProducto,
                DescripcionProducto = productoRequest.DescripcionProducto,
                EstaActivo = productoRequest.EstaActivo,
                EstaSubastado = false // Valor predeterminado, ya que no se subasta inmediatamente al crear
            };

            if (imagen != null && imagen.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await imagen.CopyToAsync(stream);

                    var s3Object = new S3Object
                    {
                        InputStream = stream,
                        Name = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName),
                        BucketName = _bucketName
                    };

                    var response = await _s3StorageService.UploadObjectAsync(s3Object, _awsCredentials);

                    if (response.StatusCode != 200)
                    {
                        ModelState.AddModelError(string.Empty, "Error uploading image to S3: " + response.Message);
                        return View(productoRequest);
                    }

                    producto.ImagenProducto = s3Object.Name;
                }
            }

            await _productoService.CreateAsync(producto);
            return RedirectToAction(nameof(Index));
        }
    }
}
