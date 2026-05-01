using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using EnemPrep.Domain.Enums;

namespace EnemPrep.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    private readonly EnemPrepDbContext _context;

    public LivroRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<Livro?> GetByIdCompletoAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Livros
            .AsNoTracking()
            .Include(l => l.Paginas.OrderBy(p => p.NumeroProprio))
            .Include(l => l.Temas.OrderBy(t => t.PaginaInicial))
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<Livro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<(IReadOnlyList<Livro> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, string? busca = null, Guid? materiaId = null, TipoConteudo? tipo = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Livros
            .Include(l => l.Materia)
            .Include(l => l.Paginas)
            .Include(l => l.Temas)
            .AsNoTracking();

        if (materiaId.HasValue && materiaId.Value != Guid.Empty)
        {
            query = query.Where(l => l.MateriaId == materiaId.Value);
        }

        if (tipo.HasValue)
        {
            query = query.Where(l => l.TipoConteudo == tipo.Value);
        }

        if (!string.IsNullOrWhiteSpace(busca))
        {
            // O EF.Functions.Like no SQL Server já utiliza o Collation padrão da base (geralmente CI_AI).
            query = query.Where(l => EF.Functions.Like(l.Titulo, $"%{busca}%"));
        }

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(l => l.DataCriacao)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, count);
    }

    public async Task AddAsync(Livro livro, CancellationToken cancellationToken = default)
    {
        await _context.Livros.AddAsync(livro, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Livro livro, CancellationToken cancellationToken = default)
    {
        _context.Livros.Update(livro);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Livro livro, CancellationToken cancellationToken = default)
    {
        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddPaginasAsync(IEnumerable<LivroPagina> paginas, CancellationToken cancellationToken = default)
    {
        await _context.LivrosPaginas.AddRangeAsync(paginas, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverPaginaAsync(LivroPagina pagina, CancellationToken cancellationToken = default)
    {
        _context.LivrosPaginas.Remove(pagina);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddTemaAsync(LivroTema tema, CancellationToken cancellationToken = default)
    {
        await _context.LivrosTemas.AddAsync(tema, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTemaAsync(LivroTema tema, CancellationToken cancellationToken = default)
    {
        _context.LivrosTemas.Update(tema);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverTemaAsync(LivroTema tema, CancellationToken cancellationToken = default)
    {
        _context.LivrosTemas.Remove(tema);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePaginasByLivroIdAsync(Guid livroId, CancellationToken cancellationToken = default)
    {
        await _context.LivrosPaginas
            .Where(p => p.LivroId == livroId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task DeleteTemasByLivroIdAsync(Guid livroId, CancellationToken cancellationToken = default)
    {
        await _context.LivrosTemas
            .Where(t => t.LivroId == livroId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
