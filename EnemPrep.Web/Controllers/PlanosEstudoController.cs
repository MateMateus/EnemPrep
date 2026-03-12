using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Models.PlanosEstudo;
using EnemPrep.Web.Services.ApiClients.Interfaces;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using EnemPrep.Web.Enums;

namespace EnemPrep.Web.Controllers;

[Route("cronograma")]
[VerificaSessaoAluno]
public class PlanosEstudoController : Controller
{
    private readonly IPlanoEstudoApiClient _planoApiClient;
    private readonly IMateriaApiClient _materiaApiClient;
    private readonly IAssuntoApiClient _assuntoApiClient;
    private readonly ILogger<PlanosEstudoController> _logger;

    public PlanosEstudoController(
        IPlanoEstudoApiClient planoApiClient,
        IMateriaApiClient materiaApiClient,
        IAssuntoApiClient assuntoApiClient,
        ILogger<PlanosEstudoController> logger)
    {
        _planoApiClient = planoApiClient;
        _materiaApiClient = materiaApiClient;
        _assuntoApiClient = assuntoApiClient;
        _logger = logger;
    }

    private Guid? GetUsuarioId()
    {
        var idStr = HttpContext.Session.GetString("UsuarioId");
        if (Guid.TryParse(idStr, out var id)) return id;
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        try
        {
            var planos = await _planoApiClient.ObterPorUsuarioAsync(usuarioId.Value, cancellationToken);
            var viewModelList = planos.Select(MapearParaViewModel).ToList();

            // No MVP, vamos focar no primeiro plano ativo ou mais recente
            var planoAtivo = viewModelList.FirstOrDefault(p => p.DataFim >= DateTime.Today) ?? viewModelList.FirstOrDefault();

            ViewBag.Planos = viewModelList;

            if (planoAtivo == null)
            {
                return View("SemPlano");
            }

            return View(planoAtivo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar planos de estudo do usuário {UsuarioId}", usuarioId.Value);
            TempData["Erro"] = "Não foi possível carregar seus planos de estudo. Tente novamente em instantes.";
            return View("SemPlano");
        }
    }

    [HttpGet("criar")]
    public async Task<IActionResult> Criar(CancellationToken cancellationToken)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        var model = new CriarPlanoEstudoViewModel();
        await PreencherMateriasDisponiveis(model, cancellationToken);

        return View(model);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Criar(CriarPlanoEstudoViewModel model, CancellationToken cancellationToken)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        if (model.ItensSelecionados == null || !model.ItensSelecionados.Any())
        {
            ModelState.AddModelError("", "Adicione pelo menos um assunto ao plano.");
        }

        if (!ModelState.IsValid)
        {
            await PreencherMateriasDisponiveis(model, cancellationToken);
            return View(model);
        }

        var sucesso = await _planoApiClient.CriarAsync(usuarioId.Value, model, cancellationToken);

        if (sucesso)
            return RedirectToAction(nameof(Index));

        ModelState.AddModelError("", "Erro ao criar o plano de estudo.");
        await PreencherMateriasDisponiveis(model, cancellationToken);
        return View(model);
    }

    [HttpPost("item/{itemId:guid}/concluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarcarConcluido(Guid itemId, CancellationToken cancellationToken)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        await _planoApiClient.AtualizarStatusItemAsync(itemId, StatusPlanoItem.Concluido, cancellationToken);

        return RedirectToAction(nameof(Index));
    }

    private PlanoEstudoViewModel MapearParaViewModel(PlanoEstudoViewModel dto)
    {
        return new PlanoEstudoViewModel
        {
            Id = dto.Id,
            Titulo = dto.Titulo,
            DataInicio = dto.DataInicio,
            DataFim = dto.DataFim,
            Itens = dto.Itens.Select(i => new PlanoEstudoItemViewModel
            {
                Id = i.Id,
                AssuntoId = i.AssuntoId,
                NomeAssunto = i.NomeAssunto,
                DataPrevista = i.DataPrevista,
                Status = i.Status
            }).OrderBy(i => i.DataPrevista).ToList()
        };
    }

    private async Task PreencherMateriasDisponiveis(CriarPlanoEstudoViewModel model, CancellationToken cancellationToken)
    {
        var materias = await _materiaApiClient.GetAllAsync(cancellationToken);

        var selecionaveis = new List<MateriasComAssuntosViewModel>();

        foreach (var m in materias)
        {
            var assuntos = await _assuntoApiClient.GetByMateriaAsync(m.Id, cancellationToken);
            selecionaveis.Add(new MateriasComAssuntosViewModel
            {
                Id = m.Id,
                Nome = m.Nome,
                Assuntos = assuntos.Select(a => new AssuntoSimplesViewModel { Id = a.Id, Nome = a.Nome }).ToList()
            });
        }

        model.MateriasDisponiveis = selecionaveis;
    }
}
