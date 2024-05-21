using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subastas.Managers;
using Subastas.Models;
using System.Diagnostics;

namespace Subastas.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
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

                return RedirectToAction("Index","Subasta");
            }
            else
            {
                // Login fallido, mostrar un mensaje de error
                ViewData["Error"] = "Invalid username or password.";
                return View();
            }
        }

        [HttpPost]
        [Route("logout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            ViewData["exit"] = "Se cerro la sesión.";
            return View("Index");
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
