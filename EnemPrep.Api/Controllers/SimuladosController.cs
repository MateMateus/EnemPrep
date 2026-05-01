using EnemPrep.Api.Extensions;
using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Simulados;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimuladosController : ControllerBase
{
    private readonly ISimuladoService _simuladoService;

    public SimuladosController(ISimuladoService simuladoService)
    {
        _simuladoService = simuladoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSimuladosDisponiveis(CancellationToken cancellationToken)
    {
        var result = await _simuladoService.GetSimuladosDisponiveisAsync(cancellationToken);

        return Ok(ApiResponse<IEnumerable<SimuladoResumoDto>>.Ok(result.Data!));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _simuladoService.GetSimuladoByIdAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<SimuladoDetalheDto>.Ok(result.Data!));
    }

    [Authorize]
    [HttpPost("iniciar")]
    public async Task<IActionResult> Iniciar([FromHeader(Name = "X-Usuario-Id")] Guid usuarioId, [FromBody] IniciarSimuladoDto request, CancellationToken cancellationToken)
    {
        var result = await _simuladoService.IniciarSimuladoAsync(usuarioId, request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<TentativaSimuladoResultDto>.Ok(result.Data!));
    }

    [Authorize]
    [HttpPost("tentativas/{tentativaId:guid}/submeter")]
    public async Task<IActionResult> Submeter([FromHeader(Name = "X-Usuario-Id")] Guid usuarioId, Guid tentativaId, [FromBody] SubmeterSimuladoDto request, CancellationToken cancellationToken)
    {
        var result = await _simuladoService.SubmeterSimuladoAsync(usuarioId, tentativaId, request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<TentativaSimuladoResultDto>.Ok(result.Data!));
    }

    [Authorize]
    [HttpGet("historico")]
    public async Task<IActionResult> GetHistorico([FromHeader(Name = "X-Usuario-Id")] Guid usuarioId, CancellationToken cancellationToken)
    {
        var result = await _simuladoService.GetHistoricoTentativasAsync(usuarioId, cancellationToken);

        return Ok(ApiResponse<IEnumerable<TentativaSimuladoResultDto>>.Ok(result.Data!));
    }
}
