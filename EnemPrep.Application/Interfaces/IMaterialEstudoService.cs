using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Materiais;

namespace EnemPrep.Application.Interfaces;

public interface IMaterialEstudoService
{
    Task<Result<IReadOnlyList<MaterialEstudoDto>>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken = default);
    Task<Result<MaterialEstudoDto>> CriarAsync(CriarMaterialRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken = default);
}
