using AppointmentApi.Domain.Enums;

namespace AppointmentApi.Domain.Entities;

public class Appointment
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public long PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public long ScheduleSlotId { get; set; }
    public ScheduleSlot ScheduleSlot { get; set; } = null!;
    public DateTime AppointmentDateUtc { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Payment? Payment { get; set; }
}
