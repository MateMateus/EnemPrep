using EnemPrep.Web.Services.ApiClients.Interfaces;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class ConquistasController(IGamificacaoApiClient gamificacaoClient) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var conquistas = await gamificacaoClient.GetConquistasAsync(ct);
        return View(conquistas);
    }
}
