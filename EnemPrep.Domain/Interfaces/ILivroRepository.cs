using EnemPrep.Domain.Entities;

using EnemPrep.Domain.Enums;

namespace EnemPrep.Domain.Interfaces;

public interface ILivroRepository
{
    Task<Livro?> GetByIdCompletoAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Livro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<Livro> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? busca = null, Guid? materiaId = null, TipoConteudo? tipo = null, CancellationToken cancellationToken = default);
    Task AddAsync(Livro livro, CancellationToken cancellationToken = default);
    Task UpdateAsync(Livro livro, CancellationToken cancellationToken = default);
    Task DeleteAsync(Livro livro, CancellationToken cancellationToken = default);
    Task AddPaginasAsync(IEnumerable<LivroPagina> paginas, CancellationToken cancellationToken = default);
    Task RemoverPaginaAsync(LivroPagina pagina, CancellationToken cancellationToken = default);
    Task DeletePaginasByLivroIdAsync(Guid livroId, CancellationToken cancellationToken = default);
    Task DeleteTemasByLivroIdAsync(Guid livroId, CancellationToken cancellationToken = default);
    Task AddTemaAsync(LivroTema tema, CancellationToken cancellationToken = default);
    Task UpdateTemaAsync(LivroTema tema, CancellationToken cancellationToken = default);
    Task RemoverTemaAsync(LivroTema tema, CancellationToken cancellationToken = default);
}
