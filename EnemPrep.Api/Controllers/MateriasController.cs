using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.Materias;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api/materias")]
public class MateriasController : ControllerBase
{
    private readonly IMateriaService _materiaService;

    public MateriasController(IMateriaService materiaService)
    {
        _materiaService = materiaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _materiaService.GetAllAsync(cancellationToken);

        return Ok(ApiResponse<IReadOnlyList<MateriaDto>>.Ok(result.Data!));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _materiaService.GetByIdComAssuntosAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<MateriaComAssuntosDto>.Ok(result.Data!));
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarMateriaRequest request, CancellationToken cancellationToken)
    {
        var result = await _materiaService.CriarAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, ApiResponse<MateriaDto>.Ok(result.Data!));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarMateriaRequest request, CancellationToken cancellationToken)
    {
        var result = await _materiaService.AtualizarAsync(id, request, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken cancellationToken)
    {
        var result = await _materiaService.DeletarAsync(id, cancellationToken);

        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}
