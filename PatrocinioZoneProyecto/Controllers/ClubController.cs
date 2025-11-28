using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Data;
using PatrocinioZoneProyecto.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PatrocinioZoneProyecto.Controllers
{
    public class ClubController : Controller
    {
        private readonly AppDbContext _context;

        public ClubController(AppDbContext context)
        {
            _context = context;
        }

        // Dashboard del club: muestra monto y sus zonas (mis patrocinios)
        public IActionResult Index()
        {
            int? clubId = HttpContext.Session.GetInt32("UserId");
            if (clubId == null || HttpContext.Session.GetString("UserType") != "Club")
                return RedirectToAction("Login", "Account");

            var club = _context.Clubes
                .Include(c => c.Zonas)
                    .ThenInclude(z => z.Patrocinador)
                .FirstOrDefault(c => c.Id == clubId.Value);

            if (club == null) return NotFound();

            return View(club);
        }

        // GET: Crear zona de patrocinio
        public IActionResult CreateZona()
        {
            int? clubId = HttpContext.Session.GetInt32("UserId");
            if (clubId == null || HttpContext.Session.GetString("UserType") != "Club")
                return RedirectToAction("Login", "Account");

            return View(new ZonaPatrocinio());
        }

        // POST: Crear zona de patrocinio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateZona(ZonaPatrocinio zona)
        {
            int? clubId = HttpContext.Session.GetInt32("UserId");
            if (clubId == null || HttpContext.Session.GetString("UserType") != "Club")
                return RedirectToAction("Login", "Account");

            // Validaciones básicas del modelo
            if (!ModelState.IsValid)
                return View(zona);

            // No permitir crear otra zona en la misma ubicacion para este club (única por ubicación)
            bool existsSameUbicacion = _context.ZonasPatrocinio
                .Any(z => z.ClubId == clubId.Value && z.Ubicacion == zona.Ubicacion);

            if (existsSameUbicacion)
            {
                ModelState.AddModelError(nameof(zona.Ubicacion), "Ya existe una zona con esa ubicación para tu club.");
                return View(zona);
            }

            zona.ClubId = clubId.Value;
            _context.ZonasPatrocinio.Add(zona);
            _context.SaveChanges();

            TempData["Success"] = "Zona creada y publicada a la venta.";
            return RedirectToAction(nameof(Index));
        }

        // Ver detalles de una zona (opcional)
        public IActionResult DetailsZona(int id)
        {
            int? clubId = HttpContext.Session.GetInt32("UserId");
            if (clubId == null || HttpContext.Session.GetString("UserType") != "Club")
                return RedirectToAction("Login", "Account");

            var zona = _context.ZonasPatrocinio
                .Include(z => z.Patrocinador)
                .Include(z => z.Club)
                .FirstOrDefault(z => z.Id == id && z.ClubId == clubId.Value);

            if (zona == null) return NotFound();

            return View(zona);
        }

        // Opcional: eliminar zona si aún no fue vendida
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteZona(int id)
        {
            int? clubId = HttpContext.Session.GetInt32("UserId");
            if (clubId == null || HttpContext.Session.GetString("UserType") != "Club")
                return RedirectToAction("Login", "Account");

            var zona = _context.ZonasPatrocinio.FirstOrDefault(z => z.Id == id && z.ClubId == clubId.Value);
            if (zona == null) return NotFound();

            if (zona.PatrocinadorId != null)
            {
                TempData["Error"] = "No se puede eliminar una zona que ya fue vendida.";
                return RedirectToAction(nameof(Index));
            }

            _context.ZonasPatrocinio.Remove(zona);
            _context.SaveChanges();

            TempData["Success"] = "Zona eliminada.";
            return RedirectToAction(nameof(Index));
        }
    }
}