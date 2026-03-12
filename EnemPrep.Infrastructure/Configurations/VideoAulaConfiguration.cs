using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class VideoAulaConfiguration : IEntityTypeConfiguration<VideoAula>
{
    public void Configure(EntityTypeBuilder<VideoAula> builder)
    {
        builder.ToTable("VideoAulas");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Titulo)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(v => v.UrlVideo)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(v => v.DuracaoSegundos)
            .IsRequired();

        builder.Property(v => v.DataCriacao)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
