using System.Net.Http.Json;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IAssuntoApiClient
{
    Task<List<AssuntoViewModel>> GetByMateriaAsync(Guid materiaId, CancellationToken ct = default);
    Task<AssuntoViewModel?> CriarAsync(string nome, string descricao, Guid materiaId, CancellationToken ct = default);
    Task<bool> AtualizarAsync(Guid id, string nome, string descricao, Guid materiaId, CancellationToken ct = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken ct = default);
}

public class AssuntoApiClient(HttpClient http) : IAssuntoApiClient
{
    public async Task<List<AssuntoViewModel>> GetByMateriaAsync(Guid materiaId, CancellationToken ct = default)
    {
        try
        {
            var response = await http.GetFromJsonAsync<ApiResponse<List<AssuntoViewModel>>>($"api/materias/{materiaId}/assuntos", ct);
            return response?.Data ?? [];
        }
        catch { return []; }
    }

    public async Task<AssuntoViewModel?> CriarAsync(string nome, string descricao, Guid materiaId, CancellationToken ct = default)
    {
        var httpResponse = await http.PostAsJsonAsync("api/assuntos", new { Nome = nome, Descricao = descricao ?? "", MateriaId = materiaId }, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;
        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<AssuntoViewModel>>(ct);
        return response?.Data;
    }

    public async Task<bool> AtualizarAsync(Guid id, string nome, string descricao, Guid materiaId, CancellationToken ct = default)
    {
        var httpResponse = await http.PutAsJsonAsync($"api/assuntos/{id}", new { Nome = nome, Descricao = descricao ?? "", MateriaId = materiaId }, ct);
        return httpResponse.IsSuccessStatusCode;
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        var httpResponse = await http.DeleteAsync($"api/assuntos/{id}", ct);
        return httpResponse.IsSuccessStatusCode;
    }
}
