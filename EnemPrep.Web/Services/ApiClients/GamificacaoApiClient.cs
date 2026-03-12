using System.Net.Http.Json;
using EnemPrep.Web.Models.Gamificacao;
using EnemPrep.Web.Services.ApiClients.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace EnemPrep.Web.Services.ApiClients;

public class GamificacaoApiClient : IGamificacaoApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GamificacaoApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        // httpClient.BaseAddress is set in Program.cs
    }

    private Task AddAuthTokenAsync()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        return Task.CompletedTask;
    }

    public async Task<StreakUsuarioViewModel?> GetStreakAsync(CancellationToken cancellationToken = default)
    {
        await AddAuthTokenAsync();
        try
        {
            var response = await _httpClient.GetAsync("/api/Gamificacao/streak", cancellationToken);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<StreakUsuarioViewModel>(cancellationToken: cancellationToken);
        }
        catch { return null; }
    }

    public async Task<DesafioDiarioViewModel?> GetDesafioDiarioAsync(CancellationToken cancellationToken = default)
    {
        await AddAuthTokenAsync();
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

    public async Task<IEnumerable<ConquistaViewModel>> GetConquistasAsync(CancellationToken cancellationToken = default)
    {
        await AddAuthTokenAsync();
        try
        {
            var response = await _httpClient.GetAsync("/api/Gamificacao/conquistas", cancellationToken);
            if (!response.IsSuccessStatusCode) return Enumerable.Empty<ConquistaViewModel>();
            return await response.Content.ReadFromJsonAsync<IEnumerable<ConquistaViewModel>>(cancellationToken: cancellationToken) ?? Enumerable.Empty<ConquistaViewModel>();
        }
        catch { return Enumerable.Empty<ConquistaViewModel>(); }
    }
}
