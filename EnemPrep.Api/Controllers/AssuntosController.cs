using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.Materias;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api")]
public class AssuntosController : ControllerBase
{
    private readonly IAssuntoService _assuntoService;

    public AssuntosController(IAssuntoService assuntoService)
    {
        _assuntoService = assuntoService;
    }

    [HttpGet("materias/{materiaId:guid}/assuntos")]
    public async Task<IActionResult> GetByMateria(Guid materiaId, CancellationToken cancellationToken)
    {
        var result = await _assuntoService.GetByMateriaIdAsync(materiaId, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<AssuntoDto>>.Ok(result.Data!));
    }

    [Authorize]
    [HttpPost("assuntos")]
    public async Task<IActionResult> Criar([FromBody] CriarAssuntoRequest request, CancellationToken cancellationToken)
    {
        var result = await _assuntoService.CriarAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<AssuntoDto>.Ok(result.Data!));
    }

    [Authorize]
    [HttpPut("assuntos/{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarAssuntoRequest request, CancellationToken cancellationToken)
    {
        var result = await _assuntoService.AtualizarAsync(id, request, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [Authorize]
    [HttpDelete("assuntos/{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var result = await _assuntoService.DeletarAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}
