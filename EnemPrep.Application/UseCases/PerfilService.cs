using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Perfil;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class PerfilService : IPerfilService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITentativaQuestaoRepository _tentativaRepository;
    private readonly IStreakUsuarioRepository _streakRepository;

    public PerfilService(
        IUsuarioRepository usuarioRepository,
        ITentativaQuestaoRepository tentativaRepository,
        IStreakUsuarioRepository streakRepository)
    {
        _usuarioRepository = usuarioRepository;
        _tentativaRepository = tentativaRepository;
        _streakRepository = streakRepository;
    }

    public async Task<Result<PerfilUsuarioDto>> GetPerfilAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId, cancellationToken);

        if (usuario is null)
            return Result<PerfilUsuarioDto>.Fail("Usuário não encontrado.");

        // Mesma base de cálculo do DashboardService — sem divergência
        var (totalRespondidas, totalAcertos) = await _tentativaRepository.GetEstatisticasGeraisAsync(usuarioId, cancellationToken);

        var streak = await _streakRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);
        var streakAtual = streak?.DiasConsecutivos ?? 0;

        var dto = new PerfilUsuarioDto(
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.PerfilUsuario?.Tipo.ToString() ?? "Aluno",
            usuario.DataCriacao,
            totalRespondidas,
            totalAcertos,
            streakAtual);

        return Result<PerfilUsuarioDto>.Ok(dto);
    }

    public async Task<Result> AtualizarNomeAsync(Guid usuarioId, string novoNome, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId, cancellationToken);

        if (usuario is null)
            return Result.Fail("Usuário não encontrado.");

        if (string.IsNullOrWhiteSpace(novoNome))
            return Result.Fail("Nome não pode ser vazio.");

        usuario.AtualizarNome(novoNome);
        await _usuarioRepository.UpdateAsync(usuario, cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> AtualizarEmailAsync(Guid usuarioId, string novoEmail, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId, cancellationToken);

        if (usuario is null)
            return Result.Fail("Usuário não encontrado.");

        if (string.IsNullOrWhiteSpace(novoEmail))
            return Result.Fail("Email não pode ser vazio.");

        var emailJaExiste = await _usuarioRepository.ExistsAsync(novoEmail, cancellationToken);
        if (emailJaExiste)
            return Result.Fail("Este email já está em uso por outra conta.");

        usuario.AtualizarEmail(novoEmail);
        await _usuarioRepository.UpdateAsync(usuario, cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> AtualizarSenhaAsync(Guid usuarioId, string senhaAtual, string novaSenha, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(usuarioId, cancellationToken);

        if (usuario is null)
            return Result.Fail("Usuário não encontrado.");

        if (string.IsNullOrWhiteSpace(senhaAtual) || string.IsNullOrWhiteSpace(novaSenha))
            return Result.Fail("Senha atual e nova senha são obrigatórias.");

        if (novaSenha.Length < 6)
            return Result.Fail("A nova senha deve ter pelo menos 6 caracteres.");

        // Validação da senha atual contra o hash armazenado
        if (!BCrypt.Net.BCrypt.Verify(senhaAtual, usuario.SenhaHash))
            return Result.Fail("Senha atual incorreta.");

        // Geração do novo hash — mesma estratégia do AuthService
        var novoHash = BCrypt.Net.BCrypt.HashPassword(novaSenha);
        usuario.AtualizarSenha(novoHash);
        await _usuarioRepository.UpdateAsync(usuario, cancellationToken);

        return Result.Ok();
    }
}
