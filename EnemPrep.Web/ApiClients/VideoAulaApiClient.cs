using System.Net.Http.Json;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IVideoAulaApiClient
{
    Task<PagedResult<VideoAulaViewModel>> GetByAssuntoAsync(Guid assuntoId, int page = 1, int pageSize = 10, CancellationToken ct = default);
    Task<VideoAulaViewModel?> CriarAsync(string titulo, string urlVideo, int duracaoSegundos, Guid assuntoId, CancellationToken ct = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken ct = default);
}

public class VideoAulaApiClient(HttpClient http) : IVideoAulaApiClient
{
    public async Task<PagedResult<VideoAulaViewModel>> GetByAssuntoAsync(Guid assuntoId, int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        var response = await http.GetFromJsonAsync<ApiResponse<PagedResult<VideoAulaViewModel>>>($"api/assuntos/{assuntoId}/videoaulas?page={page}&pageSize={pageSize}", ct);
        return response?.Data ?? new PagedResult<VideoAulaViewModel>([], 0, page, pageSize);
    }

    public async Task<VideoAulaViewModel?> CriarAsync(
        string titulo, string urlVideo, int duracaoSegundos, Guid assuntoId,
        CancellationToken ct = default)
    {
        var httpResponse = await http.PostAsJsonAsync("api/videoaulas", new
        {
            Titulo = titulo,
            UrlVideo = urlVideo,
            DuracaoSegundos = duracaoSegundos,
            AssuntoId = assuntoId
        }, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;
        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<VideoAulaViewModel>>(ct);
        return response?.Data;
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        var httpResponse = await http.DeleteAsync($"api/videoaulas/{id}", ct);
        return httpResponse.IsSuccessStatusCode;
    }
}
