using EnemPrep.Web.Models.Simulados;
using EnemPrep.Web.Services.ApiClients.Interfaces;
using EnemPrep.Web.ViewModels.Simulados;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[Route("[controller]")]
public class SimuladosController : Controller
{
    private readonly ISimuladoApiClient _simuladoApiClient;

    public SimuladosController(ISimuladoApiClient simuladoApiClient)
    {
        _simuladoApiClient = simuladoApiClient;
    }

    private Guid GetUsuarioId()
    {
        var idStr = HttpContext.Session.GetString("UsuarioId");
        if (string.IsNullOrEmpty(idStr))
        {
            // Fallback for tests or not logged in - in reality should redirect to login
            return Guid.Empty;
        }
        return Guid.Parse(idStr);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var simulados = await _simuladoApiClient.GetSimuladosDisponiveisAsync();

        var vms = simulados.Select(s => new SimuladoResumoViewModel
        {
            Id = s.Id,
            Titulo = s.Titulo,
            AnoReferencia = s.AnoReferencia,
            DuracaoMaxima = s.DuracaoMaxima,
            QuantidadeQuestoes = s.QuantidadeQuestoes
        });

        return View(vms);
    }

    [HttpGet("{id:guid}/Instrucoes")]
    public async Task<IActionResult> Instrucoes(Guid id)
    {
        var simulado = await _simuladoApiClient.GetSimuladoByIdAsync(id);
        if (simulado == null)
            return RedirectToAction(nameof(Index));

        var vm = new SimuladoDetalheViewModel
        {
            Id = simulado.Id,
            Titulo = simulado.Titulo,
            AnoReferencia = simulado.AnoReferencia,
            DuracaoMaxima = simulado.DuracaoMaxima,
            Questoes = simulado.Questoes
        };

        return View(vm);
    }

    [HttpPost("Iniciar")]
    public async Task<IActionResult> Iniciar(Guid simuladoId)
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == Guid.Empty)
            return RedirectToAction("Login", "Auth");

        var result = await _simuladoApiClient.IniciarSimuladoAsync(usuarioId, new IniciarSimuladoRequest { SimuladoId = simuladoId });

        if (result == null)
            return RedirectToAction(nameof(Index));

        return RedirectToAction(nameof(Fazendo), new { tentativaId = result.Id });
    }

    [HttpGet("{tentativaId:guid}/Fazendo")]
    public async Task<IActionResult> Fazendo(Guid tentativaId)
    {
        // For MVP-simplicity, we might just load the whole simulado into the view
        // In a real SPA it would be paginated API calls.
        var usuarioId = GetUsuarioId();
        var historico = await _simuladoApiClient.GetHistoricoTentativasAsync(usuarioId);
        var tentativa = historico.FirstOrDefault(t => t.Id == tentativaId);

        if (tentativa == null)
            return RedirectToAction(nameof(Index));

        var simulado = await _simuladoApiClient.GetSimuladoByIdAsync(tentativa.SimuladoId);

        ViewBag.TentativaId = tentativa.Id;
        return View(simulado);
    }

    [HttpPost("{tentativaId:guid}/Submeter")]
    public async Task<IActionResult> Submeter(Guid tentativaId, [FromBody] SubmeterSimuladoRequest request)
    {
        var usuarioId = GetUsuarioId();
        var response = await _simuladoApiClient.SubmeterSimuladoAsync(usuarioId, tentativaId, request);

        if (response == null)
            return BadRequest(new { message = "Erro inesperado ao comunicar com o servidor." });

        if (!response.Success)
            return BadRequest(new { message = response.ErrorMessage ?? "Erro ao submeter o simulado." });

        return Ok(new { redirectUrl = Url.Action(nameof(Resultado), new { tentativaId = response.Data!.Id }) });
    }

    [HttpGet("{tentativaId:guid}/Resultado")]
    public async Task<IActionResult> Resultado(Guid tentativaId)
    {
        var usuarioId = GetUsuarioId();
        var historico = await _simuladoApiClient.GetHistoricoTentativasAsync(usuarioId);
        var tentativa = historico.FirstOrDefault(t => t.Id == tentativaId);

        if (tentativa == null)
            return RedirectToAction(nameof(Index));

        return View(tentativa);
    }
}
