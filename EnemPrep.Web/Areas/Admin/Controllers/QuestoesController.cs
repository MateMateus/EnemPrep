using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Areas.Admin.ViewModels.Questoes;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Areas.Admin.Controllers;

[Area("Admin")]
[VerificaSessaoAdmin]
public class QuestoesController(IQuestaoApiClient questaoClient, ILivroApiClient livroClient, ILogger<QuestoesController> logger) : Controller
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
    public async Task<IActionResult> Criar(Guid assuntoId, Guid? livroId = null, Guid? livroTemaId = null, string assuntoNome = "", CancellationToken ct = default)
    {
        if (assuntoId == Guid.Empty)
        {
            TempData["Erro"] = "É necessário selecionar um assunto para criar uma questão.";
            return RedirectToAction("Index", "Materias");
        }

        var livros = await livroClient.GetAllAsync(ct: ct);
        ViewBag.Livros = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(livros, "Id", "Titulo", livroId);

        ViewData["Title"] = "Nova Questão";
        return View(new CriarQuestaoViewModel
        {
            AssuntoId = assuntoId,
            AssuntoNome = assuntoNome,
            LivroId = livroId,
            LivroTemaId = livroTemaId
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(CriarQuestaoViewModel vm, CancellationToken ct)
    {
        // Normaliza URL vazia para null (evita falha do [Url] em string vazia)
        if (string.IsNullOrWhiteSpace(vm.VideoExplicacaoUrl))
        {
            vm.VideoExplicacaoUrl = null;
            ModelState.Remove(nameof(vm.VideoExplicacaoUrl));
        }

        // Normaliza Explicacao vazia para null
        if (string.IsNullOrWhiteSpace(vm.Explicacao))
        {
            vm.Explicacao = null;
            ModelState.Remove(nameof(vm.Explicacao));
        }

        var alternativasPreenchidas = vm.Alternativas.Where(a => !string.IsNullOrWhiteSpace(a.Texto)).ToList();

        if (alternativasPreenchidas.Count < 2)
            ModelState.AddModelError(string.Empty, "Preencha o texto de pelo menos 2 alternativas.");

        // Valida que exatamente uma alternativa entre as preenchidas está marcada como correta
        if (alternativasPreenchidas.Count(a => a.IsCorreta) != 1)
            ModelState.AddModelError(string.Empty, "Selecione exatamente uma alternativa correta.");

        if (!ModelState.IsValid)
        {
            // Log de depuração para ver quais campos falham (Sanitizado)
            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    logger.LogWarning("[ModelState ERROR] Falha de validação identificada no campo: '{Key}'", entry.Key);
                }
            }

            var livros = await livroClient.GetAllAsync(ct: ct);
            ViewBag.Livros = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(livros, "Id", "Titulo", vm.LivroId);
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

        var resultado = await questaoClient.CriarAsync(vm.Enunciado, dificuldadeInt, vm.AssuntoId, vm.Explicacao, vm.VideoExplicacaoUrl, alternativas, vm.LivroId, vm.LivroTemaId, ct);
        if (resultado is null)
        {
            logger.LogError("Falha na API ao criar questão. AssuntoId={AssuntoId}, Enunciado='{Enunciado}'", vm.AssuntoId, vm.Enunciado[..Math.Min(50, vm.Enunciado.Length)]);
            ModelState.AddModelError(string.Empty, "Erro ao criar questão. Verifique se a API está rodando.");
            
            var livros = await livroClient.GetAllAsync(ct: ct);
            ViewBag.Livros = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(livros, "Id", "Titulo", vm.LivroId);
            ViewData["Title"] = "Nova Questão";
            return View(vm);
        }

        TempData["Sucesso"] = "Questão criada com sucesso!";
        return RedirectToAction(nameof(Index), new { assuntoId = vm.AssuntoId });
    }

    [HttpGet]
    public async Task<IActionResult> Editar(Guid id, Guid assuntoId, CancellationToken ct)
    {
        var questao = await questaoClient.GetByIdAsync(id, ct);
        if (questao is null)
        {
            TempData["Erro"] = "Questão não encontrada.";
            return RedirectToAction(nameof(Index), new { assuntoId });
        }

        var vm = new EditarQuestaoViewModel
        {
            Id = questao.Id,
            AssuntoId = questao.AssuntoId,
            AssuntoNome = questao.NomeAssunto,
            Enunciado = questao.Enunciado,
            Dificuldade = questao.Dificuldade,
            Explicacao = questao.Explicacao,
            VideoExplicacaoUrl = questao.VideoExplicacaoUrl,
            Alternativas = questao.Alternativas.Select(a => new AlternativaViewModel { Texto = a.Texto, IsCorreta = a.IsCorreta }).ToList()
        };

        // Ensure 5 alternatives minimum for UI matching
        while (vm.Alternativas.Count < 5)
        {
            vm.Alternativas.Add(new AlternativaViewModel());
        }

        var livros = await livroClient.GetAllAsync(ct: ct);
        ViewBag.Livros = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(livros, "Id", "Titulo", questao.LivroId);
        vm.LivroId = questao.LivroId;
        vm.LivroTemaId = questao.LivroTemaId;

        ViewData["Title"] = "Editar Questão";
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(Guid id, EditarQuestaoViewModel vm, CancellationToken ct)
    {
        if (id != vm.Id) return BadRequest();

        if (string.IsNullOrWhiteSpace(vm.VideoExplicacaoUrl)) { vm.VideoExplicacaoUrl = null; ModelState.Remove(nameof(vm.VideoExplicacaoUrl)); }
        if (string.IsNullOrWhiteSpace(vm.Explicacao)) { vm.Explicacao = null; ModelState.Remove(nameof(vm.Explicacao)); }

        var alternativasPreenchidas = vm.Alternativas.Where(a => !string.IsNullOrWhiteSpace(a.Texto)).ToList();

        if (alternativasPreenchidas.Count < 2) ModelState.AddModelError(string.Empty, "Preencha o texto de pelo menos 2 alternativas.");
        if (alternativasPreenchidas.Count(a => a.IsCorreta) != 1) ModelState.AddModelError(string.Empty, "Selecione exatamente uma alternativa correta.");

        if (!ModelState.IsValid)
        {
            var livros = await livroClient.GetAllAsync(ct: ct);
            ViewBag.Livros = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(livros, "Id", "Titulo", vm.LivroId);
            ViewData["Title"] = "Editar Questão";
            return View(vm);
        }

        int dificuldadeInt = vm.Dificuldade switch { "Facil" => 1, "Medio" => 2, "Dificil" => 3, _ => 2 };
        var alternativas = alternativasPreenchidas.Select(a => (a.Texto!, a.IsCorreta));

        var resultado = await questaoClient.AtualizarAsync(vm.Id, vm.Enunciado, dificuldadeInt, vm.Explicacao, vm.VideoExplicacaoUrl, alternativas, vm.LivroId, vm.LivroTemaId, ct);
        
        if (resultado is null)
        {
            ModelState.AddModelError(string.Empty, "Erro ao atualizar questão.");
            var livros = await livroClient.GetAllAsync(ct: ct);
            ViewBag.Livros = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(livros, "Id", "Titulo", vm.LivroId);
            ViewData["Title"] = "Editar Questão";
            return View(vm);
        }

        TempData["Sucesso"] = "Questão atualizada com sucesso!";
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

    [HttpGet]
    public async Task<IActionResult> ObterTemas(Guid livroId, CancellationToken ct)
    {
        var livro = await livroClient.GetByIdAsync(livroId, ct);
        if (livro == null) return NotFound();
        return Json(new { data = new { temas = livro.Temas } });
    }
}
