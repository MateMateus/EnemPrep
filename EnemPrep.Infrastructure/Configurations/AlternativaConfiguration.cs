using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EnemPrep.Domain.Entities;

namespace EnemPrep.Infrastructure.Configurations;

public class AlternativaConfiguration : IEntityTypeConfiguration<Alternativa>
{
    public void Configure(EntityTypeBuilder<Alternativa> builder)
    {
        builder.ToTable("Alternativas");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Texto)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(a => a.Correta)
            .IsRequired();
    }
}
