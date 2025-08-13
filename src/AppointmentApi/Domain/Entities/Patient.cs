namespace AppointmentApi.Domain.Entities;

public class Patient
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public string? Notes { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
