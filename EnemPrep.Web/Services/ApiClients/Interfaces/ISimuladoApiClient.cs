using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Models.Simulados;

namespace EnemPrep.Web.Services.ApiClients.Interfaces;

public interface ISimuladoApiClient
{
    Task<IEnumerable<SimuladoResumo>> GetSimuladosDisponiveisAsync();
    Task<SimuladoDetalhe?> GetSimuladoByIdAsync(Guid id);
    Task<TentativaSimuladoResult?> IniciarSimuladoAsync(Guid usuarioId, IniciarSimuladoRequest request);
    Task<ApiResponse<TentativaSimuladoResult>?> SubmeterSimuladoAsync(Guid usuarioId, Guid tentativaId, SubmeterSimuladoRequest request);
    Task<IEnumerable<TentativaSimuladoResult>> GetHistoricoTentativasAsync(Guid usuarioId);
}
