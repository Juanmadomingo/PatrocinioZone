using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            int? patId = HttpContext.Session.GetInt32("PatrocinadorId");
            if (patId == null) return RedirectToAction("Login", "Account");

            var zonas = _context.ZonasPatrocinio
                .Where(z => z.PatrocinadorId == null)
                .Include(z => z.Club)
                .ToList();

            return View(zonas);
        }

        public IActionResult Comprar(int id)
        {
            var zona = _context.ZonasPatrocinio.Include(z => z.Club)
                    .FirstOrDefault(z => z.Id == id);

            return View(zona);
        }

        [HttpPost]
        public IActionResult ComprarConfirmado(int id)
        {
            int? patId = HttpContext.Session.GetInt32("PatrocinadorId");
            if (patId == null) return RedirectToAction("Login", "Account");

            var zona = _context.ZonasPatrocinio.Find(id);

            zona.PatrocinadorId = patId.Value;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

