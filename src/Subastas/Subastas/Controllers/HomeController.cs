using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Subastas.Managers;

namespace Subastas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Username, string Password)
        {
            // Aqu� debes agregar tu l�gica de autenticaci�n
            // Por ejemplo, podr�as verificar las credenciales contra una base de datos

            if (Username == "admin" && Password == "password") // Ejemplo de credenciales est�ticas
            {
                var token = JwtManager.GenerateToken(Username);

                await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, JwtManager.GetPrincipal(token));

                return RedirectToAction("Privacy");
            }
            else
            {
                // Login fallido, mostrar un mensaje de error
                ViewData["Error"] = "Invalid username or password.";
                return View();
            }
        }

        [Authorize(AuthenticationSchemes= "Bearer", Roles ="admin")]
        public IActionResult Privacy()
        {
            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            }).ToList();

            return View();
        }
    }
}
