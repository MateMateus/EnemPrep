using Microsoft.EntityFrameworkCore;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Persistence;

public class EnemPrepDbContext : DbContext
{
    public EnemPrepDbContext(DbContextOptions<EnemPrepDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<PerfilUsuario> PerfisUsuario => Set<PerfilUsuario>();
    public DbSet<Materia> Materias => Set<Materia>();
    public DbSet<Assunto> Assuntos => Set<Assunto>();
    public DbSet<Questao> Questoes => Set<Questao>();
    public DbSet<Alternativa> Alternativas => Set<Alternativa>();
    public DbSet<TentativaQuestao> TentativasQuestao => Set<TentativaQuestao>();
    public DbSet<VideoAula> VideoAulas => Set<VideoAula>();
    public DbSet<MaterialEstudo> MateriaisEstudo => Set<MaterialEstudo>();
    public DbSet<PlanoEstudo> PlanosEstudo => Set<PlanoEstudo>();
    public DbSet<PlanoEstudoItem> PlanosEstudoItens => Set<PlanoEstudoItem>();
    public DbSet<StreakUsuario> StreaksUsuario => Set<StreakUsuario>();
    public DbSet<Conquista> Conquistas => Set<Conquista>();
    public DbSet<UsuarioConquista> UsuarioConquistas => Set<UsuarioConquista>();
    public DbSet<DesafioDiario> DesafiosDiarios => Set<DesafioDiario>();

    public DbSet<Simulado> Simulados => Set<Simulado>();
    public DbSet<SimuladoQuestao> SimuladosQuestoes => Set<SimuladoQuestao>();
    public DbSet<TentativaSimulado> TentativasSimulado => Set<TentativaSimulado>();
    public DbSet<RespostaSimulado> RespostasSimulado => Set<RespostaSimulado>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EnemPrepDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.AtualizarData();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
