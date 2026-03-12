using EnemPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnemPrep.Infrastructure.Persistence.Configurations;

public class UsuarioConquistaConfiguration : IEntityTypeConfiguration<UsuarioConquista>
{
    public void Configure(EntityTypeBuilder<UsuarioConquista> builder)
    {
        builder.ToTable("UsuarioConquistas");

        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.DataObtencao)
            .IsRequired();

        builder.HasOne(uc => uc.Usuario)
            .WithMany(u => u.Conquistas)
            .HasForeignKey(uc => uc.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uc => uc.Conquista)
            .WithMany(c => c.UsuariosConquista)
            .HasForeignKey(uc => uc.ConquistaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(uc => new { uc.UsuarioId, uc.ConquistaId }).IsUnique();
    }
}
