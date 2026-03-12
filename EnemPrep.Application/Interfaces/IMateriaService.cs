using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Materias;

namespace EnemPrep.Application.Interfaces;

public interface IMateriaService
{
    Task<Result<IReadOnlyList<MateriaDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MateriaComAssuntosDto>> GetByIdComAssuntosAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<MateriaDto>> CriarAsync(CriarMateriaRequest request, CancellationToken cancellationToken = default);
    Task<Result> AtualizarAsync(Guid id, AtualizarMateriaRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken = default);
}
