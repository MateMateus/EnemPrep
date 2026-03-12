using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;

namespace EnemPrep.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EnemPrepDbContext _context;

    public UsuarioRepository(EnemPrepDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .Include(u => u.PerfilUsuario)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .Include(u => u.PerfilUsuario)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        await _context.Usuarios.AddAsync(usuario, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Usuario usuario, CancellationToken cancellationToken = default)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<PerfilUsuario?> GetPerfilByTipoAsync(EnemPrep.Domain.Enums.TipoPerfil tipo, CancellationToken cancellationToken = default)
    {
        return await _context.PerfisUsuario
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Tipo == tipo, cancellationToken);
    }
}
