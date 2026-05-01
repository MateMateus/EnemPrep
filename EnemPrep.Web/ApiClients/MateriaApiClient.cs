using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IMateriaApiClient
{
    Task<List<MateriaViewModel>> GetAllAsync(CancellationToken ct = default);
    Task<MateriaComAssuntosViewModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<MateriaViewModel?> CriarAsync(string nome, string? descricao, CancellationToken ct = default);
    Task<bool> AtualizarAsync(Guid id, string nome, string? descricao, CancellationToken ct = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken ct = default);
}

public class MateriaApiClient(HttpClient http, IMemoryCache cache) : IMateriaApiClient
{
    public async Task<List<MateriaViewModel>> GetAllAsync(CancellationToken ct = default)
    {
        if (cache.TryGetValue("Cache_Materias", out List<MateriaViewModel>? cachedData))
            return cachedData!;

        try
        {
            var response = await http.GetFromJsonAsync<ApiResponse<List<MateriaViewModel>>>("api/materias", ct);
            var data = response?.Data ?? [];
            if (data.Any())
                cache.Set("Cache_Materias", data, TimeSpan.FromMinutes(30));
            return data;
        }
        catch { return []; }
    }

    public async Task<MateriaComAssuntosViewModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await http.GetFromJsonAsync<ApiResponse<MateriaComAssuntosViewModel>>($"api/materias/{id}", ct);
            return response?.Data;
        }
        catch { return null; }
    }

    public async Task<MateriaViewModel?> CriarAsync(string nome, string? descricao, CancellationToken ct = default)
    {
        var httpResponse = await http.PostAsJsonAsync("api/materias", new { Nome = nome, Descricao = descricao }, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;
        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<MateriaViewModel>>(ct);
        cache.Remove("Cache_Materias");
        return response?.Data;
    }

    public async Task<bool> AtualizarAsync(Guid id, string nome, string? descricao, CancellationToken ct = default)
    {
        var httpResponse = await http.PutAsJsonAsync($"api/materias/{id}", new { Nome = nome, Descricao = descricao }, ct);
        if (httpResponse.IsSuccessStatusCode) cache.Remove("Cache_Materias");
        return httpResponse.IsSuccessStatusCode;
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        var httpResponse = await http.DeleteAsync($"api/materias/{id}", ct);
        if (httpResponse.IsSuccessStatusCode) cache.Remove("Cache_Materias");
        return httpResponse.IsSuccessStatusCode;
    }
}
