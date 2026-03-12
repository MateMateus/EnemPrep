using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Materiais;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class MaterialEstudoService : IMaterialEstudoService
{
    private readonly IMaterialEstudoRepository _materialRepository;

    public MaterialEstudoService(IMaterialEstudoRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<Result<IReadOnlyList<MaterialEstudoDto>>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken)
    {
        var materiais = await _materialRepository.GetByAssuntoIdAsync(assuntoId, cancellationToken);

        var dtos = materiais
            .Select(m => new MaterialEstudoDto(m.Id, m.Titulo, m.Tipo, m.UrlArquivo, m.AssuntoId, m.Assunto?.Nome ?? string.Empty))
            .ToList();

        return Result<IReadOnlyList<MaterialEstudoDto>>.Ok(dtos);
    }

    public async Task<Result<MaterialEstudoDto>> CriarAsync(CriarMaterialRequest request, CancellationToken cancellationToken)
    {
        var material = new MaterialEstudo(request.Titulo, request.Tipo, request.UrlArquivo, request.AssuntoId);
        await _materialRepository.AddAsync(material, cancellationToken);

        return Result<MaterialEstudoDto>.Ok(
            new MaterialEstudoDto(material.Id, material.Titulo, material.Tipo, material.UrlArquivo, material.AssuntoId, string.Empty));
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(id, cancellationToken);

        if (material is null)
            return Result.Fail("Material de estudo não encontrado.");

        await _materialRepository.DeleteAsync(material, cancellationToken);

        return Result.Ok();
    }
}
