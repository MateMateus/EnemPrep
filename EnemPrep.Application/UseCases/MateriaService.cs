using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Materias;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class MateriaService : IMateriaService
{
    private readonly IMateriaRepository _materiaRepository;

    public MateriaService(IMateriaRepository materiaRepository)
    {
        _materiaRepository = materiaRepository;
    }

    public async Task<Result<IReadOnlyList<MateriaDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var materias = await _materiaRepository.GetAllAsync(cancellationToken);

        var dtos = materias.Select(m => new MateriaDto(
            m.Id, m.Nome, m.Descricao, m.Assuntos.Count)).ToList();

        return Result<IReadOnlyList<MateriaDto>>.Ok(dtos);
    }

    public async Task<Result<MateriaComAssuntosDto>> GetByIdComAssuntosAsync(Guid id, CancellationToken cancellationToken)
    {
        var materia = await _materiaRepository.GetByIdComAssuntosAsync(id, cancellationToken);

        if (materia is null)
            return Result<MateriaComAssuntosDto>.Fail("Matéria não encontrada.");

        var assuntoDtos = materia.Assuntos
            .Select(a => new AssuntoDto(a.Id, a.Nome, a.Descricao, a.MateriaId))
            .ToList();

        var dto = new MateriaComAssuntosDto(materia.Id, materia.Nome, materia.Descricao, assuntoDtos);

        return Result<MateriaComAssuntosDto>.Ok(dto);
    }

    public async Task<Result<MateriaDto>> CriarAsync(CriarMateriaRequest request, CancellationToken cancellationToken)
    {
        var materia = new Materia(request.Nome, request.Descricao);
        await _materiaRepository.AddAsync(materia, cancellationToken);

        return Result<MateriaDto>.Ok(new MateriaDto(materia.Id, materia.Nome, materia.Descricao, 0));
    }

    public async Task<Result> AtualizarAsync(Guid id, AtualizarMateriaRequest request, CancellationToken cancellationToken)
    {
        var materia = await _materiaRepository.GetByIdAsync(id, cancellationToken);

        if (materia is null)
            return Result.Fail("Matéria não encontrada.");

        await _materiaRepository.UpdateAsync(materia, cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var materia = await _materiaRepository.GetByIdAsync(id, cancellationToken);

        if (materia is null)
            return Result.Fail("Matéria não encontrada.");

        await _materiaRepository.DeleteAsync(materia, cancellationToken);

        return Result.Ok();
    }
}
