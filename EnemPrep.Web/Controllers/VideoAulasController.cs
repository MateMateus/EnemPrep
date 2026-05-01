using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Filters;
using EnemPrep.Web.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class VideoAulasController(IVideoAulaApiClient videoAulaClient) : Controller
{
    public async Task<IActionResult> ListarPorAssunto(Guid assuntoId, int page = 1, CancellationToken ct = default)
    {
        var videoaulas = await videoAulaClient.GetByAssuntoAsync(assuntoId, page, 10, ct);

        ViewBag.AssuntoId = assuntoId;
        return View(videoaulas);
    }

    [HttpGet]
    public IActionResult Assistir(string url, string titulo, int duracao, Guid assuntoId)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            TempData["Erro"] = "URL do vídeo não informada.";
            return RedirectToAction(nameof(ListarPorAssunto), new { assuntoId });
        }

        var vm = new VideoAulaViewModel
        {
            Titulo = titulo ?? "Videoaula",
            UrlVideo = url,
            DuracaoSegundos = duracao,
            AssuntoId = assuntoId
        };

        return View(vm);
    }
}
