using System.Net.Http.Json;
using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Models;
using EnemPrep.Web.Models.PlanosEstudo;
using EnemPrep.Web.Services.ApiClients.Interfaces;

namespace EnemPrep.Web.Services.ApiClients;

public class PlanoEstudoApiClient : IPlanoEstudoApiClient
{
    private readonly HttpClient _httpClient;

    public PlanoEstudoApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<PlanoEstudoViewModel>> ObterPorUsuarioAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<IReadOnlyList<PlanoEstudoViewModel>>>($"/api/usuarios/{usuarioId}/planos", cancellationToken);
        return response?.Data ?? Array.Empty<PlanoEstudoViewModel>();
    }

    public async Task<PlanoEstudoViewModel?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/api/planos/{id}", cancellationToken);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<PlanoEstudoViewModel>>(cancellationToken: cancellationToken);
        return result?.Data;
    }

    public async Task<bool> CriarAsync(Guid usuarioId, CriarPlanoEstudoViewModel request, CancellationToken cancellationToken = default)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/planos");
        httpRequest.Headers.Add("X-Usuario-Id", usuarioId.ToString());

        // Map to anonymous object that matches the API request shape
        var apiRequest = new
        {
            Titulo = request.Titulo,
            DataInicio = request.DataInicio,
            DataFim = request.DataFim,
            Itens = request.ItensSelecionados.Select(i => new { AssuntoId = i.AssuntoId, DataPrevista = i.DataPrevista }).ToList()
        };

        httpRequest.Content = JsonContent.Create(apiRequest);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> AtualizarStatusItemAsync(Guid itemId, EnemPrep.Web.Enums.StatusPlanoItem novoStatus, CancellationToken cancellationToken = default)
    {
        var request = new { NovoStatus = novoStatus };
        var response = await _httpClient.PatchAsJsonAsync($"/api/planos/itens/{itemId}/status", request, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"/api/planos/{id}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
