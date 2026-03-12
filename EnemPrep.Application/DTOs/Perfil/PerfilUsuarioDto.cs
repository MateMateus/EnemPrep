namespace EnemPrep.Application.DTOs.Perfil;

public record PerfilUsuarioDto(
    Guid Id,
    string Nome,
    string Email,
    string TipoPerfil,
    DateTime DataCriacao,
    int QuestoesRespondidas,
    int QuestoesCorretas,
    int StreakDias
);
