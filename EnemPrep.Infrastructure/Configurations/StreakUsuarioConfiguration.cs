using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class StreakUsuarioConfiguration : IEntityTypeConfiguration<StreakUsuario>
{
    public void Configure(EntityTypeBuilder<StreakUsuario> builder)
    {
        builder.ToTable("StreaksUsuario");

        builder.HasKey(s => s.Id);

        builder.HasIndex(s => s.UsuarioId)
            .IsUnique();

        builder.Property(s => s.DiasConsecutivos)
            .IsRequired();

        builder.Property(s => s.MaiorStreak)
            .IsRequired();

        builder.Property(s => s.UltimaAtividade)
            .IsRequired();
    }
}
