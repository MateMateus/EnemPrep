using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnemPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/gamificacao")]
public class GamificacaoController : ControllerBase
{
    private readonly IGamificacaoService _gamificacaoService;

    public GamificacaoController(IGamificacaoService gamificacaoService)
    {
        _gamificacaoService = gamificacaoService;
    }

    [HttpGet("streak")]
    public async Task<IActionResult> GetStreak(CancellationToken cancellationToken)
    {
        var result = await _gamificacaoService.GetStreakAsync(GetUsuarioId(), cancellationToken);
        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("desafio-diario")]
    public async Task<IActionResult> GetDesafioDiario(CancellationToken cancellationToken)
    {
        // Pega o desafio do dia atual considerando o relógio do servidor (UTC)
        var result = await _gamificacaoService.GetDesafioDiarioAsync(GetUsuarioId(), DateTime.UtcNow, cancellationToken);
        // Pode retornar 204 No Content se não houver desafio para hoje
        return result.Success ? (result.Data != null ? Ok(result.Data) : NoContent()) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("conquistas")]
    public async Task<IActionResult> GetConquistas(CancellationToken cancellationToken)
    {
        var result = await _gamificacaoService.GetConquistasDoUsuarioAsync(GetUsuarioId(), cancellationToken);
        return result.Success ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    private Guid GetUsuarioId()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return idClaim != null ? Guid.Parse(idClaim) : Guid.Empty;
    }
}
