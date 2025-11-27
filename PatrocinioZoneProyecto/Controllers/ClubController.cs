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

        public IActionResult Index()
        {
            int? clubId = HttpContext.Session.GetInt32("ClubId");
            if (clubId == null) return RedirectToAction("Login", "Account");

            var zonas = _context.ZonasPatrocinio
                .Where(z => z.ClubId == clubId)
                .Include(z => z.Patrocinador)
                .ToList();

            return View(zonas);
        }

        // CREAR ZONA
        public IActionResult CrearZona()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearZona(ZonaPatrocinio zona)
        {
            int? clubId = HttpContext.Session.GetInt32("ClubId");
            if (clubId == null) return RedirectToAction("Login", "Account");

            zona.ClubId = clubId.Value;

            _context.ZonasPatrocinio.Add(zona);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // VER ZONAS VENDIDAS / NO VENDIDAS
        public IActionResult Zonas()
        {
            int? clubId = HttpContext.Session.GetInt32("ClubId");
            if (clubId == null) return RedirectToAction("Login", "Account");

            var zonas = _context.ZonasPatrocinio
                .Where(z => z.ClubId == clubId)
                .Include(z => z.Patrocinador)
                .ToList();

            return View(zonas);
        }
    }
}
