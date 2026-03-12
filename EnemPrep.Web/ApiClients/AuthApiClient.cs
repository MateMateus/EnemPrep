using System.Net.Http.Json;
using EnemPrep.Web.Models.Auth;
using EnemPrep.Web.ApiClients; // Para a classe ApiResponse<T>

namespace EnemPrep.Web.ApiClients;

// Definimos o response da API de Auth (espelhando AuthResponse)
public class AuthResponseData
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TipoPerfil { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

public interface IAuthApiClient
{
    Task<AuthResponseData?> LoginAsync(LoginViewModel vm, CancellationToken ct = default);
    Task<AuthResponseData?> RegisterAsync(RegisterViewModel vm, CancellationToken ct = default);
}

public class AuthApiClient(HttpClient http) : IAuthApiClient
{
    public async Task<AuthResponseData?> LoginAsync(LoginViewModel vm, CancellationToken ct = default)
    {
        var httpResponse = await http.PostAsJsonAsync("api/auth/login", new { vm.Email, vm.Senha }, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;

        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<AuthResponseData>>(ct);
        return response?.Data;
    }

    public async Task<AuthResponseData?> RegisterAsync(RegisterViewModel vm, CancellationToken ct = default)
    {
        var httpResponse = await http.PostAsJsonAsync("api/auth/register", new { vm.Nome, vm.Email, vm.Senha, ConfirmacaoSenha = vm.ConfirmarSenha }, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;

        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<AuthResponseData>>(ct);
        return response?.Data;
    }
}
