using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Questoes;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class QuestaoService : IQuestaoService
{
    private readonly IQuestaoRepository _questaoRepository;
    private readonly ITentativaQuestaoRepository _tentativaRepository;
    private readonly IGamificacaoService _gamificacaoService;

    public QuestaoService(
        IQuestaoRepository questaoRepository, 
        ITentativaQuestaoRepository tentativaRepository,
        IGamificacaoService gamificacaoService)
    {
        _questaoRepository = questaoRepository;
        _tentativaRepository = tentativaRepository;
        _gamificacaoService = gamificacaoService;
    }

    public async Task<Result<QuestaoDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var questao = await _questaoRepository.GetByIdComAlternativasAsync(id, cancellationToken);

        if (questao is null)
            return Result<QuestaoDto>.Fail("Questão não encontrada.");

        return Result<QuestaoDto>.Ok(MapToDto(questao));
    }

    public async Task<Result<PagedResult<QuestaoDto>>> GetPagedByAssuntoAsync(Guid assuntoId, PagedRequest request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _questaoRepository.GetPagedByAssuntoAsync(
            assuntoId, request.PageNumber, request.PageSize, cancellationToken);

        var dtos = items.Select(MapToDto).ToList();

        var pagedResult = new PagedResult<QuestaoDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        return Result<PagedResult<QuestaoDto>>.Ok(pagedResult);
    }

    public async Task<Result<QuestaoDto>> CriarAsync(CriarQuestaoRequest request, CancellationToken cancellationToken)
    {
        var questao = new Questao(request.Enunciado, request.Dificuldade, request.AssuntoId, request.Explicacao);

        foreach (var alt in request.Alternativas)
        {
            var alternativa = new Alternativa(alt.Texto, alt.Correta, questao.Id);
            questao.AdicionarAlternativa(alternativa);
        }

        await _questaoRepository.AddAsync(questao, cancellationToken);

        return Result<QuestaoDto>.Ok(MapToDto(questao));
    }

    public async Task<Result<ResultadoQuestaoDto>> ResponderAsync(Guid usuarioId, ResponderQuestaoRequest request, CancellationToken cancellationToken)
    {
        var questao = await _questaoRepository.GetByIdComAlternativasAsync(request.QuestaoId, cancellationToken);

        if (questao is null)
            return Result<ResultadoQuestaoDto>.Fail("Questão não encontrada.");

        var alternativaCorreta = questao.Alternativas.FirstOrDefault(a => a.Correta);
        if (alternativaCorreta is null)
            return Result<ResultadoQuestaoDto>.Fail("Questão sem alternativa correta configurada.");

        var acertou = request.AlternativaSelecionadaId == alternativaCorreta.Id;

        var tentativa = new TentativaQuestao(
            usuarioId,
            request.QuestaoId,
            request.AlternativaSelecionadaId,
            acertou,
            request.TempoGastoSegundos);

        await _tentativaRepository.AddAsync(tentativa, cancellationToken);

        // Dispara gatilhos da gamificação baseados na resolução da questão
        await _gamificacaoService.RegistrarAtividadeDiariaAsync(usuarioId, cancellationToken);
        await _gamificacaoService.VerificarEAtualizarDesafioDiarioAsync(usuarioId, request.QuestaoId, acertou, cancellationToken);

        return Result<ResultadoQuestaoDto>.Ok(
            new ResultadoQuestaoDto(acertou, alternativaCorreta.Id, questao.Explicacao));
    }

    public async Task<Result<IEnumerable<TentativaQuestaoDto>>> GetHistoricoAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var tentativas = await _tentativaRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);
        
        var dtos = tentativas.Select(t => new TentativaQuestaoDto(
            t.Id,
            t.QuestaoId,
            t.Questao?.Enunciado ?? "Questão indisponível",
            t.Acertou,
            t.TempoGastoSegundos,
            t.DataCriacao
        )).OrderByDescending(t => t.DataTentativa).ToList();

        return Result<IEnumerable<TentativaQuestaoDto>>.Ok(dtos);
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var questao = await _questaoRepository.GetByIdComAlternativasAsync(id, cancellationToken);

        if (questao is null)
            return Result.Fail("Questão não encontrada.");

        await _questaoRepository.DeleteAsync(questao, cancellationToken);

        return Result.Ok();
    }

    private static QuestaoDto MapToDto(Questao q)
    {
        var alternativas = q.Alternativas
            .Select(a => new AlternativaDto(a.Id, a.Texto, a.Correta))
            .ToList();

        return new QuestaoDto(
            q.Id,
            q.Enunciado,
            q.Dificuldade,
            q.AssuntoId,
            q.Assunto?.Nome ?? string.Empty,
            q.Explicacao,
            alternativas);
    }
}
