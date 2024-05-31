using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        public async Task<IActionResult> Create([FromBody] Puja puja)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    puja.FechaPuja = DateTime.Now;
                    var user = await userService.GetUserWithCuentum(puja.IdUsuario);
                    if (user == null)
                    {
                        return Json(new { success = false, message = "Usuario no encontrado" });
                    }
                    if ((puja.MontoPuja - user.Cuentum.Saldo) > 0)
                    {
                        return Json(new { success = false, message = "Tu saldo es insuficiente" });
                    }
                    decimal saldo = user.Cuentum.Saldo - puja.MontoPuja;

                    var cuenta = await cuentaService.GetByUserIdAsync(puja.IdUsuario);
                    cuenta.Saldo = saldo;
                    bool update = await cuentaService.UpdateCuenta(cuenta);
                    if (!update)
                    {
                        return Json(new { success = false, message = "No se actualizó tu saldo" });
                    }
                    await pujaService.CreateAsync(puja);

                    return Json(new { success = true, message = "Puja realizada con éxito", MontoPuja = puja.MontoPuja });
                }
                return Json(new { success = false, message = "Modelo inválido" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
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
