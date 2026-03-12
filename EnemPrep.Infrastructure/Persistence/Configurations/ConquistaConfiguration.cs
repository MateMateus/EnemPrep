using EnemPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnemPrep.Infrastructure.Persistence.Configurations;

public class ConquistaConfiguration : IEntityTypeConfiguration<Conquista>
{
    public void Configure(EntityTypeBuilder<Conquista> builder)
    {
        builder.ToTable("Conquistas");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Titulo)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Icone)
            .HasMaxLength(50);

        builder.Property(c => c.PontosZ)
            .IsRequired();
    }
}
