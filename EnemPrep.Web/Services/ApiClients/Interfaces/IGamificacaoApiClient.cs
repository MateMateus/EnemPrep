using EnemPrep.Web.Models.Gamificacao;

namespace EnemPrep.Web.Services.ApiClients.Interfaces;

public interface IGamificacaoApiClient
{
    Task<DesafioDiarioViewModel?> GetDesafioDiarioAsync(CancellationToken cancellationToken = default);
}
