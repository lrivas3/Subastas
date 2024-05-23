using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Subastas.Database;
using Subastas.Domain;
using Subastas.Interfaces;
using System.Security.Claims;
namespace Subastas.Controllers
{
    public class PujasController(IPujaService pujaService, IUserService userService, ICuentaService cuentaService) : Controller
    {
        // GET: PujasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PujasController/Details/5
        public ActionResult Details(int id)
        {
            Subastas.Domain.Puja subasta = new Subastas.Domain.Puja();
            subasta.IdSubasta = id;
            subasta.IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return View(subasta);
        }

        // GET: PujasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PujasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSubasta,IdUsuario,MontoPuja")] Puja Puja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Puja.FechaPuja = DateOnly.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                    var user = await userService.GetUserWithCuentum(Puja.IdUsuario);
                    if (user == null)
                    {
                        ViewData["Error"] = "Usuario no encontrado";
                        return View();
                    }
                    if ((Puja.MontoPuja-user.Cuentum.Saldo)>0)
                    {
                        ViewData["Error"] = "Tu saldo es insuficiente";
                        return View();
                    }
                    decimal saldo = user.Cuentum.Saldo - Puja.MontoPuja;

                    var cuenta = await cuentaService.GetByUserIdAsync(Puja.IdUsuario);
                    cuenta.Saldo = saldo;
                    bool update = await cuentaService.UpdateCuenta(cuenta);
                    if (!update) 
                    {
                        ViewData["Error"] = "No se actualizo tu saldo";
                        return View();
                    }
                    await pujaService.CreateAsync(Puja);
                    return RedirectToAction("Details", "Subasta", new { id = Puja.IdSubasta });

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PujasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PujasController/Edit/5
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

        // GET: PujasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PujasController/Delete/5
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
