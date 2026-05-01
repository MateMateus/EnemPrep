using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Areas.Admin.ViewModels.VideoAulas;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Areas.Admin.Controllers;

[Area("Admin")]
[VerificaSessaoAdmin]
public class VideoAulasController(IVideoAulaApiClient videoAulaClient, ILogger<VideoAulasController> logger) : Controller
{
    public async Task<IActionResult> Index(Guid assuntoId, CancellationToken ct)
    {
        if (assuntoId == Guid.Empty)
        {
            TempData["Erro"] = "É necessário selecionar um assunto primeiro.";
            return RedirectToAction("Index", "Materias");
        }

        ViewData["Title"] = "Videoaulas";
        ViewBag.AssuntoId = assuntoId;
        
        try
        {
            var videoaulas = await videoAulaClient.GetByAssuntoAsync(assuntoId, 1, 100, ct);
            return View(videoaulas);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao carregar videoaulas do assunto {AssuntoId}", assuntoId);
            TempData["Erro"] = "Não foi possível carregar a lista de videoaulas no momento. Tente novamente mais tarde.";
            return View(new List<EnemPrep.Web.Models.Shared.VideoAulaViewModel>());
        }
    }

    [HttpGet]
    public IActionResult Criar(Guid assuntoId, string assuntoNome = "")
    {
        if (assuntoId == Guid.Empty)
        {
            TempData["Erro"] = "É necessário selecionar um assunto para criar uma videoaula.";
            return RedirectToAction("Index", "Materias");
        }

        ViewData["Title"] = "Nova Videoaula";
        return View(new CriarVideoAulaViewModel { AssuntoId = assuntoId, AssuntoNome = assuntoNome });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarVideoAulaViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Nova Videoaula";
            return View(vm);
        }

        var resultado = await videoAulaClient.CriarAsync(vm.Titulo, vm.UrlVideo, vm.DuracaoSegundos, vm.AssuntoId, ct);
        if (resultado is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao criar videoaula.");
            ViewData["Title"] = "Nova Videoaula";
            return View(vm);
        }

        TempData["Sucesso"] = $"Videoaula \"{resultado.Titulo}\" criada!";
        return RedirectToAction(nameof(Index), new { assuntoId = vm.AssuntoId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deletar(Guid id, Guid assuntoId, CancellationToken ct)
    {
        await videoAulaClient.DeletarAsync(id, ct);
        TempData["Sucesso"] = "Videoaula removida.";
        return RedirectToAction(nameof(Index), new { assuntoId });
    }
}
