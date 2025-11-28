using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PatrocinioZoneProyecto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var userType = HttpContext.Session.GetString("UserType");
            if (string.IsNullOrEmpty(userType))
                return RedirectToAction("Login", "Account");

            // Redirigir según tipo de usuario
            if (userType == "Club")
                return RedirectToAction("Index", "Club");

            if (userType == "Patrocinador")
                return RedirectToAction("Index", "Patrocinador");

            // fallback
            return View();
        }
    }
}