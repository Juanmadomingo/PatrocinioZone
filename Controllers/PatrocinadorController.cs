using Microsoft.AspNetCore.Mvc;
using PatrocinioZoneProyecto.Data;
using PatrocinioZoneProyecto.Models;

namespace PatrocinioZoneProyecto.Controllers
{
    public class PatrocinadorController : Controller
    {
        private readonly AppDbContext _context;

        public PatrocinadorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Patrocinador
        public IActionResult Index()
        {
            var patrocinadores = _context.Patrocinadores.ToList();
            return View(patrocinadores);
        }

        // GET: Patrocinador/Details/5
        public IActionResult Details(int id)
        {
            var patrocinador = _context.Patrocinadores.FirstOrDefault(p => p.Id == id);
            if (patrocinador == null)
                return NotFound();

            return View(patrocinador);
        }

        // GET: Patrocinador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patrocinador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                _context.Patrocinadores.Add(patrocinador);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(patrocinador);
        }

        // GET: Patrocinador/Edit/5
        public IActionResult Edit(int id)
        {
            var patrocinador = _context.Patrocinadores.Find(id);
            if (patrocinador == null)
                return NotFound();

            return View(patrocinador);
        }

        // POST: Patrocinador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Patrocinador patrocinador)
        {
            if (id != patrocinador.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(patrocinador);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(patrocinador);
        }

        // GET: Patrocinador/Delete/5
        public IActionResult Delete(int id)
        {
            var patrocinador = _context.Patrocinadores.FirstOrDefault(p => p.Id == id);
            if (patrocinador == null)
                return NotFound();

            return View(patrocinador);
        }

        // POST: Patrocinador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var patrocinador = _context.Patrocinadores.Find(id);
            if (patrocinador != null)
            {
                _context.Patrocinadores.Remove(patrocinador);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
