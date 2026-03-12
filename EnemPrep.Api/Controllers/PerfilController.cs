using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.Perfil;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api/perfil")]
public class PerfilController : ControllerBase
{
    private readonly IPerfilService _perfilService;

    public PerfilController(IPerfilService perfilService)
    {
        _perfilService = perfilService;
    }

    [HttpGet("{usuarioId:guid}")]
    public async Task<IActionResult> GetPerfil(Guid usuarioId, CancellationToken cancellationToken)
    {
        var result = await _perfilService.GetPerfilAsync(usuarioId, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<PerfilUsuarioDto>.Ok(result.Data!));
    }

    [HttpPut("{usuarioId:guid}/nome")]
    public async Task<IActionResult> AtualizarNome(Guid usuarioId, [FromBody] AtualizarNomeRequest request, CancellationToken cancellationToken)
    {
        var result = await _perfilService.AtualizarNomeAsync(usuarioId, request.NovoNome, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpPut("{usuarioId:guid}/email")]
    public async Task<IActionResult> AtualizarEmail(Guid usuarioId, [FromBody] AtualizarEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _perfilService.AtualizarEmailAsync(usuarioId, request.NovoEmail, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpPut("{usuarioId:guid}/senha")]
    public async Task<IActionResult> AtualizarSenha(Guid usuarioId, [FromBody] AtualizarSenhaRequest request, CancellationToken cancellationToken)
    {
        var result = await _perfilService.AtualizarSenhaAsync(usuarioId, request.SenhaAtual, request.NovaSenha, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}

public record AtualizarNomeRequest(string NovoNome);
public record AtualizarEmailRequest(string NovoEmail);
public record AtualizarSenhaRequest(string SenhaAtual, string NovaSenha);
