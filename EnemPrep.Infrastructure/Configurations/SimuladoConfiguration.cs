using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class SimuladoConfiguration : IEntityTypeConfiguration<Simulado>
{
    public void Configure(EntityTypeBuilder<Simulado> builder)
    {
        builder.ToTable("Simulados");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.AnoReferencia)
            .IsRequired(false);

        builder.Property(s => s.DuracaoMaxima)
            .IsRequired();

        builder.Property(s => s.DataCriacao)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasMany(s => s.Questoes)
            .WithOne(sq => sq.Simulado)
            .HasForeignKey(sq => sq.SimuladoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Tentativas)
            .WithOne(ts => ts.Simulado)
            .HasForeignKey(ts => ts.SimuladoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(s => s.Questoes).HasField("_questoes");
        builder.Navigation(s => s.Tentativas).HasField("_tentativas");
    }
}
