namespace Domain.Entities;

public class Patient
{
    public long Id { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}