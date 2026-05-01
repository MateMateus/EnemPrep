using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.DataCriacao)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(u => u.PerfilUsuario)
            .WithMany(p => p.Usuarios)
            .HasForeignKey(u => u.PerfilUsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Tentativas)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.UsuarioId);

        builder.HasMany(u => u.PlanosEstudo)
            .WithOne(p => p.Usuario)
            .HasForeignKey(p => p.UsuarioId);

        builder.HasMany(u => u.Conquistas)
            .WithOne(uc => uc.Usuario)
            .HasForeignKey(uc => uc.UsuarioId);

        builder.HasOne(u => u.Streak)
            .WithOne(s => s.Usuario)
            .HasForeignKey<StreakUsuario>(s => s.UsuarioId);

        builder.Navigation(u => u.Tentativas).HasField("_tentativas");
        builder.Navigation(u => u.PlanosEstudo).HasField("_planosEstudo");
        builder.Navigation(u => u.Conquistas).HasField("_conquistas");
    }
}
