using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Gender).HasMaxLength(50).IsRequired(false);

        builder.HasOne(p => p.User)
               .WithOne(x=>x.Patient)
               .HasForeignKey<Patient>(p => p.UserId);
    }
}
