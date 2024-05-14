using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Subastas.DAL;

namespace Subastas.Controllers
{
    public class SubastaController(SubastasContext _contex) : Controller
    {
        // GET: SubastaController
        public ActionResult Index()
        {
            var listSubasta = _contex.Subastas.ToList();
            return View(listSubasta);
        }

        // GET: SubastaController/Details/5
        public ActionResult Details(int id)
        {
            var subasta = _contex.Subastas.Include(x=>x.Pujas).ThenInclude(x=>x.IdUsuarioNavigation).Where(x=>x.IdSubasta == id).FirstOrDefault();
            
            return View(subasta);
        }

        // GET: SubastaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubastaController/Create
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

        // GET: SubastaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubastaController/Edit/5
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

        // GET: SubastaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubastaController/Delete/5
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
