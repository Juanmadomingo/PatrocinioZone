using Microsoft.AspNetCore.Mvc;
using PatrocinioZoneProyecto.Data;
using PatrocinioZoneProyecto.Models;
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

        // ===========================
        // LOGIN - GET
        // ===========================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ===========================
        // LOGIN - POST
        // ===========================
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Buscar usuario en Clubes
            var club = _context.Clubes.FirstOrDefault(c => c.Email == email && c.Password == password);
            // Buscar usuario en Patrocinadores
            var patrocinador = _context.Patrocinadores.FirstOrDefault(p => p.Email == email && p.Password == password);

            Usuario usuario = null;

            if (club != null)
                usuario = club;
            else if (patrocinador != null)
                usuario = patrocinador;

            if (usuario == null)
            {
                ViewBag.Error = "Email o contraseña incorrectos";
                return View();
            }

            // Guardar usuario en sesión (opcional)
            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

            // Redirigir al Home
            return RedirectToAction("Index", "Home");
        }

        // ===========================
        // REGISTER - GET
        // ===========================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ===========================
        // REGISTER - POST
        // ===========================
        [HttpPost]
        public IActionResult Register(string nombre, string email, string password, string tipo, string direccion, string deporte, string empresa, string telefono, decimal monto)
        {
            Usuario usuario = null;

            if (tipo == "club")
            {
                var club = new Club
                {
                    Nombre = nombre,
                    Email = email,
                    Password = password,
                    Direccion = direccion,
                    Deporte = deporte
                };
                _context.Clubes.Add(club);
                usuario = club;
            }
            else if (tipo == "patrocinador")
            {
                var patrocinador = new Patrocinador
                {
                    Nombre = nombre,
                    Email = email,
                    Password = password,
                    Empresa = empresa,
                    Telefono = telefono,
                    Monto = monto
                };
                _context.Patrocinadores.Add(patrocinador);
                usuario = patrocinador;
            }

            _context.SaveChanges();

            // Guardar usuario en sesión (opcional)
            HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

            // Redirigir al Home
            return RedirectToAction("Index", "Home");
        }
    }
}
