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
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        var dashboard = await dashboardClient.GetDashboardAsync(usuarioId, ct);
        if (dashboard is null)
        {
            // Limpa a sessão para quebrar qualquer loop de redirecionamento.
            // Se a API falhou (token expirado, indisponível, etc.), o usuário
            // deve ver a tela de login, não ficar preso num loop.
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        var desafio = await gamificacaoClient.GetDesafioDiarioAsync(ct);

        dashboard.NomeUsuario = HttpContext.Session.GetString("UsuarioNome") ?? "Estudante";

        dashboard.DesafioDiario = desafio;

        return View(dashboard);
    }
}
