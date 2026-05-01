using EnemPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnemPrep.Infrastructure.Configurations;

public class LivroPaginaConfiguration : IEntityTypeConfiguration<LivroPagina>
{
    public void Configure(EntityTypeBuilder<LivroPagina> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NumeroProprio)
            .IsRequired();

        builder.Property(x => x.UrlImagem)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasIndex(x => new { x.LivroId, x.NumeroProprio })
            .IsUnique();
    }
}
