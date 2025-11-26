using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Data;
using PatrocinioZoneProyecto.Models;

namespace PatrocinioZoneProyecto.Controllers
{
    public class ClubController : Controller
    {
        private readonly AppDbContext _context;

        public ClubController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Club
        public IActionResult Index()
        {
            var clubes = _context.Clubes.ToList();
            return View(clubes);
        }

        // GET: Club/Details/5
        public IActionResult Details(int id)
        {
            var club = _context.Clubes.FirstOrDefault(m => m.Id == id);
            if (club == null) return NotFound();

            return View(club);
        }

        // GET: Club/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Club/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Club club)
        {
            if (ModelState.IsValid)
            {
                _context.Clubes.Add(club);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Club/Edit/5
        public IActionResult Edit(int id)
        {
            var club = _context.Clubes.Find(id);
            if (club == null) return NotFound();

            return View(club);
        }

        // POST: Club/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Club club)
        {
            if (id != club.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(club);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Club/Delete/5
        public IActionResult Delete(int id)
        {
            var club = _context.Clubes.FirstOrDefault(m => m.Id == id);
            if (club == null) return NotFound();

            return View(club);
        }

        // POST: Club/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var club = _context.Clubes.Find(id);
            if (club != null)
            {
                _context.Clubes.Remove(club);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
