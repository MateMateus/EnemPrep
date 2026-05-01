using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class DesafioDiarioConfiguration : IEntityTypeConfiguration<DesafioDiario>
{
    public void Configure(EntityTypeBuilder<DesafioDiario> builder)
    {
        builder.ToTable("DesafiosDiarios");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Titulo)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(d => d.DataDesafio)
            .IsRequired();

        builder.Property(d => d.XPRecompensa)
            .IsRequired();

        builder.Property(d => d.DataCriacao)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(d => d.Questao)
            .WithMany()
            .HasForeignKey(d => d.QuestaoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
