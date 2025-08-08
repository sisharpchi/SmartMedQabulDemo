using Domain.Enums;

namespace Domain.Entities;

public class Appointment
{
    public long Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public string? Reason { get; set; }
    public string? Location { get; set; }


    public long PatientId { get; set; }
    public Patient Patient { get; set; }

    public long DoctorId { get; set; }
    public Doctor Doctor { get; set; }
}