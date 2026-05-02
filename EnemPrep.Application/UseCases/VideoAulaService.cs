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

    public async Task<Result<PagedResult<VideoAulaDto>>> GetByAssuntoIdAsync(Guid assuntoId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await _videoAulaRepository.GetPagedByAssuntoIdAsync(assuntoId, pageNumber, pageSize, cancellationToken);

        var dtos = result.Items
            .Select(v => new VideoAulaDto(v.Id, v.Titulo, v.UrlVideo, v.DuracaoSegundos, v.AssuntoId, v.Assunto?.Nome ?? string.Empty))
            .ToList();

        return Result<PagedResult<VideoAulaDto>>.Ok(new PagedResult<VideoAulaDto>(dtos, result.TotalCount, pageNumber, pageSize));
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
