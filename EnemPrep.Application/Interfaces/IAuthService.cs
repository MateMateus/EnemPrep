using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Auth;

namespace EnemPrep.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
}
