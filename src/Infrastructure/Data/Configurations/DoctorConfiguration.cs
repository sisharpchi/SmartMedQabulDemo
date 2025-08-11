using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Specialty).IsRequired(false).HasMaxLength(100);
        builder.Property(d => d.Description).IsRequired(false).HasMaxLength(1000);
        builder.Property(d => d.Experience).HasMaxLength(500).IsRequired(false);

        builder.Property(d => d.WorkStartTime).IsRequired(false);
        builder.Property(d => d.WorkEndTime).IsRequired(false);

        builder.HasOne(d => d.User)
               .WithOne(x=>x.Doctor)
               .HasForeignKey<Doctor>(d => d.UserId);

        builder.HasOne(d => d.Hospital)
               .WithMany(h => h.Doctors)
               .HasForeignKey(d => d.HospitalId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
