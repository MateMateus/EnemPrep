using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.Materiais;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api")]
public class MateriaisController : ControllerBase
{
    private readonly IMaterialEstudoService _materialService;

    public MateriaisController(IMaterialEstudoService materialService)
    {
        _materialService = materialService;
    }

    [HttpGet("assuntos/{assuntoId:guid}/materiais")]
    public async Task<IActionResult> GetByAssunto(Guid assuntoId, CancellationToken cancellationToken)
    {
        var result = await _materialService.GetByAssuntoIdAsync(assuntoId, cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<MaterialEstudoDto>>.Ok(result.Data!));
    }

    [HttpPost("materiais")]
    public async Task<IActionResult> Criar([FromBody] CriarMaterialRequest request, CancellationToken cancellationToken)
    {
        var result = await _materialService.CriarAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<MaterialEstudoDto>.Ok(result.Data!));
    }

    [HttpDelete("materiais/{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var result = await _materialService.DeletarAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}
