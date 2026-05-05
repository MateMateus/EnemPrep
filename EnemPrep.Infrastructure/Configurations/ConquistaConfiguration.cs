using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class ConquistaConfiguration : IEntityTypeConfiguration<Conquista>
{
    public void Configure(EntityTypeBuilder<Conquista> builder)
    {
        builder.ToTable("Conquistas");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.Icone)
            .HasMaxLength(200);

        builder.Property(c => c.PontosZ)
            .IsRequired();
        builder.HasMany(c => c.UsuariosConquista)
            .WithOne(uc => uc.Conquista)
            .HasForeignKey(uc => uc.ConquistaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(c => c.UsuariosConquista).HasField("_usuariosConquista");
    }
}
