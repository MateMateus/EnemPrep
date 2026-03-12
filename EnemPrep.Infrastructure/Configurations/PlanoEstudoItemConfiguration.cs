using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class PlanoEstudoItemConfiguration : IEntityTypeConfiguration<PlanoEstudoItem>
{
    public void Configure(EntityTypeBuilder<PlanoEstudoItem> builder)
    {
        builder.ToTable("PlanosEstudoItens");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.DataPrevista)
            .IsRequired();

        builder.Property(i => i.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(i => i.DataCriacao)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasOne(i => i.Assunto)
            .WithMany()
            .HasForeignKey(i => i.AssuntoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
