using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Areas.Admin.ViewModels.Materiais;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class MateriaisController(IMaterialApiClient materialClient, ILogger<MateriaisController> logger) : Controller
{
    public async Task<IActionResult> Index(Guid assuntoId, CancellationToken ct)
    {
        if (assuntoId == Guid.Empty)
        {
            TempData["Erro"] = "É necessário selecionar um assunto primeiro.";
            return RedirectToAction("Index", "Materias");
        }

        ViewData["Title"] = "Materiais de Estudo";
        ViewBag.AssuntoId = assuntoId;

        try
        {
            var materiais = await materialClient.GetByAssuntoAsync(assuntoId, ct);
            
            var viewModel = materiais.Select(m => new MaterialEstudoViewModel
            {
                Id = m.Id,
                Titulo = m.Titulo,
                Tipo = m.Tipo,
                UrlArquivo = m.UrlArquivo,
                AssuntoId = m.AssuntoId,
                AssuntoNome = ""
            }).ToList();

            return View(viewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao carregar materiais do assunto {AssuntoId}", assuntoId);
            TempData["Erro"] = "Não foi possível carregar a lista de materiais no momento. Tente novamente mais tarde.";
            return View(new List<MaterialEstudoViewModel>());
        }
    }

    [HttpGet]
    public IActionResult Criar(Guid assuntoId, string assuntoNome = "")
    {
        if (assuntoId == Guid.Empty)
        {
            TempData["Erro"] = "É necessário selecionar um assunto para criar um material.";
            return RedirectToAction("Index", "Materias");
        }

        ViewData["Title"] = "Novo Material";
        return View(new CriarMaterialViewModel { AssuntoId = assuntoId, AssuntoNome = assuntoNome });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarMaterialViewModel vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Novo Material";
            return View(vm);
        }

        int tipoInt = vm.Tipo switch
        {
            "PDF" => 1,
            "Resumo" => 2,
            "MapaMental" => 3,
            "LinkExterno" => 4,
            _ => 1
        };

        var resultado = await materialClient.CriarAsync(vm.Titulo, tipoInt, vm.UrlArquivo, vm.AssuntoId, ct);
        if (resultado is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao criar material.");
            ViewData["Title"] = "Novo Material";
            return View(vm);
        }

        TempData["Sucesso"] = $"Material \"{resultado.Titulo}\" criado!";
        return RedirectToAction(nameof(Index), new { assuntoId = vm.AssuntoId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deletar(Guid id, Guid assuntoId, CancellationToken ct)
    {
        await materialClient.DeletarAsync(id, ct);
        TempData["Sucesso"] = "Material removido.";
        return RedirectToAction(nameof(Index), new { assuntoId });
    }
}
