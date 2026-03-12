using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Areas.Admin.ViewModels.Questoes;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class QuestoesController(IQuestaoApiClient questaoClient, ILogger<QuestoesController> logger) : Controller
{
    public async Task<IActionResult> Index(Guid assuntoId, CancellationToken ct)
    {
        if (assuntoId == Guid.Empty)
        {
            TempData["Erro"] = "É necessário selecionar um assunto primeiro.";
            return RedirectToAction("Index", "Materias");
        }

        ViewData["Title"] = "Questões";
        ViewBag.AssuntoId = assuntoId;

        try
        {
            var questoes = await questaoClient.GetByAssuntoAsync(assuntoId, ct);
            
            var viewModel = questoes.Select(q => new QuestaoViewModel
            {
                Id = q.Id,
                Enunciado = q.Enunciado,
                Dificuldade = q.Dificuldade,
                AssuntoId = q.AssuntoId,
                AssuntoNome = q.NomeAssunto,
                TotalAlternativas = q.Alternativas?.Count ?? 0
            }).ToList();

            return View(viewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao carregar questões do assunto {AssuntoId}", assuntoId);
            TempData["Erro"] = "Não foi possível carregar a lista de questões no momento. Tente novamente mais tarde.";
            return View(new List<QuestaoViewModel>());
        }
    }

    [HttpGet]
    public IActionResult Criar(Guid assuntoId, string assuntoNome = "")
    {
        if (assuntoId == Guid.Empty)
        {
            TempData["Erro"] = "É necessário selecionar um assunto para criar uma questão.";
            return RedirectToAction("Index", "Materias");
        }

        ViewData["Title"] = "Nova Questão";
        return View(new CriarQuestaoViewModel
        {
            AssuntoId = assuntoId,
            AssuntoNome = assuntoNome
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarQuestaoViewModel vm, CancellationToken ct)
    {
        var alternativasPreenchidas = vm.Alternativas.Where(a => !string.IsNullOrWhiteSpace(a.Texto)).ToList();

        if (alternativasPreenchidas.Count < 2)
            ModelState.AddModelError(string.Empty, "Preencha o texto de pelo menos 2 alternativas.");

        // Valida que exatamente uma alternativa entre as preenchidas está marcada como correta
        if (alternativasPreenchidas.Count(a => a.IsCorreta) != 1)
            ModelState.AddModelError(string.Empty, "Selecione exatamente uma alternativa correta.");

        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Nova Questão";
            return View(vm);
        }

        int dificuldadeInt = vm.Dificuldade switch
        {
            "Facil" => 1,
            "Medio" => 2,
            "Dificil" => 3,
            _ => 2
        };
        var alternativas = alternativasPreenchidas.Select(a => (a.Texto!, a.IsCorreta));

        var resultado = await questaoClient.CriarAsync(vm.Enunciado, dificuldadeInt, vm.AssuntoId, vm.Explicacao, alternativas, ct);
        if (resultado is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao criar questão.");
            ViewData["Title"] = "Nova Questão";
            return View(vm);
        }

        TempData["Sucesso"] = "Questão criada com sucesso!";
        return RedirectToAction(nameof(Index), new { assuntoId = vm.AssuntoId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deletar(Guid id, Guid assuntoId, CancellationToken ct)
    {
        await questaoClient.DeletarAsync(id, ct);
        TempData["Sucesso"] = "Questão removida.";
        return RedirectToAction(nameof(Index), new { assuntoId });
    }
}
