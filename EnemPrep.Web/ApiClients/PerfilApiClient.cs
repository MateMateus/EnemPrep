using System.Net.Http.Json;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IPerfilApiClient
{
    Task<PerfilViewModel?> GetPerfilAsync(Guid usuarioId, CancellationToken ct = default);
    Task<bool> AtualizarNomeAsync(Guid usuarioId, string novoNome, CancellationToken ct = default);
    Task<bool> AtualizarEmailAsync(Guid usuarioId, string novoEmail, CancellationToken ct = default);
    Task<(bool Success, string? ErrorMessage)> AtualizarSenhaAsync(Guid usuarioId, string senhaAtual, string novaSenha, CancellationToken ct = default);
}

public class PerfilApiClient(HttpClient http) : IPerfilApiClient
{
    public async Task<PerfilViewModel?> GetPerfilAsync(Guid usuarioId, CancellationToken ct = default)
    {
        var response = await http.GetFromJsonAsync<ApiResponse<PerfilViewModel>>($"api/perfil/{usuarioId}", ct);
        return response?.Data;
    }

    public async Task<bool> AtualizarNomeAsync(Guid usuarioId, string novoNome, CancellationToken ct = default)
    {
        var httpResponse = await http.PutAsJsonAsync($"api/perfil/{usuarioId}/nome", new { NovoNome = novoNome }, ct);
        return httpResponse.IsSuccessStatusCode;
    }

    public async Task<bool> AtualizarEmailAsync(Guid usuarioId, string novoEmail, CancellationToken ct = default)
    {
        var httpResponse = await http.PutAsJsonAsync($"api/perfil/{usuarioId}/email", new { NovoEmail = novoEmail }, ct);
        return httpResponse.IsSuccessStatusCode;
    }

    public async Task<(bool Success, string? ErrorMessage)> AtualizarSenhaAsync(Guid usuarioId, string senhaAtual, string novaSenha, CancellationToken ct = default)
    {
        var httpResponse = await http.PutAsJsonAsync($"api/perfil/{usuarioId}/senha", new { SenhaAtual = senhaAtual, NovaSenha = novaSenha }, ct);

        if (httpResponse.IsSuccessStatusCode)
            return (true, null);

        try
        {
            var errorBody = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<object>>(cancellationToken: ct);
            return (false, errorBody?.ErrorMessage ?? "Erro ao alterar senha.");
        }
        catch
        {
            return (false, "Erro ao alterar senha.");
        }
    }
}
