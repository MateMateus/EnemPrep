using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class MateriaConfiguration : IEntityTypeConfiguration<Materia>
{
    public void Configure(EntityTypeBuilder<Materia> builder)
    {
        builder.ToTable("Materias");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(m => m.Descricao)
            .HasMaxLength(1000);
        builder.HasMany(m => m.Assuntos)
            .WithOne(a => a.Materia)
            .HasForeignKey(a => a.MateriaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(m => m.Assuntos).HasField("_assuntos");
    }
}
