using EnemPrep.Web.Models.Gamificacao;

namespace EnemPrep.Web.Services.ApiClients.Interfaces;

public interface IGamificacaoApiClient
{
    Task<StreakUsuarioViewModel?> GetStreakAsync(CancellationToken cancellationToken = default);
    Task<DesafioDiarioViewModel?> GetDesafioDiarioAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ConquistaViewModel>> GetConquistasAsync(CancellationToken cancellationToken = default);
}
