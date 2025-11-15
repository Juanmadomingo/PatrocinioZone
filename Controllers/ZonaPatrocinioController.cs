using Microsoft.AspNetCore.Mvc;
using PatrocinioZoneProyecto.Data;
using PatrocinioZoneProyecto.Models;

namespace PatrocinioZoneProyecto.Controllers
{
    public class ZonaPatrocinioController : Controller
    {
        private readonly AppDbContext _context;

        public ZonaPatrocinioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ZonaPatrocinio
        public IActionResult Index()
        {
            var zonas = _context.ZonasPatrocinio.ToList();
            return View(zonas);
        }

        // GET: ZonaPatrocinio/Details/5
        public IActionResult Details(int id)
        {
            var zona = _context.ZonasPatrocinio.FirstOrDefault(z => z.Id == id);
            if (zona == null)
                return NotFound();

            return View(zona);
        }

        // GET: ZonaPatrocinio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZonaPatrocinio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ZonaPatrocinio zona)
        {
            if (ModelState.IsValid)
            {
                _context.ZonasPatrocinio.Add(zona);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(zona);
        }

        // GET: ZonaPatrocinio/Edit/5
        public IActionResult Edit(int id)
        {
            var zona = _context.ZonasPatrocinio.Find(id);
            if (zona == null)
                return NotFound();

            return View(zona);
        }

        // POST: ZonaPatrocinio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ZonaPatrocinio zona)
        {
            if (id != zona.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(zona);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(zona);
        }

        // GET: ZonaPatrocinio/Delete/5
        public IActionResult Delete(int id)
        {
            var zona = _context.ZonasPatrocinio.FirstOrDefault(z => z.Id == id);
            if (zona == null)
                return NotFound();

            return View(zona);
        }

        // POST: ZonaPatrocinio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var zona = _context.ZonasPatrocinio.Find(id);
            if (zona != null)
            {
                _context.ZonasPatrocinio.Remove(zona);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
