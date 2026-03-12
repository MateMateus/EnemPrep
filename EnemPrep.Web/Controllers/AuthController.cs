using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

public class AuthController(IAuthApiClient authClient) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        // Se já estiver logado, vai direto pro Dashboard
        if (HttpContext.Session.GetString("UsuarioId") is not null)
            return RedirectToAction("Index", "Dashboard");

        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(vm);

        var response = await authClient.LoginAsync(vm, ct);
        if (response is null)
        {
            ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
            return View(vm);
        }

        HttpContext.Session.SetString("UsuarioId", response.UsuarioId.ToString());
        HttpContext.Session.SetString("NomeUsuario", response.Nome);
        HttpContext.Session.SetString("TipoPerfil", response.TipoPerfil);
        HttpContext.Session.SetString("Token", response.Token); // Opcional, mantido caso útil futuramente

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.Session.GetString("UsuarioId") is not null)
            return RedirectToAction("Index", "Dashboard");

        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(vm);

        var response = await authClient.RegisterAsync(vm, ct);
        if (response is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao registrar. Verifique os dados ou se o email já existe.");
            return View(vm);
        }

        // Login automático após registro
        HttpContext.Session.SetString("UsuarioId", response.UsuarioId.ToString());
        HttpContext.Session.SetString("NomeUsuario", response.Nome);
        HttpContext.Session.SetString("TipoPerfil", response.TipoPerfil);
        HttpContext.Session.SetString("Token", response.Token);

        return RedirectToAction("Index", "Dashboard");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
