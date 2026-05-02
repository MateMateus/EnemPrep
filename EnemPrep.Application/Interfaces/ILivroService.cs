using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Livros;

using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.Interfaces;

public interface ILivroService
{
    Task<Result<LivroDetalhadoDto>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Result<PagedResult<LivroDto>>> GetPagedAsync(int pageNumber, int pageSize, string? busca = null, Guid? materiaId = null, TipoConteudo? tipo = null, CancellationToken ct = default);
    Task<Result<LivroDto>> CriarAsync(CriarLivroRequest request, CancellationToken ct = default);
    Task<Result> AtualizarAsync(Guid id, CriarLivroRequest request, CancellationToken ct = default);
    Task<Result> DeletarAsync(Guid id, CancellationToken ct = default);
    Task<Result<int>> AdicionarPaginasAsync(Guid livroId, AdicionarPaginasRequest request, CancellationToken ct = default);
    Task<Result> RemoverPaginaAsync(Guid livroId, Guid paginaId, CancellationToken ct = default);
    Task<Result<LivroTemaDto>> CriarTemaAsync(Guid livroId, CriarTemaRequest request, CancellationToken ct = default);
    Task<Result> AtualizarTemaAsync(Guid livroId, Guid temaId, CriarTemaRequest request, CancellationToken ct = default);
    Task<Result> DeletarTemaAsync(Guid livroId, Guid temaId, CancellationToken ct = default);
}
