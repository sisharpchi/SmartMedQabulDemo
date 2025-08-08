namespace Domain.Entities;

public class Doctor
{
    public long Id { get; set; }
    public string? Specialty { get; set; }
    public double? Rating { get; set; }
    public string? Description { get; set; }
    public string? Experience { get; set; }
    public bool IsConfirmedByAdmin { get; set; } = false;

    public TimeOnly? WorkStartTime { get; set; }
    public TimeOnly? WorkEndTime { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long? HospitalId { get; set; }
    public Hospital? Hospital { get; set; }

    public ICollection<Appointment>? Appointments { get; set; }
}