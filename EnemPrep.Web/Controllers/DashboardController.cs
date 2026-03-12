using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class DashboardController(
    IDashboardApiClient dashboardClient,
    EnemPrep.Web.Services.ApiClients.Interfaces.IGamificacaoApiClient gamificacaoClient) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        if (string.IsNullOrEmpty(usuarioIdStr) || !Guid.TryParse(usuarioIdStr, out var usuarioId))
            return RedirectToAction("Login", "Auth");

        var dashboard = await dashboardClient.GetDashboardAsync(usuarioId, ct);
        if (dashboard is null)
            return RedirectToAction("Index", "Home");

        var streak = await gamificacaoClient.GetStreakAsync(ct);
        var desafio = await gamificacaoClient.GetDesafioDiarioAsync(ct);

        dashboard.NomeUsuario = HttpContext.Session.GetString("UsuarioNome") ?? "Estudante";

        if (streak is null)
        {
            dashboard.StreakAtual = 0;
            dashboard.MaiorStreak = 0;
        }
        else
        {
            dashboard.StreakAtual = streak.DiasConsecutivos;
            dashboard.MaiorStreak = streak.MaiorStreak;
        }

        dashboard.DesafioDiario = desafio;

        return View(dashboard);
    }
}
