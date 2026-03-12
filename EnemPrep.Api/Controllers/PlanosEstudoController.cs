using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.PlanoEstudo;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api")]
public class PlanosEstudoController : ControllerBase
{
    private readonly IPlanoEstudoService _planoService;

    public PlanosEstudoController(IPlanoEstudoService planoService)
    {
        _planoService = planoService;
    }

    [HttpGet("usuarios/{usuarioId:guid}/planos")]
    public async Task<IActionResult> GetByUsuario(Guid usuarioId, CancellationToken cancellationToken)
    {
        var result = await _planoService.GetByUsuarioIdAsync(usuarioId, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<PlanoEstudoDto>>.Ok(result.Data!));
    }

    [HttpGet("planos/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _planoService.GetByIdAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<PlanoEstudoDto>.Ok(result.Data!));
    }

    [HttpPost("planos")]
    public async Task<IActionResult> Criar([FromHeader(Name = "X-Usuario-Id")] Guid usuarioId, [FromBody] CriarPlanoEstudoRequest request, CancellationToken cancellationToken)
    {
        var result = await _planoService.CriarAsync(usuarioId, request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<PlanoEstudoDto>.Ok(result.Data!));
    }

    [HttpPatch("planos/itens/{itemId:guid}/status")]
    public async Task<IActionResult> AtualizarStatusItem(Guid itemId, [FromBody] AtualizarStatusPlanoItemRequest request, CancellationToken cancellationToken)
    {
        var result = await _planoService.AtualizarStatusItemAsync(itemId, request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpDelete("planos/{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var result = await _planoService.DeletarAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}
