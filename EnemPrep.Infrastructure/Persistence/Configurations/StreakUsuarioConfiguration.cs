using EnemPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnemPrep.Infrastructure.Persistence.Configurations;

public class StreakUsuarioConfiguration : IEntityTypeConfiguration<StreakUsuario>
{
    public void Configure(EntityTypeBuilder<StreakUsuario> builder)
    {
        builder.ToTable("StreaksUsuario");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.DiasConsecutivos)
            .IsRequired();

        builder.Property(s => s.MaiorStreak)
            .IsRequired();

        builder.Property(s => s.UltimaAtividade)
            .IsRequired();

        builder.HasOne(s => s.Usuario)
            .WithOne(u => u.Streak)
            .HasForeignKey<StreakUsuario>(s => s.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
