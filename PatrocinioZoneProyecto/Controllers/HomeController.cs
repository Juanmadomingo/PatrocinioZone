using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Si ya está logueado, redirigir según tipo
        var tipo = HttpContext.Session.GetString("UserType");
        if (tipo == "Club") return RedirectToAction("Index", "Club");
        if (tipo == "Patrocinador") return RedirectToAction("Index", "Patrocinador");

        return View();
    }

    public IActionResult SeleccionTipoUsuario()
    {
        return View();
    }
}
