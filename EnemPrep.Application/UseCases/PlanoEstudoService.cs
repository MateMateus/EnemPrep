using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.PlanoEstudo;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class PlanoEstudoService : IPlanoEstudoService
{
    private readonly IPlanoEstudoRepository _planoRepository;

    public PlanoEstudoService(IPlanoEstudoRepository planoRepository)
    {
        _planoRepository = planoRepository;
    }

    public async Task<Result<IReadOnlyList<PlanoEstudoDto>>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken)
    {
        var planos = await _planoRepository.GetByUsuarioIdAsync(usuarioId, cancellationToken);

        var dtos = planos.Select(MapToDto).ToList();

        return Result<IReadOnlyList<PlanoEstudoDto>>.Ok(dtos);
    }

    public async Task<Result<PlanoEstudoDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var plano = await _planoRepository.GetByIdComItensAsync(id, cancellationToken);

        if (plano is null)
            return Result<PlanoEstudoDto>.Fail("Plano de estudo não encontrado.");

        return Result<PlanoEstudoDto>.Ok(MapToDto(plano));
    }

    public async Task<Result<PlanoEstudoDto>> CriarAsync(Guid usuarioId, CriarPlanoEstudoRequest request, CancellationToken cancellationToken)
    {
        var plano = new PlanoEstudo(request.Titulo, request.DataInicio, request.DataFim, usuarioId);

        foreach (var itemReq in request.Itens)
        {
            plano.AdicionarItem(itemReq.AssuntoId, itemReq.DataPrevista);
        }

        await _planoRepository.AddAsync(plano, cancellationToken);

        return Result<PlanoEstudoDto>.Ok(MapToDto(plano));
    }

    public async Task<Result> AtualizarStatusItemAsync(Guid planoEstudoItemId, AtualizarStatusPlanoItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _planoRepository.GetItemByIdAsync(planoEstudoItemId, cancellationToken);
        
        if (item is null)
            return Result.Fail("Item do plano de estudo não encontrado.");

        item.AtualizarStatus(request.Status);
        
        await _planoRepository.UpdateItemAsync(item, cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken cancellationToken)
    {
        var plano = await _planoRepository.GetByIdComItensAsync(id, cancellationToken);

        if (plano is null)
            return Result.Fail("Plano de estudo não encontrado.");

        await _planoRepository.DeleteAsync(plano, cancellationToken);

        return Result.Ok();
    }

    private static PlanoEstudoDto MapToDto(PlanoEstudo plano)
    {
        var itens = plano.Itens.Select(i => new PlanoEstudoItemDto(
            i.Id,
            i.AssuntoId,
            i.Assunto?.Nome ?? string.Empty,
            i.DataPrevista,
            i.Status)).ToList();

        return new PlanoEstudoDto(plano.Id, plano.Titulo, plano.DataInicio, plano.DataFim, itens);
    }
}
