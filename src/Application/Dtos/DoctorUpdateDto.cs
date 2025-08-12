namespace Application.Dtos;

public class DoctorUpdateDto
{
    public string? Specialty { get; set; }
    public string? Description { get; set; }
    public string? Experience { get; set; }
    public TimeOnly? WorkStartTime { get; set; }
    public TimeOnly? WorkEndTime { get; set; }
}
