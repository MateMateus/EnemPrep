using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class VideoAulasController(IVideoAulaApiClient videoAulaClient) : Controller
{
    public async Task<IActionResult> ListarPorAssunto(Guid assuntoId, CancellationToken ct)
    {
        var videoaulas = await videoAulaClient.GetByAssuntoAsync(assuntoId, ct);

        ViewBag.AssuntoId = assuntoId;
        return View(videoaulas);
    }
}
