using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.VideoAulas;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class VideoAulaService : IVideoAulaService
{
    private readonly IVideoAulaRepository _videoAulaRepository;

    public VideoAulaService(IVideoAulaRepository videoAulaRepository)
    {
        _videoAulaRepository = videoAulaRepository;
    }

    public async Task<Result<IReadOnlyList<VideoAulaDto>>> GetByAssuntoIdAsync(Guid assuntoId, CancellationToken cancellationToken)
    {
        var videoAulas = await _videoAulaRepository.GetByAssuntoIdAsync(assuntoId, cancellationToken);

        var dtos = videoAulas
            .Select(v => new VideoAulaDto(v.Id, v.Titulo, v.UrlVideo, v.DuracaoSegundos, v.AssuntoId, v.Assunto?.Nome ?? string.Empty))
            .ToList();

        return Result<IReadOnlyList<VideoAulaDto>>.Ok(dtos);
    }

    public async Task<Result<VideoAulaDto>> CriarAsync(CriarVideoAulaRequest request, CancellationToken cancellationToken)
    {
        var videoAula = new VideoAula(request.Titulo, request.UrlVideo, request.DuracaoSegundos, request.AssuntoId);
        await _videoAulaRepository.AddAsync(videoAula, cancellationToken);

        return Result<VideoAulaDto>.Ok(
            new VideoAulaDto(videoAula.Id, videoAula.Titulo, videoAula.UrlVideo, videoAula.DuracaoSegundos, videoAula.AssuntoId, string.Empty));
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var videoAula = await _videoAulaRepository.GetByIdAsync(id, cancellationToken);

        if (videoAula is null)
            return Result.Fail("Videoaula não encontrada.");

        await _videoAulaRepository.DeleteAsync(videoAula, cancellationToken);

        return Result.Ok();
    }
}
