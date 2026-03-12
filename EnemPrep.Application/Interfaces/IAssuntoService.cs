using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Materias;

namespace EnemPrep.Application.Interfaces;

public interface IAssuntoService
{
    Task<Result<IReadOnlyList<AssuntoDto>>> GetByMateriaIdAsync(Guid materiaId, CancellationToken cancellationToken = default);
    Task<Result<AssuntoDto>> CriarAsync(CriarAssuntoRequest request, CancellationToken cancellationToken = default);
    Task<Result> AtualizarAsync(Guid id, CriarAssuntoRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken = default);
}
