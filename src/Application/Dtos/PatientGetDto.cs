namespace Application.Dtos;

public class PatientGetDto
{
    public long Id { get; set; }
    public string? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public long UserId { get; set; }
}
