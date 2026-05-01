using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class TentativaSimuladoConfiguration : IEntityTypeConfiguration<TentativaSimulado>
{
    public void Configure(EntityTypeBuilder<TentativaSimulado> builder)
    {
        builder.ToTable("TentativasSimulado");

        builder.HasKey(ts => ts.Id);

        builder.Property(ts => ts.DataInicio)
            .IsRequired();

        builder.Property(ts => ts.DataFim)
            .IsRequired(false);

        builder.Property(ts => ts.NotaTotalBruta)
            .IsRequired(false);

        builder.Property(ts => ts.DataCriacao)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(ts => ts.Usuario)
            .WithMany() // Usuario nao tem colecao de tentativas de simulado
            .HasForeignKey(ts => ts.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ts => ts.Respostas)
            .WithOne(rs => rs.TentativaSimulado)
            .HasForeignKey(rs => rs.TentativaSimuladoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(ts => ts.Respostas).HasField("_respostas");

        // Indice para historico por usuario
        builder.HasIndex(ts => ts.UsuarioId);
    }
}
