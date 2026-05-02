using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Materias;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class MateriaService : IMateriaService
{
    private readonly IMateriaRepository _materiaRepository;
    private readonly IAssuntoRepository _assuntoRepository;
    private readonly IQuestaoRepository _questaoRepository;
    private readonly ITentativaQuestaoRepository _tentativaQuestaoRepository;
    private readonly IVideoAulaRepository _videoAulaRepository;
    private readonly ILivroRepository _livroRepository;

    public MateriaService(
        IMateriaRepository materiaRepository,
        IAssuntoRepository assuntoRepository,
        IQuestaoRepository questaoRepository,
        ITentativaQuestaoRepository tentativaQuestaoRepository,
        IVideoAulaRepository videoAulaRepository,
        ILivroRepository livroRepository)
    {
        _materiaRepository = materiaRepository;
        _assuntoRepository = assuntoRepository;
        _questaoRepository = questaoRepository;
        _tentativaQuestaoRepository = tentativaQuestaoRepository;
        _videoAulaRepository = videoAulaRepository;
        _livroRepository = livroRepository;
    }


    public async Task<Result<IReadOnlyList<MateriaDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var materias = await _materiaRepository.GetAllAsync(cancellationToken);

        var dtos = materias.Select(m => new MateriaDto(
            m.Id, m.Nome, m.Descricao, m.Assuntos.Count)).ToList();

        return Result<IReadOnlyList<MateriaDto>>.Ok(dtos);
    }

    public async Task<Result<MateriaComAssuntosDto>> GetByIdComAssuntosAsync(Guid id, CancellationToken cancellationToken)
    {
        var materia = await _materiaRepository.GetByIdComAssuntosAsync(id, cancellationToken);

        if (materia is null)
            return Result<MateriaComAssuntosDto>.Fail("Matéria não encontrada.");

        var assuntoDtos = materia.Assuntos
            .Select(a => new AssuntoDto(a.Id, a.Nome, a.Descricao, a.MateriaId))
            .ToList();

        var dto = new MateriaComAssuntosDto(materia.Id, materia.Nome, materia.Descricao, assuntoDtos);

        return Result<MateriaComAssuntosDto>.Ok(dto);
    }

    public async Task<Result<MateriaDto>> CriarAsync(CriarMateriaRequest request, CancellationToken cancellationToken)
    {
        var materia = new Materia(request.Nome, request.Descricao);
        await _materiaRepository.AddAsync(materia, cancellationToken);

        return Result<MateriaDto>.Ok(new MateriaDto(materia.Id, materia.Nome, materia.Descricao, 0));
    }

    public async Task<Result> AtualizarAsync(Guid id, AtualizarMateriaRequest request, CancellationToken cancellationToken)
    {
        var materia = await _materiaRepository.GetByIdAsync(id, cancellationToken);

        if (materia is null)
            return Result.Fail("Matéria não encontrada.");

        await _materiaRepository.UpdateAsync(materia, cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var materia = await _materiaRepository.GetByIdAsync(id, cancellationToken);

        if (materia is null)
            return Result.Fail("Matéria não encontrada.");

        var livrosVinculados = await _livroRepository.GetPagedAsync(1, 1, materiaId: id, cancellationToken: cancellationToken);
        if (livrosVinculados.TotalCount > 0)
            return Result.Fail("Não é possível excluir esta matéria pois existem Livros e Provas vinculados a ela. Exclua ou mova os livros primeiro.");

        // 1. Buscar todos os assuntos da matéria
        var assuntos = await _assuntoRepository.GetByMateriaIdAsync(id, cancellationToken);
        
        foreach (var assunto in assuntos)
        {
            // 2. Buscar IDs de todas as questões do assunto
            var questaoIds = await _questaoRepository.GetIdsByAssuntoAsync(assunto.Id, cancellationToken);
            
            if (questaoIds.Any())
            {
                // 3. Limpar tentativas de alunos para cada questão
                foreach (var qId in questaoIds)
                {
                    await _tentativaQuestaoRepository.DeleteByQuestaoIdAsync(qId, cancellationToken);
                }

                // 4. Limpar vínculos em simulados
                await _questaoRepository.DeleteSimuladosQuestoesByQuestaoIdsAsync(questaoIds, cancellationToken);

                // 5. Excluir questões (bulk)
                await _questaoRepository.DeleteByAssuntoIdAsync(assunto.Id, cancellationToken);
            }

            // 6. Limpar mídias do assunto
            await _videoAulaRepository.DeleteByAssuntoIdAsync(assunto.Id, cancellationToken);

            // 7. Excluir o assunto
            await _assuntoRepository.DeleteAsync(assunto, cancellationToken);
        }

        // 8. Por fim, excluir a matéria
        await _materiaRepository.DeleteAsync(materia, cancellationToken);

        return Result.Ok();
    }
}
