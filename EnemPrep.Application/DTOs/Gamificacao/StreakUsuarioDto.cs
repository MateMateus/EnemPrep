namespace EnemPrep.Application.DTOs.Gamificacao;

public record StreakUsuarioDto(
    Guid UsuarioId,
    int DiasConsecutivos,
    int MaiorStreak,
    DateTime UltimaAtividade);
