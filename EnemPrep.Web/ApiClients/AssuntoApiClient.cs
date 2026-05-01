using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IAssuntoApiClient
{
    Task<List<AssuntoViewModel>> GetByMateriaAsync(Guid materiaId, CancellationToken ct = default);
    Task<AssuntoViewModel?> CriarAsync(string nome, string descricao, Guid materiaId, CancellationToken ct = default);
    Task<bool> AtualizarAsync(Guid id, string nome, string descricao, Guid materiaId, CancellationToken ct = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken ct = default);
}

public class AssuntoApiClient(HttpClient http, IMemoryCache cache) : IAssuntoApiClient
{
    public async Task<List<AssuntoViewModel>> GetByMateriaAsync(Guid materiaId, CancellationToken ct = default)
    {
        var cacheKey = $"Cache_Assuntos_{materiaId}";
        if (cache.TryGetValue(cacheKey, out List<AssuntoViewModel>? cachedData))
            return cachedData!;

        try
        {
            var response = await http.GetFromJsonAsync<ApiResponse<List<AssuntoViewModel>>>($"api/materias/{materiaId}/assuntos", ct);
            var data = response?.Data ?? [];
            if (data.Any())
                cache.Set(cacheKey, data, TimeSpan.FromMinutes(30));
            return data;
        }
        catch { return []; }
    }

    public async Task<AssuntoViewModel?> CriarAsync(string nome, string descricao, Guid materiaId, CancellationToken ct = default)
    {
        var httpResponse = await http.PostAsJsonAsync("api/assuntos", new { Nome = nome, Descricao = descricao ?? "", MateriaId = materiaId }, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;
        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<AssuntoViewModel>>(ct);
        cache.Remove($"Cache_Assuntos_{materiaId}");
        return response?.Data;
    }

    public async Task<bool> AtualizarAsync(Guid id, string nome, string descricao, Guid materiaId, CancellationToken ct = default)
    {
        var httpResponse = await http.PutAsJsonAsync($"api/assuntos/{id}", new { Nome = nome, Descricao = descricao ?? "", MateriaId = materiaId }, ct);
        if (httpResponse.IsSuccessStatusCode) cache.Remove($"Cache_Assuntos_{materiaId}");
        return httpResponse.IsSuccessStatusCode;
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        // Precisamos limpar o cache de todas as materias já que não sabemos a materia do assunto aqui sem fazer outro GET
        // Vamos deixar expirar sozinho ou se o admin deletar, vai expirar em 30 min.
        var httpResponse = await http.DeleteAsync($"api/assuntos/{id}", ct);
        return httpResponse.IsSuccessStatusCode;
    }
}
