using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[VerificaSessaoAluno]
public class QuestoesController(IQuestaoApiClient questaoClient, IMateriaApiClient materiaClient) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        // Ao invés de mandar o aluno para a área genérica de matérias,
        // carregamos as matérias e assuntos na própria área de Questões.
        var materiasBasicas = await materiaClient.GetAllAsync(ct);
        
        var materiasComAssuntos = new List<EnemPrep.Web.Models.Shared.MateriaComAssuntosViewModel>();
        foreach (var m in materiasBasicas)
        {
            var detalhe = await materiaClient.GetByIdAsync(m.Id, ct);
            if (detalhe != null)
                materiasComAssuntos.Add(detalhe);
        }

        return View(materiasComAssuntos);
    }

    public async Task<IActionResult> ListarPorAssunto(Guid assuntoId, string filtro = "todas", CancellationToken ct = default)
    {
        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        if (string.IsNullOrEmpty(usuarioIdStr) || !Guid.TryParse(usuarioIdStr, out var usuarioId))
            return RedirectToAction("Login", "Auth");

        var questoes = await questaoClient.GetByAssuntoAsync(assuntoId, ct);
        var historico = await questaoClient.GetHistoricoAsync(usuarioId, ct);
        
        var respondidasIds = historico.Select(h => h.QuestaoId).ToHashSet();

        // Aplica o filtro em memória, respeitando o backend original
        var questoesFiltradas = filtro.ToLower() switch
        {
            "respondidas" => questoes.Where(q => respondidasIds.Contains(q.Id)).ToList(),
            "nao-respondidas" => questoes.Where(q => !respondidasIds.Contains(q.Id)).ToList(),
            _ => questoes // default: "todas"
        };

        ViewBag.FiltroAtual = filtro;
        ViewBag.AssuntoId = assuntoId;

        // Opcional: Se quiser mandar a flag de respondida na ViewModel (para a view pintar de verde, etc)
        // Isso pode ser feito via ViewBag ou criando um Wrapper local na View.
        ViewBag.RespondidasIds = respondidasIds;

        return View(questoesFiltradas);
    }

    [HttpGet("Questoes/Resolver/{id:guid}")]
    public async Task<IActionResult> Resolver(Guid id, CancellationToken ct)
    {
        var questao = await questaoClient.GetByIdAsync(id, ct);
        if (questao == null)
            return NotFound("Questão não encontrada.");

        return View(questao);
    }

    [HttpPost("Questoes/Responder")]
    public async Task<IActionResult> Responder([FromBody] ResponderQuestaoRequest request, CancellationToken ct)
    {
        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        if (string.IsNullOrEmpty(usuarioIdStr) || !Guid.TryParse(usuarioIdStr, out var usuarioId))
            return Unauthorized();

        var resultado = await questaoClient.ResponderAsync(usuarioId, request.QuestaoId, request.AlternativaId, request.TempoGasto, ct);

        if (resultado == null)
            return BadRequest("Erro ao registrar tentativa.");

        return Json(resultado);
    }

    [HttpGet("Questoes/Historico")]
    public async Task<IActionResult> Historico(CancellationToken ct)
    {
        var usuarioIdStr = HttpContext.Session.GetString("UsuarioId");
        if (string.IsNullOrEmpty(usuarioIdStr) || !Guid.TryParse(usuarioIdStr, out var usuarioId))
            return RedirectToAction("Login", "Auth");

        var historico = await questaoClient.GetHistoricoAsync(usuarioId, ct);
        return View(historico);
    }
}

public class ResponderQuestaoRequest
{
    public Guid QuestaoId { get; set; }
    public Guid AlternativaId { get; set; }
    public int TempoGasto { get; set; }
}
