using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Gamificacao;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class GamificacaoService : IGamificacaoService
{
    private readonly IStreakUsuarioRepository _streakRepository;
    private readonly IDesafioDiarioRepository _desafioRepository;
    private readonly IConquistaRepository _conquistaRepository;
    private readonly IUsuarioConquistaRepository _usuarioConquistaRepository;
    private readonly ITentativaQuestaoRepository _tentativaRepository;

    public GamificacaoService(
        IStreakUsuarioRepository streakRepository,
        IDesafioDiarioRepository desafioRepository,
        IConquistaRepository conquistaRepository,
        IUsuarioConquistaRepository usuarioConquistaRepository,
        ITentativaQuestaoRepository tentativaRepository)
    {
        _streakRepository = streakRepository;
        _desafioRepository = desafioRepository;
        _conquistaRepository = conquistaRepository;
        _usuarioConquistaRepository = usuarioConquistaRepository;
        _tentativaRepository = tentativaRepository;
    }

    public async Task<Result<StreakUsuarioDto>> GetStreakAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var streak = await _streakRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);
        
        if (streak is null)
            return Result<StreakUsuarioDto>.Ok(new StreakUsuarioDto(usuarioId, 0, 0, DateTime.UtcNow));

        return Result<StreakUsuarioDto>.Ok(new StreakUsuarioDto(
            streak.UsuarioId,
            streak.DiasConsecutivos,
            streak.MaiorStreak,
            streak.UltimaAtividade));
    }

    public async Task<Result<DesafioDiarioDto?>> GetDesafioDiarioAsync(Guid usuarioId, DateTime data, CancellationToken cancellationToken = default)
    {
        var desafio = await _desafioRepository.GetDesafioDoDiaAsync(data, cancellationToken);
        if (desafio is null)
            return Result<DesafioDiarioDto?>.Ok(null);

        // Verifica se o usuário já fez e acertou a questão do desafio no dia correspondente
        var tentativasDoDia = await _tentativaRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);
        
        // Verifica se houve alguma tentativa certa nesta questão na data do desafio.
        bool concluido = tentativasDoDia.Any(t => 
            t.QuestaoId == desafio.QuestaoId && 
            t.Acertou && 
            t.DataCriacao.Date == data.Date);

        return Result<DesafioDiarioDto?>.Ok(new DesafioDiarioDto(
            desafio.Id,
            desafio.Titulo,
            desafio.DataDesafio,
            desafio.QuestaoId,
            desafio.XPRecompensa,
            concluido));
    }

    public async Task<Result<IEnumerable<ConquistaDto>>> GetConquistasDoUsuarioAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var todasConquistas = await _conquistaRepository.GetAllAsync(cancellationToken);
        var conquistasDoUsuario = await _usuarioConquistaRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);

        var usuarioConquistaDic = conquistasDoUsuario.ToDictionary(uc => uc.ConquistaId);

        var dtos = todasConquistas.Select(c =>
        {
            usuarioConquistaDic.TryGetValue(c.Id, out var uc);
            return new ConquistaDto(
                c.Id,
                c.Titulo,
                c.Descricao,
                c.Icone,
                c.PontosZ,
                uc != null,
                uc?.DataObtencao
            );
        }).ToList();

        return Result<IEnumerable<ConquistaDto>>.Ok(dtos);
    }

    public async Task RegistrarAtividadeDiariaAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var streak = await _streakRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);

        if (streak is null)
        {
            streak = new StreakUsuario(usuarioId);
            streak.RegistrarAtividade();
            await _streakRepository.AddAsync(streak, cancellationToken);
        }
        else
        {
            streak.RegistrarAtividade();
            await _streakRepository.UpdateAsync(streak, cancellationToken);
        }
    }

    public async Task VerificarEAtualizarDesafioDiarioAsync(Guid usuarioId, Guid questaoRespondidaId, bool acertou, CancellationToken cancellationToken = default)
    {
        if (!acertou) return;

        var desafioDeHoje = await _desafioRepository.GetDesafioDoDiaAsync(DateTime.UtcNow, cancellationToken);
        
        if (desafioDeHoje is not null && desafioDeHoje.QuestaoId == questaoRespondidaId)
        {
            // O usuário acertou o desafio do dia.
            // Para não quebrar o DRY nem inventar domínio de "Wallet/XP" sem necessidade (fora de escopo),
            // a atribuição de Conquista vinculada a isso pode ocorrer aqui.
            // Exemplo: Conquista de "Primeiro Desafio Diário", que deve carregar ID real se fosse semeado.
            // Pelo escopo minimalista, daremos por concluído via GetDesafioDiarioAsync conferindo a tentativa.
        }
    }
}
