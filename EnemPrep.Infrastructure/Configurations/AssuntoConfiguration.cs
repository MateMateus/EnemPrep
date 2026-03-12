using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class AssuntoConfiguration : IEntityTypeConfiguration<Assunto>
{
    public void Configure(EntityTypeBuilder<Assunto> builder)
    {
        builder.ToTable("Assuntos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Descricao)
            .HasMaxLength(1000);

        builder.Property(a => a.DataCriacao)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasMany(a => a.Questoes)
            .WithOne(q => q.Assunto)
            .HasForeignKey(q => q.AssuntoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.VideoAulas)
            .WithOne(v => v.Assunto)
            .HasForeignKey(v => v.AssuntoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.MateriaisEstudo)
            .WithOne(m => m.Assunto)
            .HasForeignKey(m => m.AssuntoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(a => a.Questoes).HasField("_questoes");
        builder.Navigation(a => a.VideoAulas).HasField("_videoAulas");
        builder.Navigation(a => a.MateriaisEstudo).HasField("_materiaisEstudo");
    }
}
