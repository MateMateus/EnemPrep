using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class QuestaoConfiguration : IEntityTypeConfiguration<Questao>
{
    public void Configure(EntityTypeBuilder<Questao> builder)
    {
        builder.ToTable("Questoes");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Enunciado)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(q => q.Dificuldade)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(q => q.Explicacao)
            .HasMaxLength(5000);

        builder.Property(q => q.VideoExplicacaoUrl)
            .HasMaxLength(500);

        builder.Property(q => q.DataCriacao)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(q => q.Alternativas)
            .WithOne(a => a.Questao)
            .HasForeignKey(a => a.QuestaoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.Tentativas)
            .WithOne(t => t.Questao)
            .HasForeignKey(t => t.QuestaoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(q => q.Alternativas).HasField("_alternativas");
        builder.Navigation(q => q.Tentativas).HasField("_tentativas");

        // Indices para filtros frequentes
        builder.HasIndex(q => q.AssuntoId);
        builder.HasIndex(q => q.LivroTemaId);
        builder.HasIndex(q => q.LivroId);
    }
}
