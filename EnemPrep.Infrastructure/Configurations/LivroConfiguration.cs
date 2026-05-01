using EnemPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnemPrep.Infrastructure.Configurations;

public class LivroConfiguration : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Descricao)
            .HasMaxLength(2000);

        builder.Property(x => x.UrlCapa)
            .HasMaxLength(500);

        builder.Property(x => x.TipoConteudo)
            .IsRequired();

        builder.HasOne(x => x.Materia)
            .WithMany()
            .HasForeignKey(x => x.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Paginas)
            .WithOne(p => p.Livro)
            .HasForeignKey(p => p.LivroId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Temas)
            .WithOne(t => t.Livro)
            .HasForeignKey(t => t.LivroId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(x => x.Questoes)
            .WithOne(q => q.Livro)
            .HasForeignKey(q => q.LivroId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        // Indices para filtros da listagem de livros
        builder.HasIndex(x => x.MateriaId);
        builder.HasIndex(x => x.TipoConteudo);
    }
}
