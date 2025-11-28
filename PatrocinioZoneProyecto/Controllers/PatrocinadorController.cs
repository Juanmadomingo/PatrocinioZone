using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Data;
using PatrocinioZoneProyecto.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PatrocinioZoneProyecto.Controllers
{
    public class PatrocinadorController : Controller
    {
        private readonly AppDbContext _context;

        public PatrocinadorController(AppDbContext context)
        {
            _context = context;
        }

        // ========================
        //   REGISTER GET
        // ========================
        public IActionResult Register()
        {
            return View();
        }

        // ========================
        //   REGISTER POST
        // ========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Patrocinador patrocinador)
        {
            if (!ModelState.IsValid)
                return View(patrocinador);

            var exists = _context.Patrocinadores.Any(p => p.Email == patrocinador.Email);
            if (exists)
            {
                ModelState.AddModelError(nameof(patrocinador.Email), "Ya existe un patrocinador con ese email.");
                return View(patrocinador);
            }

            patrocinador.MontoDisponible = patrocinador.MontoDisponible <= 0 ? 10000m : patrocinador.MontoDisponible;
            _context.Patrocinadores.Add(patrocinador);
            _context.SaveChanges();

            TempData["Success"] = "Cuenta creada con éxito. Ahora podés iniciar sesión.";
            return RedirectToAction("Login", "Account");
        }

        // ========================
        //   INDEX (ZONAS DISPONIBLES) + búsqueda opcional
        // ========================
        public IActionResult Index(string q = null)
        {
            int? patId = HttpContext.Session.GetInt32("UserId");
            if (patId == null || HttpContext.Session.GetString("UserType") != "Patrocinador")
                return RedirectToAction("Login", "Account");

            var query = _context.ZonasPatrocinio
                .Where(z => z.PatrocinadorId == null)
                .Include(z => z.Club)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim().ToLower();
                query = query.Where(z => z.Nombre.ToLower().Contains(q) || z.Descripcion.ToLower().Contains(q) || z.Club.Nombre.ToLower().Contains(q));
            }

            var zonas = query.ToList();
            ViewBag.Query = q;
            return View(zonas);
        }

        // ========================
        //   COMPRA — GET (confirmación)
        // ========================
        public IActionResult Comprar(int id)
        {
            int? patId = HttpContext.Session.GetInt32("UserId");
            if (patId == null || HttpContext.Session.GetString("UserType") != "Patrocinador")
                return RedirectToAction("Login", "Account");

            var zona = _context.ZonasPatrocinio
                .Include(z => z.Club)
                .FirstOrDefault(z => z.Id == id);

            if (zona == null)
                return NotFound();

            if (zona.PatrocinadorId != null)
            {
                TempData["Error"] = "La zona ya fue comprada.";
                return RedirectToAction("Index");
            }

            return View(zona);
        }

        // ========================
        //   COMPRA — POST (confirmado)
        // ========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ComprarConfirmado(int id)
        {
            int? patId = HttpContext.Session.GetInt32("UserId");
            if (patId == null || HttpContext.Session.GetString("UserType") != "Patrocinador")
                return RedirectToAction("Login", "Account");

            // Recuperamos zona y patrocinador con locking mínimo
            var zona = _context.ZonasPatrocinio
                .Include(z => z.Club)
                .FirstOrDefault(z => z.Id == id);

            var patrocinador = _context.Patrocinadores.Find(patId.Value);

            if (zona == null)
            {
                TempData["Error"] = "La zona no existe.";
                return RedirectToAction("Index");
            }

            if (zona.PatrocinadorId != null)
            {
                TempData["Error"] = "La zona ya fue comprada por otro usuario.";
                return RedirectToAction("Index");
            }

            if (patrocinador == null)
            {
                TempData["Error"] = "Patrocinador no encontrado.";
                return RedirectToAction("Login", "Account");
            }

            if (patrocinador.MontoDisponible < zona.Precio)
            {
                TempData["Error"] = "No tenés dinero suficiente para comprar esta zona.";
                return RedirectToAction("Index");
            }

            // Marcar la zona como comprada y actualizar montos
            zona.PatrocinadorId = patrocinador.Id;
            patrocinador.MontoDisponible -= zona.Precio;

            // Sumar al monto del club (acumular ingresos)
            var club = _context.Clubes.Find(zona.ClubId);
            if (club != null)
            {
                club.MontoBase += zona.Precio;
            }

            _context.SaveChanges();

            TempData["Success"] = $"Compra exitosa: adquiriste la zona '{zona.Nombre}' por ${zona.Precio}.";
            return RedirectToAction("MisPatrocinios");
        }

        // ========================
        //   MIS PATROCINIOS (lo que compró el patrocinador)
        // ========================
        public IActionResult MisPatrocinios()
        {
            int? patId = HttpContext.Session.GetInt32("UserId");
            if (patId == null || HttpContext.Session.GetString("UserType") != "Patrocinador")
                return RedirectToAction("Login", "Account");

            var zonas = _context.ZonasPatrocinio
                .Where(z => z.PatrocinadorId == patId.Value)
                .Include(z => z.Club)
                .ToList();

            return View(zonas);
        }

        // ========================
        //   LOGOUT
        // ========================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}