using System.Net.Http.Json;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IDashboardApiClient
{
    Task<DashboardViewModel?> GetDashboardAsync(Guid usuarioId, CancellationToken ct = default);
}

public class DashboardApiClient(HttpClient http) : IDashboardApiClient
{
    public async Task<DashboardViewModel?> GetDashboardAsync(Guid usuarioId, CancellationToken ct = default)
    {
        try
        {
            var response = await http.GetFromJsonAsync<ApiResponse<DashboardViewModel>>($"api/dashboard/{usuarioId}", ct);
            return response?.Data;
        }
        catch { return null; }
    }
}
