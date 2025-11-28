using Microsoft.AspNetCore.Mvc;
using PatrocinioZoneProyecto.Data;
using PatrocinioZoneProyecto.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace PatrocinioZoneProyecto.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Debe ingresar un email.";
                return View();
            }

            // Buscar CLUB
            var club = _context.Clubes
                .FirstOrDefault(c => c.Email == email && c.Password == password);

            if (club != null)
            {
                HttpContext.Session.SetString("UserType", "Club");
                HttpContext.Session.SetInt32("UserId", club.Id);
                return RedirectToAction("Index", "Club");
            }

            // Buscar PATROCINADOR
            var patrocinador = _context.Patrocinadores
                .FirstOrDefault(p => p.Email == email && p.Password == password);

            if (patrocinador != null)
            {
                HttpContext.Session.SetString("UserType", "Patrocinador");
                HttpContext.Session.SetInt32("UserId", patrocinador.Id);
                return RedirectToAction("Index", "Patrocinador");
            }

            ViewBag.Error = "Email o contraseña incorrectos.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult RegisterClub()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterClub(Club club)
        {
            if (!ModelState.IsValid)
                return View(club);

            var exists = _context.Clubes.Any(c => c.Email == club.Email);
            if (exists)
            {
                ModelState.AddModelError("Email", "Ya existe un club con ese email.");
                return View(club);
            }

            _context.Clubes.Add(club);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        public IActionResult RegisterPatrocinador()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPatrocinador(Patrocinador patrocinador)
        {
            if (!ModelState.IsValid)
                return View(patrocinador);

            var exists = _context.Patrocinadores.Any(p => p.Email == patrocinador.Email);
            if (exists)
            {
                ModelState.AddModelError("Email", "Ya existe un patrocinador con ese email.");
                return View(patrocinador);
            }

            _context.Patrocinadores.Add(patrocinador);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
