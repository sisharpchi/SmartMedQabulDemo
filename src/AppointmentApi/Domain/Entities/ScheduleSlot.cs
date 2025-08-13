namespace AppointmentApi.Domain.Entities;

public class ScheduleSlot
{
    public long Id { get; set; }
    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public decimal Price { get; set; }
    public bool IsBooked { get; set; }
    public long? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
}
