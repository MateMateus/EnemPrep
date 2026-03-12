using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class MaterialEstudoConfiguration : IEntityTypeConfiguration<MaterialEstudo>
{
    public void Configure(EntityTypeBuilder<MaterialEstudo> builder)
    {
        builder.ToTable("MateriaisEstudo");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Titulo)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(m => m.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(m => m.UrlArquivo)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(m => m.DataCriacao)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
