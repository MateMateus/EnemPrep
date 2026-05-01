using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.Dashboard;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("{usuarioId:guid}")]
    public async Task<IActionResult> GetDashboard(Guid usuarioId, CancellationToken cancellationToken)
    {
        var result = await _dashboardService.GetDashboardAsync(usuarioId, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<DashboardDto>.Ok(result.Data!));
    }
}
