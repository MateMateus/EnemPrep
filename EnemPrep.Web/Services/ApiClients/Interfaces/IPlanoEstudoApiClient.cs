using EnemPrep.Web.Models;
using EnemPrep.Web.Models.PlanosEstudo;

namespace EnemPrep.Web.Services.ApiClients.Interfaces;

public interface IPlanoEstudoApiClient
{
    Task<IReadOnlyList<PlanoEstudoViewModel>> ObterPorUsuarioAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<PlanoEstudoViewModel?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CriarAsync(Guid usuarioId, CriarPlanoEstudoViewModel request, CancellationToken cancellationToken = default);
    Task<bool> AtualizarStatusItemAsync(Guid itemId, EnemPrep.Web.Enums.StatusPlanoItem novoStatus, CancellationToken cancellationToken = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken cancellationToken = default);
}
