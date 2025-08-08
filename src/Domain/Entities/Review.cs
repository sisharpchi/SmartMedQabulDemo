namespace Domain.Entities;

public class Review
{
    public long Id { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public long? AppointmentId { get; set; }
    public Appointment? Appointment { get; set; }
}