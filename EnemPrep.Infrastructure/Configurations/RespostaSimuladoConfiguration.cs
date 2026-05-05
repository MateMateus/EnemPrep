using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class RespostaSimuladoConfiguration : IEntityTypeConfiguration<RespostaSimulado>
{
    public void Configure(EntityTypeBuilder<RespostaSimulado> builder)
    {
        builder.ToTable("RespostasSimulado");

        builder.HasKey(rs => rs.Id);

        builder.Property(rs => rs.Correta)
            .IsRequired();
        // Pode ser nulo se passou em branco
        builder.Property(rs => rs.AlternativaId)
            .IsRequired(false);

        builder.HasOne(rs => rs.Questao)
            .WithMany()
            .HasForeignKey(rs => rs.QuestaoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rs => rs.Alternativa)
            .WithMany()
            .HasForeignKey(rs => rs.AlternativaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
