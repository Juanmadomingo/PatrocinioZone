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
            if (ModelState.IsValid)
            {
                patrocinador.MontoDisponible = 10000; // monto inicial
                _context.Patrocinadores.Add(patrocinador);
                _context.SaveChanges();

                TempData["Success"] = "Cuenta creada con éxito. Ahora podés iniciar sesión.";
                return RedirectToAction("Login", "Account");
            }

            return View(patrocinador);
        }

        // ========================
        //   INDEX (ZONAS DISPONIBLES)
        // ========================
        public IActionResult Index()
        {
            int? patId = HttpContext.Session.GetInt32("UserId");
            if (patId == null || HttpContext.Session.GetString("UserType") != "Patrocinador")
                return RedirectToAction("Login", "Account");

            var zonas = _context.ZonasPatrocinio
                .Where(z => z.PatrocinadorId == null)
                .Include(z => z.Club)
                .ToList();

            return View(zonas);
        }

        // ========================
        //   COMPRAR GET
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

            return View(zona);
        }

        // ========================
        //   COMPRAR POST CONFIRMADO
        // ========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ComprarConfirmado(int id)
        {
            int? patId = HttpContext.Session.GetInt32("UserId");
            if (patId == null || HttpContext.Session.GetString("UserType") != "Patrocinador")
                return RedirectToAction("Login", "Account");

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
                TempData["Error"] = "La zona ya fue comprada.";
                return RedirectToAction("Index");
            }

            if (patrocinador.MontoDisponible < zona.Precio)
            {
                TempData["Error"] = "No tenés dinero suficiente para comprar esta zona.";
                return RedirectToAction("Index");
            }

            // Actualizar
            zona.PatrocinadorId = patId.Value;
            patrocinador.MontoDisponible -= zona.Precio;

            _context.SaveChanges();

            TempData["Success"] = $"Compra exitosa: adquiriste la zona '{zona.Nombre}' por ${zona.Precio}.";
            return RedirectToAction("Index");
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

