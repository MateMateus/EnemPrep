using System.Net.Http.Json;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IMaterialApiClient
{
    Task<List<MaterialViewModel>> GetByAssuntoAsync(Guid assuntoId, CancellationToken ct = default);
    Task<MaterialViewModel?> CriarAsync(string titulo, int tipo, string urlArquivo, Guid assuntoId, CancellationToken ct = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken ct = default);
}

public class MaterialApiClient(HttpClient http) : IMaterialApiClient
{
    public async Task<List<MaterialViewModel>> GetByAssuntoAsync(Guid assuntoId, CancellationToken ct = default)
    {
        var response = await http.GetFromJsonAsync<ApiResponse<List<MaterialViewModel>>>($"api/assuntos/{assuntoId}/materiais", ct);
        return response?.Data ?? [];
    }

    public async Task<MaterialViewModel?> CriarAsync(
        string titulo, int tipo, string urlArquivo, Guid assuntoId,
        CancellationToken ct = default)
    {
        var httpResponse = await http.PostAsJsonAsync("api/materiais", new
        {
            Titulo = titulo,
            Tipo = tipo,
            UrlArquivo = urlArquivo,
            AssuntoId = assuntoId
        }, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;
        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<MaterialViewModel>>(ct);
        return response?.Data;
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        var httpResponse = await http.DeleteAsync($"api/materiais/{id}", ct);
        return httpResponse.IsSuccessStatusCode;
    }
}
