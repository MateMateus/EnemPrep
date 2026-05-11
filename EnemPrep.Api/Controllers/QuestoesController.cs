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

    public QuestoesController(IQuestaoService questaoService)
    {
        _questaoService = questaoService;
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
        [FromForm] CriarQuestaoRequest request, 
        [FromForm] IFormFile? imagemArquivo,
        [FromServices] IFileStorageService storageService,
        CancellationToken cancellationToken)
    {
        string? urlImagem = request.ImagemUrl;

        if (imagemArquivo != null && imagemArquivo.Length > 0)
        {
            using var stream = imagemArquivo.OpenReadStream();
            urlImagem = await storageService.SaveFileAsync(stream, imagemArquivo.FileName, "questoes", cancellationToken);
        }

        var reqWithImage = request with { ImagemUrl = urlImagem };

        var result = await _questaoService.CriarAsync(reqWithImage, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<QuestaoDto>.Ok(result.Data!));
    }

    [HttpPut("questoes/{id:guid}")]
    public async Task<IActionResult> Atualizar(
        Guid id, 
        [FromForm] AtualizarQuestaoRequest request, 
        [FromForm] IFormFile? imagemArquivo,
        [FromServices] IFileStorageService storageService,
        CancellationToken cancellationToken)
    {
        string? urlImagem = request.ImagemUrl;

        if (imagemArquivo != null && imagemArquivo.Length > 0)
        {
            using var stream = imagemArquivo.OpenReadStream();
            urlImagem = await storageService.SaveFileAsync(stream, imagemArquivo.FileName, "questoes", cancellationToken);
        }

        var reqWithImage = request with { ImagemUrl = urlImagem };

        var result = await _questaoService.AtualizarAsync(id, reqWithImage, cancellationToken);

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
