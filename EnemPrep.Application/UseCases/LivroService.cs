using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Livros;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;

using EnemPrep.Domain.Enums;

namespace EnemPrep.Application.UseCases;

public class LivroService : ILivroService
{
    private readonly ILivroRepository _repo;
    private readonly IQuestaoRepository _questaoRepo;

    public LivroService(ILivroRepository repo, IQuestaoRepository questaoRepo)
    {
        _repo = repo;
        _questaoRepo = questaoRepo;
    }

    public async Task<Result<LivroDetalhadoDto>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdCompletoAsync(id, ct);
        if (livro is null)
            return Result<LivroDetalhadoDto>.Fail("Livro não encontrado.");

        return Result<LivroDetalhadoDto>.Ok(MapDetalhado(livro));
    }

    public async Task<Result<PagedResult<LivroDto>>> GetPagedAsync(int pageNumber, int pageSize, string? busca = null, Guid? materiaId = null, TipoConteudo? tipo = null, CancellationToken ct = default)
    {
        var (items, total) = await _repo.GetPagedAsync(pageNumber, pageSize, busca, materiaId, tipo, ct);
        var dtos = items.Select(MapSimples).ToList();
        return Result<PagedResult<LivroDto>>.Ok(new PagedResult<LivroDto>(dtos, total, pageNumber, pageSize));
    }

    public async Task<Result<LivroDto>> CriarAsync(CriarLivroRequest request, CancellationToken ct = default)
    {
        var livro = new Livro(request.Titulo, request.Descricao, request.UrlCapa, request.MateriaId, request.TipoConteudo);
        await _repo.AddAsync(livro, ct);
        return Result<LivroDto>.Ok(MapSimples(livro));
    }

    public async Task<Result> AtualizarAsync(Guid id, CriarLivroRequest request, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdAsync(id, ct);
        if (livro is null) return Result.Fail("Livro não encontrado.");

        livro.AtualizarDetalhes(request.Titulo, request.Descricao, request.UrlCapa, request.MateriaId, request.TipoConteudo);
        await _repo.UpdateAsync(livro, ct);
        return Result.Ok();
    }

    public async Task<Result> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdAsync(id, ct);
        if (livro is null) return Result.Fail("Livro não encontrado.");

        // 1. Desvincular todas as questões que apontam para este livro ou seus temas
        await _questaoRepo.DesvincularLivroETemasAsync(id, ct);

        // 2. Remover todas as páginas (bulk)
        await _repo.DeletePaginasByLivroIdAsync(id, ct);

        // 3. Remover todos os temas (bulk)
        await _repo.DeleteTemasByLivroIdAsync(id, ct);

        // 4. Por fim, excluir o livro
        await _repo.DeleteAsync(livro, ct);
        
        return Result.Ok();
    }

    public async Task<Result<int>> AdicionarPaginasAsync(Guid livroId, AdicionarPaginasRequest request, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdCompletoAsync(livroId, ct);
        if (livro is null) return Result<int>.Fail("Livro não encontrado.");

        int proximoNumero = livro.Paginas.Any() ? livro.Paginas.Max(p => p.NumeroProprio) + 1 : 1;

        var novasPaginas = request.UrlsImagens
            .Select(url => new LivroPagina(livroId, proximoNumero++, url))
            .ToList();

        await _repo.AddPaginasAsync(novasPaginas, ct);
        return Result<int>.Ok(novasPaginas.Count);
    }

    public async Task<Result> RemoverPaginaAsync(Guid livroId, Guid paginaId, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdCompletoAsync(livroId, ct);
        if (livro is null) return Result.Fail("Livro não encontrado.");

        var pagina = livro.Paginas.FirstOrDefault(p => p.Id == paginaId);
        if (pagina is null) return Result.Fail("Página não encontrada.");

        await _repo.RemoverPaginaAsync(pagina, ct);
        return Result.Ok();
    }

    public async Task<Result<LivroTemaDto>> CriarTemaAsync(Guid livroId, CriarTemaRequest request, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdAsync(livroId, ct);
        if (livro is null) return Result<LivroTemaDto>.Fail("Livro não encontrado.");

        var tema = new LivroTema(livroId, request.Nome, request.PaginaInicial, request.PaginaFinal);
        await _repo.AddTemaAsync(tema, ct);
        return Result<LivroTemaDto>.Ok(new LivroTemaDto(tema.Id, tema.Nome, tema.PaginaInicial, tema.PaginaFinal));
    }

    public async Task<Result> AtualizarTemaAsync(Guid livroId, Guid temaId, CriarTemaRequest request, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdCompletoAsync(livroId, ct);
        if (livro is null) return Result.Fail("Livro não encontrado.");

        var tema = livro.Temas.FirstOrDefault(t => t.Id == temaId);
        if (tema is null) return Result.Fail("Tema não encontrado.");

        tema.Atualizar(request.Nome, request.PaginaInicial, request.PaginaFinal);
        await _repo.UpdateTemaAsync(tema, ct);
        return Result.Ok();
    }

    public async Task<Result> DeletarTemaAsync(Guid livroId, Guid temaId, CancellationToken ct = default)
    {
        var livro = await _repo.GetByIdCompletoAsync(livroId, ct);
        if (livro is null) return Result.Fail("Livro não encontrado.");

        var tema = livro.Temas.FirstOrDefault(t => t.Id == temaId);
        if (tema is null) return Result.Fail("Tema não encontrado.");

        await _repo.RemoverTemaAsync(tema, ct);
        return Result.Ok();
    }

    private static LivroDto MapSimples(Livro l) => new(
        l.Id, l.Titulo, l.Descricao, l.UrlCapa,
        l.Paginas?.Count ?? 0,
        l.Temas?.Count ?? 0,
        l.DataCriacao,
        l.MateriaId,
        l.Materia?.Nome ?? string.Empty,
        l.TipoConteudo);

    private static LivroDetalhadoDto MapDetalhado(Livro l) => new(
        l.Id, l.Titulo, l.Descricao, l.UrlCapa,
        l.Paginas.Count,
        l.DataCriacao,
        l.Paginas.Select(p => new LivroPaginaDto(p.Id, p.NumeroProprio, p.UrlImagem)).ToList(),
        l.Temas.Select(t => new LivroTemaDto(t.Id, t.Nome, t.PaginaInicial, t.PaginaFinal)).ToList(),
        l.MateriaId,
        l.Materia?.Nome ?? string.Empty,
        l.TipoConteudo);
}
