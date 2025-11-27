using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            return View(await _context.ZonasPatrocinio.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZonaPatrocinio zona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zona);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var zona = await _context.ZonasPatrocinio.FindAsync(id);
            if (zona == null) return NotFound();

            return View(zona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ZonaPatrocinio zona)
        {
            if (ModelState.IsValid)
            {
                _context.Update(zona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zona);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var zona = await _context.ZonasPatrocinio.FindAsync(id);
            if (zona == null) return NotFound();

            return View(zona);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zona = await _context.ZonasPatrocinio.FindAsync(id);
            if (zona != null)
            {
                _context.ZonasPatrocinio.Remove(zona);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var zona = await _context.ZonasPatrocinio.FindAsync(id);
            if (zona == null) return NotFound();

            return View(zona);
        }
    }
}

