using System.Net.Http.Json;
using EnemPrep.Web.Models.Gamificacao;
using EnemPrep.Web.Services.ApiClients.Interfaces;

namespace EnemPrep.Web.Services.ApiClients;

public class GamificacaoApiClient : IGamificacaoApiClient
{
    private readonly HttpClient _httpClient;
    public GamificacaoApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DesafioDiarioViewModel?> GetDesafioDiarioAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/Gamificacao/desafio-diario", cancellationToken);
            // 204 No Content => sem desafio pra hoje
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent || !response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<DesafioDiarioViewModel>(cancellationToken: cancellationToken);
        }
        catch { return null; }
    }
}
