using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.Comment).IsRequired().HasMaxLength(1000);
        builder.Property(r => r.CreatedAt).IsRequired();

        builder.HasOne(r => r.Doctor)
               .WithMany()
               .HasForeignKey(r => r.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Patient)
               .WithMany()
               .HasForeignKey(r => r.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Appointment)
               .WithMany()
               .HasForeignKey(r => r.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
