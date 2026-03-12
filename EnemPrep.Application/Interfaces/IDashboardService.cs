using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Dashboard;

namespace EnemPrep.Application.Interfaces;

public interface IDashboardService
{
    Task<Result<DashboardDto>> GetDashboardAsync(Guid usuarioId, CancellationToken cancellationToken = default);
}
