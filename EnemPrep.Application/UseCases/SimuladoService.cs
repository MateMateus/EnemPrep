using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Simulados;
using EnemPrep.Application.DTOs.Questoes;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class SimuladoService : ISimuladoService
{
    private readonly ISimuladoRepository _simuladoRepository;
    private readonly ITentativaSimuladoRepository _tentativaRepository;
    private readonly IQuestaoRepository _questaoRepository;

    public SimuladoService(
        ISimuladoRepository simuladoRepository,
        ITentativaSimuladoRepository tentativaRepository,
        IQuestaoRepository questaoRepository)
    {
        _simuladoRepository = simuladoRepository;
        _tentativaRepository = tentativaRepository;
        _questaoRepository = questaoRepository;
    }

    public async Task<Result<IEnumerable<SimuladoResumoDto>>> GetSimuladosDisponiveisAsync(CancellationToken cancellationToken = default)
    {
        var simulados = await _simuladoRepository.GetAllAsync(cancellationToken);

        var dtos = simulados.Select(s => new SimuladoResumoDto
        {
            Id = s.Id,
            Titulo = s.Titulo,
            AnoReferencia = s.AnoReferencia,
            DuracaoMaxima = s.DuracaoMaxima,
            QuantidadeQuestoes = s.Questoes?.Count ?? 0
        });

        return Result<IEnumerable<SimuladoResumoDto>>.Ok(dtos.ToList());
    }

    public async Task<Result<SimuladoDetalheDto>> GetSimuladoByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var simulado = await _simuladoRepository.GetByIdWithQuestoesAsync(id, cancellationToken);

        if (simulado == null)
            return Result<SimuladoDetalheDto>.Fail("Simulado não encontrado.");

        var dto = new SimuladoDetalheDto
        {
            Id = simulado.Id,
            Titulo = simulado.Titulo,
            AnoReferencia = simulado.AnoReferencia,
            DuracaoMaxima = simulado.DuracaoMaxima,
            Questoes = simulado.Questoes.Select(sq => new QuestaoDto(
                sq.Questao.Id,
                sq.Questao.Enunciado,
                sq.Questao.Dificuldade,
                sq.Questao.AssuntoId,
                sq.Questao.Assunto?.Nome ?? string.Empty,
                sq.Questao.Explicacao,
                sq.Questao.Alternativas.Select(a => new AlternativaDto(a.Id, a.Texto, a.Correta)).ToList()
            )).ToList()
        };

        return Result<SimuladoDetalheDto>.Ok(dto);
    }

    public async Task<Result<TentativaSimuladoResultDto>> IniciarSimuladoAsync(Guid usuarioId, IniciarSimuladoDto request, CancellationToken cancellationToken = default)
    {
        var simulado = await _simuladoRepository.GetByIdAsync(request.SimuladoId, cancellationToken);
        if (simulado == null)
            return Result<TentativaSimuladoResultDto>.Fail("Simulado não encontrado.");

        var tentativa = new TentativaSimulado(usuarioId, request.SimuladoId);

        await _tentativaRepository.AddAsync(tentativa, cancellationToken);
        await _tentativaRepository.SaveChangesAsync(cancellationToken);

        return Result<TentativaSimuladoResultDto>.Ok(new TentativaSimuladoResultDto
        {
            Id = tentativa.Id,
            SimuladoId = tentativa.SimuladoId,
            DataInicio = tentativa.DataInicio
        });
    }

    public async Task<Result<TentativaSimuladoResultDto>> SubmeterSimuladoAsync(Guid usuarioId, Guid tentativaId, SubmeterSimuladoDto request, CancellationToken cancellationToken = default)
    {
        var tentativa = await _tentativaRepository.GetByIdWithRespostasAsync(tentativaId, cancellationToken);

        if (tentativa == null)
            return Result<TentativaSimuladoResultDto>.Fail("Tentativa não encontrada.");

        if (tentativa.UsuarioId != usuarioId)
            return Result<TentativaSimuladoResultDto>.Fail("Acesso negado à tentativa de outro usuário.");

        if (tentativa.DataFim.HasValue)
            return Result<TentativaSimuladoResultDto>.Fail("Este simulado já foi finalizado.");

        var simulado = await _simuladoRepository.GetByIdWithQuestoesAsync(tentativa.SimuladoId, cancellationToken);
        if (simulado == null)
            return Result<TentativaSimuladoResultDto>.Fail("Simulado base não encontrado.");

        var respostasSalvas = new List<RespostaSimulado>();

        foreach (var dtoResp in request.Respostas)
        {
            var sq = simulado.Questoes.FirstOrDefault(q => q.QuestaoId == dtoResp.QuestaoId);
            if (sq == null) continue; // Questão não pertence ao simulado

            // Verificar se está correta
            bool correta = false;

            if (dtoResp.AlternativaId.HasValue)
            {
                var alternativaCorreta = sq.Questao.Alternativas.FirstOrDefault(a => a.Correta);
                if (alternativaCorreta != null && alternativaCorreta.Id == dtoResp.AlternativaId.Value)
                {
                    correta = true;
                }
            }

            respostasSalvas.Add(new RespostaSimulado(tentativa.Id, dtoResp.QuestaoId, dtoResp.AlternativaId, correta));
        }

        tentativa.Finalizar(respostasSalvas);
        _tentativaRepository.Update(tentativa);
        await _tentativaRepository.SaveChangesAsync(cancellationToken);

        return Result<TentativaSimuladoResultDto>.Ok(new TentativaSimuladoResultDto
        {
            Id = tentativa.Id,
            SimuladoId = tentativa.SimuladoId,
            DataInicio = tentativa.DataInicio,
            DataFim = tentativa.DataFim,
            NotaTotalBruta = tentativa.NotaTotalBruta
        });
    }

    public async Task<Result<IEnumerable<TentativaSimuladoResultDto>>> GetHistoricoTentativasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var tentativas = await _tentativaRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);

        var dtos = tentativas.Select(t => new TentativaSimuladoResultDto
        {
            Id = t.Id,
            SimuladoId = t.SimuladoId,
            DataInicio = t.DataInicio,
            DataFim = t.DataFim,
            NotaTotalBruta = t.NotaTotalBruta
        });

        return Result<IEnumerable<TentativaSimuladoResultDto>>.Ok(dtos.ToList());
    }
}
