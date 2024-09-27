using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWorkpiece.Controllers
{
    public class StatController : Controller
    {
        // GET: StatController
        public ActionResult Index()
        {
            return View();
        }

        // GET: StatController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StatController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatController/Create
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

        // GET: StatController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StatController/Edit/5
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

        // GET: StatController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StatController/Delete/5
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
