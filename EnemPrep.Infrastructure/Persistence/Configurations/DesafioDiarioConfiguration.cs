using EnemPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnemPrep.Infrastructure.Persistence.Configurations;

public class DesafioDiarioConfiguration : IEntityTypeConfiguration<DesafioDiario>
{
    public void Configure(EntityTypeBuilder<DesafioDiario> builder)
    {
        builder.ToTable("DesafiosDiarios");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.DataDesafio)
            .IsRequired();

        builder.Property(d => d.XPRecompensa)
            .IsRequired();

        builder.HasOne(d => d.Questao)
            .WithMany()
            .HasForeignKey(d => d.QuestaoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
