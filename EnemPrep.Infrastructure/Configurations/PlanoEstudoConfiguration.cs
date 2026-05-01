using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class PlanoEstudoConfiguration : IEntityTypeConfiguration<PlanoEstudo>
{
    public void Configure(EntityTypeBuilder<PlanoEstudo> builder)
    {
        builder.ToTable("PlanosEstudo");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Titulo)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(p => p.DataInicio)
            .IsRequired();

        builder.Property(p => p.DataFim)
            .IsRequired();

        builder.Property(p => p.DataCriacao)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(p => p.Itens)
            .WithOne(i => i.PlanoEstudo)
            .HasForeignKey(i => i.PlanoEstudoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(p => p.Itens).HasField("_itens");

        // Indice para busca de planos por usuario
        builder.HasIndex(p => p.UsuarioId);
    }
}
