using Microsoft.AspNetCore.Mvc;

namespace PatrocinioZoneProyecto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Va a buscar Views/Home/Index.cshtml
        }

        public IActionResult Privacy()
        {
            return View(); // Opcional
        }
    }
}
