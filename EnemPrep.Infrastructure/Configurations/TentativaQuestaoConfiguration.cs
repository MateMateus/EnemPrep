using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class TentativaQuestaoConfiguration : IEntityTypeConfiguration<TentativaQuestao>
{
    public void Configure(EntityTypeBuilder<TentativaQuestao> builder)
    {
        builder.ToTable("TentativasQuestao");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Acertou)
            .IsRequired();

        builder.Property(t => t.TempoGastoSegundos)
            .IsRequired();
        builder.HasOne(t => t.AlternativaSelecionada)
            .WithMany()
            .HasForeignKey(t => t.AlternativaSelecionadaId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        // Indices para historico e estatisticas por usuario
        builder.HasIndex(t => t.UsuarioId);
        builder.HasIndex(t => t.QuestaoId);
        builder.HasIndex(t => new { t.UsuarioId, t.QuestaoId });
    }
}
