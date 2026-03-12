using EnemPrep.Api.Extensions;
using EnemPrep.Application.DTOs.Auth;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);

        if (!result.Success)
            return Unauthorized(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<AuthResponse>.Ok(result.Data!));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(request, cancellationToken);

        if (!result.Success)
            return BadRequest(ApiResponse<object>.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<AuthResponse>.Ok(result.Data!));
    }
}
