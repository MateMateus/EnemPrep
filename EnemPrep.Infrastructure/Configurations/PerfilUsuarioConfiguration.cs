using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class PerfilUsuarioConfiguration : IEntityTypeConfiguration<PerfilUsuario>
{
    public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
    {
        builder.ToTable("PerfisUsuario");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(p => p.NomeApresentacao)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.DataCriacao)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Navigation(p => p.Usuarios).HasField("_usuarios");
    }
}
