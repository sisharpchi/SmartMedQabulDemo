using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ScheduleSlotConfiguration : IEntityTypeConfiguration<ScheduleSlot>
{
    public void Configure(EntityTypeBuilder<ScheduleSlot> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.StartTime).IsRequired();
        builder.Property(s => s.EndTime).IsRequired();
        builder.Property(s => s.IsBooked).IsRequired();

        builder.HasOne(s => s.Doctor)
               .WithMany()
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Appointment)
               .WithMany()
               .HasForeignKey(s => s.AppointmentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
