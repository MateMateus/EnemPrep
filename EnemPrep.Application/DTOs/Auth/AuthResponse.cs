namespace EnemPrep.Application.DTOs.Auth;

public record AuthResponse(
    Guid UsuarioId,
    string Nome,
    string Email,
    string TipoPerfil,
    string Token
);
