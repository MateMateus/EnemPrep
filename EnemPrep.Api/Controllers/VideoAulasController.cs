using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.VideoAulas;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api")]
public class VideoAulasController : ControllerBase
{
    private readonly IVideoAulaService _videoAulaService;

    public VideoAulasController(IVideoAulaService videoAulaService)
    {
        _videoAulaService = videoAulaService;
    }

    [HttpGet("assuntos/{assuntoId:guid}/videoaulas")]
    public async Task<IActionResult> GetByAssunto(Guid assuntoId, CancellationToken cancellationToken)
    {
        var result = await _videoAulaService.GetByAssuntoIdAsync(assuntoId, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<VideoAulaDto>>.Ok(result.Data!));
    }

    [HttpPost("videoaulas")]
    public async Task<IActionResult> Criar([FromBody] CriarVideoAulaRequest request, CancellationToken cancellationToken)
    {
        var result = await _videoAulaService.CriarAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<VideoAulaDto>.Ok(result.Data!));
    }

    [HttpDelete("videoaulas/{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var result = await _videoAulaService.DeletarAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}
