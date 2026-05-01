using EnemPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnemPrep.Infrastructure.Configurations;

public class LivroTemaConfiguration : IEntityTypeConfiguration<LivroTema>
{
    public void Configure(EntityTypeBuilder<LivroTema> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.PaginaInicial)
            .IsRequired();

        builder.Property(x => x.PaginaFinal)
            .IsRequired();
            
        builder.HasMany(x => x.Questoes)
            .WithOne(q => q.LivroTema)
            .HasForeignKey(q => q.LivroTemaId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

