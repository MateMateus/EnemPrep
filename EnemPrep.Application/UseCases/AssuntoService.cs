using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Materias;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class AssuntoService : IAssuntoService
{
    private readonly IAssuntoRepository _assuntoRepository;

    public AssuntoService(IAssuntoRepository assuntoRepository)
    {
        _assuntoRepository = assuntoRepository;
    }

    public async Task<Result<IReadOnlyList<AssuntoDto>>> GetByMateriaIdAsync(Guid materiaId, CancellationToken cancellationToken)
    {
        var assuntos = await _assuntoRepository.GetByMateriaIdAsync(materiaId, cancellationToken);

        var dtos = assuntos
            .Select(a => new AssuntoDto(a.Id, a.Nome, a.Descricao, a.MateriaId))
            .ToList();

        return Result<IReadOnlyList<AssuntoDto>>.Ok(dtos);
    }

    public async Task<Result<AssuntoDto>> CriarAsync(CriarAssuntoRequest request, CancellationToken cancellationToken)
    {
        var assunto = new Assunto(request.Nome, request.Descricao, request.MateriaId);
        await _assuntoRepository.AddAsync(assunto, cancellationToken);

        return Result<AssuntoDto>.Ok(new AssuntoDto(assunto.Id, assunto.Nome, assunto.Descricao, assunto.MateriaId));
    }

    public async Task<Result> AtualizarAsync(Guid id, CriarAssuntoRequest request, CancellationToken cancellationToken)
    {
        var assunto = await _assuntoRepository.GetByIdAsync(id, cancellationToken);

        if (assunto is null)
            return Result.Fail("Assunto não encontrado.");

        await _assuntoRepository.UpdateAsync(assunto, cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var assunto = await _assuntoRepository.GetByIdAsync(id, cancellationToken);

        if (assunto is null)
            return Result.Fail("Assunto não encontrado.");

        await _assuntoRepository.DeleteAsync(assunto, cancellationToken);

        return Result.Ok();
    }
}
