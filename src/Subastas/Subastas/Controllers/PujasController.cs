using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subastas.Database;
namespace Subastas.Controllers
{
    public class PujasController(SubastasContext _context) : Controller
    {
        // GET: PujasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PujasController/Details/5
        [Authorize(AuthenticationSchemes = "Bearer")]
        public ActionResult Details(int id)
        {
            Subastas.Domain.Puja subasta = new Subastas.Domain.Puja();
            subasta.IdSubasta = id;
            subasta.IdUsuario = int.Parse(User.FindFirst("idUser")?.Value);
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
