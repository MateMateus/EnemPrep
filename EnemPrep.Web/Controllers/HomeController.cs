using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EnemPrep.Web.Models;

namespace EnemPrep.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UsuarioId") is not null)
            return RedirectToAction("Index", "Dashboard");

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
