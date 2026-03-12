using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Models.Simulados;
using EnemPrep.Web.Services.ApiClients.Interfaces;
using System.Net.Http.Json;

namespace EnemPrep.Web.Services.ApiClients;

public class SimuladoApiClient : ISimuladoApiClient
{
    private readonly HttpClient _httpClient;

    public SimuladoApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private void AddAuthHeader(Guid usuarioId)
    {
        _httpClient.DefaultRequestHeaders.Remove("X-Usuario-Id");
        _httpClient.DefaultRequestHeaders.Add("X-Usuario-Id", usuarioId.ToString());
    }

    public async Task<IEnumerable<SimuladoResumo>> GetSimuladosDisponiveisAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<SimuladoResumo>>>("api/simulados");
        return response?.Data ?? Array.Empty<SimuladoResumo>();
    }

    public async Task<SimuladoDetalhe?> GetSimuladoByIdAsync(Guid id)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<SimuladoDetalhe>>($"api/simulados/{id}");
        return response?.Data;
    }

    public async Task<TentativaSimuladoResult?> IniciarSimuladoAsync(Guid usuarioId, IniciarSimuladoRequest request)
    {
        AddAuthHeader(usuarioId);
        var response = await _httpClient.PostAsJsonAsync("api/simulados/iniciar", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TentativaSimuladoResult>>();
            return result?.Data;
        }
        return null;
    }

    public async Task<ApiResponse<TentativaSimuladoResult>?> SubmeterSimuladoAsync(Guid usuarioId, Guid tentativaId, SubmeterSimuladoRequest request)
    {
        AddAuthHeader(usuarioId);
        var response = await _httpClient.PostAsJsonAsync($"api/simulados/tentativas/{tentativaId}/submeter", request);

        if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<TentativaSimuladoResult>>();
            return result;
        }
        return null;
    }

    public async Task<IEnumerable<TentativaSimuladoResult>> GetHistoricoTentativasAsync(Guid usuarioId)
    {
        AddAuthHeader(usuarioId);
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<TentativaSimuladoResult>>>("api/simulados/historico");
            return response?.Data ?? Array.Empty<TentativaSimuladoResult>();
        }
        catch
        {
            return Array.Empty<TentativaSimuladoResult>();
        }
    }
}
