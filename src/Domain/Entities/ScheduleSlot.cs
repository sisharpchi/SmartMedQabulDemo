namespace Domain.Entities;

public class ScheduleSlot
{
    public long Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsBooked { get; set; }

    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public long? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
}