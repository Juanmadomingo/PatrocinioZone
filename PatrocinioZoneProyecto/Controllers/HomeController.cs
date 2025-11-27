using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("SeleccionTipoUsuario");
    }

    public IActionResult SeleccionTipoUsuario()
    {
        return View();
    }
}
