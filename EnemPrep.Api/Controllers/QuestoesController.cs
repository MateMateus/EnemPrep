using EnemPrep.Api.DTOs;
using EnemPrep.Api.Extensions;
using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Questoes;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api")]
public class QuestoesController : ControllerBase
{
    private readonly IQuestaoService _questaoService;
    private readonly ILogger<QuestoesController> _logger;

    public QuestoesController(IQuestaoService questaoService, ILogger<QuestoesController> logger)
    {
        _questaoService = questaoService;
        _logger = logger;
    }

    [HttpGet("questoes/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _questaoService.GetByIdAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<QuestaoDto>.Ok(result.Data!));
    }

    [HttpGet("assuntos/{assuntoId:guid}/questoes")]
    public async Task<IActionResult> GetByAssunto(Guid assuntoId, [FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await _questaoService.GetPagedByAssuntoAsync(assuntoId, request, cancellationToken);
        return Ok(ApiResponse<PagedResult<QuestaoDto>>.Ok(result.Data!));
    }

    [HttpGet("temas/{temaId:guid}/questoes")]
    public async Task<IActionResult> GetByTema(Guid temaId, [FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await _questaoService.GetPagedByTemaAsync(temaId, request, cancellationToken);
        return Ok(ApiResponse<PagedResult<QuestaoDto>>.Ok(result.Data!));
    }


    [HttpPost("questoes")]
    public async Task<IActionResult> Criar(
        [FromForm] CriarQuestaoFormRequest form, 
        [FromServices] IFileStorageService storageService,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Criando questão: Enunciado='{Enunciado}', Dificuldade={Dificuldade}, AssuntoId={AssuntoId}, Alternativas={Count}, TemImagem={TemImagem}",
            form.Enunciado?[..Math.Min(50, form.Enunciado?.Length ?? 0)],
            form.Dificuldade, form.AssuntoId, form.Alternativas?.Count ?? 0, form.ImagemArquivo != null);

        string? urlImagem = form.ImagemUrl;

        if (form.ImagemArquivo != null && form.ImagemArquivo.Length > 0)
        {
            using var stream = form.ImagemArquivo.OpenReadStream();
            urlImagem = await storageService.SaveFileAsync(stream, form.ImagemArquivo.FileName, "questoes", cancellationToken);
        }

        var alternativasList = string.IsNullOrWhiteSpace(form.AlternativasJson) ? new List<AlternativaFormItem>() : System.Text.Json.JsonSerializer.Deserialize<List<AlternativaFormItem>>(form.AlternativasJson, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var alternativas = (alternativasList ?? [])
            .Select(a => new CriarAlternativaRequest(a.Texto, a.Correta))
            .ToList();

        var request = new CriarQuestaoRequest(
            form.Enunciado ?? string.Empty,
            form.Dificuldade,
            form.AssuntoId,
            form.Explicacao,
            form.VideoExplicacaoUrl,
            urlImagem,
            alternativas,
            form.LivroId,
            form.LivroTemaId);

        var result = await _questaoService.CriarAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<QuestaoDto>.Ok(result.Data!));
    }

    [HttpPut("questoes/{id:guid}")]
    public async Task<IActionResult> Atualizar(
        Guid id, 
        [FromForm] AtualizarQuestaoFormRequest form, 
        [FromServices] IFileStorageService storageService,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Atualizando questão {Id}: Alternativas={Count}, TemImagem={TemImagem}",
            id, form.Alternativas?.Count ?? 0, form.ImagemArquivo != null);

        string? urlImagem = form.ImagemUrl;

        if (form.ImagemArquivo != null && form.ImagemArquivo.Length > 0)
        {
            using var stream = form.ImagemArquivo.OpenReadStream();
            urlImagem = await storageService.SaveFileAsync(stream, form.ImagemArquivo.FileName, "questoes", cancellationToken);
        }

        var alternativasList = string.IsNullOrWhiteSpace(form.AlternativasJson) ? new List<AlternativaFormItem>() : System.Text.Json.JsonSerializer.Deserialize<List<AlternativaFormItem>>(form.AlternativasJson, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var alternativas = (alternativasList ?? [])
            .Select(a => new CriarAlternativaRequest(a.Texto, a.Correta))
            .ToList();

        var request = new AtualizarQuestaoRequest(
            form.Enunciado ?? string.Empty,
            form.Dificuldade,
            form.Explicacao,
            form.VideoExplicacaoUrl,
            urlImagem,
            alternativas,
            form.LivroId,
            form.LivroTemaId);

        var result = await _questaoService.AtualizarAsync(id, request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<QuestaoDto>.Ok(result.Data!));
    }

    [HttpPost("questoes/responder")]
    public async Task<IActionResult> Responder([FromHeader(Name = "X-Usuario-Id")] Guid usuarioId, [FromBody] ResponderQuestaoRequest request, CancellationToken cancellationToken)
    {
        var result = await _questaoService.ResponderAsync(usuarioId, request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<ResultadoQuestaoDto>.Ok(result.Data!));
    }

    [HttpGet("questoes/tentativas")]
    public async Task<IActionResult> GetHistorico([FromHeader(Name = "X-Usuario-Id")] Guid usuarioId, CancellationToken cancellationToken)
    {
        var result = await _questaoService.GetHistoricoAsync(usuarioId, cancellationToken);

        return Ok(ApiResponse<IEnumerable<TentativaQuestaoDto>>.Ok(result.Data!));
    }

    [HttpDelete("questoes/{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var result = await _questaoService.DeletarAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}

