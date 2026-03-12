using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class UsuarioConquistaConfiguration : IEntityTypeConfiguration<UsuarioConquista>
{
    public void Configure(EntityTypeBuilder<UsuarioConquista> builder)
    {
        builder.ToTable("UsuarioConquistas");

        builder.HasKey(uc => uc.Id);

        builder.Property(uc => uc.DataObtencao)
            .IsRequired();

        builder.Property(uc => uc.DataCriacao)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(uc => new { uc.UsuarioId, uc.ConquistaId })
            .IsUnique();
    }
}
