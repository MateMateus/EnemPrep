using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class SimuladoQuestaoConfiguration : IEntityTypeConfiguration<SimuladoQuestao>
{
    public void Configure(EntityTypeBuilder<SimuladoQuestao> builder)
    {
        builder.ToTable("SimuladosQuestoes");

        builder.HasKey(sq => sq.Id);

        builder.Property(sq => sq.Ordem)
            .IsRequired();

        builder.Property(sq => sq.DataCriacao)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Relacionamentos configurados na outra ponta (Simulado e Questao) porem confirmamos obrigatorio aqui
        builder.HasOne(sq => sq.Questao)
            .WithMany() // Questão não possui lista inversa para n poluir agregado
            .HasForeignKey(sq => sq.QuestaoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
