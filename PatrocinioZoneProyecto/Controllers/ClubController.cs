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

        // ========================
        //   INDEX ZONAS DEL CLUB
        // ========================
        public IActionResult Index()
        {
            int? clubId = HttpContext.Session.GetInt32("UserId");
            if (clubId == null || HttpContext.Session.GetString("UserType") != "Club")
                return RedirectToAction("Login", "Account");

            var zonas = _context.ZonasPatrocinio
                .Where(z => z.ClubId == clubId)
                .Include(z => z.Patrocinador)
                .ToList();

            return View(zonas);
        }

        // ========================
        //   CREAR ZONA GET
        // ========================
        public IActionResult CrearZona()
        {
            ViewBag.Ubicaciones = Enum.GetValues(typeof(Ubicacion));
            return View();
        }

        // ========================
        //   CREAR ZONA POST
        // ========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearZona(ZonaPatrocinio zona)
        {
            int? clubId = HttpContext.Session.GetInt32("UserId");
            if (clubId == null || HttpContext.Session.GetString("UserType") != "Club")
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                ViewBag.Ubicaciones = Enum.GetValues(typeof(Ubicacion));
                return View(zona);
            }

            try
            {
                zona.ClubId = clubId.Value;
                _context.ZonasPatrocinio.Add(zona);
                _context.SaveChanges();

                TempData["Success"] = "Zona creada correctamente.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Ocurrió un error al crear la zona.";
                ViewBag.Ubicaciones = Enum.GetValues(typeof(Ubicacion));
                return View(zona);
            }
        }
    }
}
