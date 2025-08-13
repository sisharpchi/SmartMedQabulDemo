namespace AppointmentApi.Domain.Entities;

public class Doctor
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = null!;
    public string Specialty { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public string? Description { get; set; }
    public int? Experience { get; set; }
    public bool IsConfirmedByAdmin { get; set; }
    public TimeOnly WorkStartTime { get; set; }
    public TimeOnly WorkEndTime { get; set; }
    public TimeOnly LunchStart { get; set; }
    public TimeOnly LunchEnd { get; set; }
    public int ConsultationDurationMinutes { get; set; } = 60;
    public decimal DefaultSlotPrice { get; set; }
    public long? HospitalId { get; set; }
    public Hospital? Hospital { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<ScheduleSlot> ScheduleSlots { get; set; } = new List<ScheduleSlot>();
}
