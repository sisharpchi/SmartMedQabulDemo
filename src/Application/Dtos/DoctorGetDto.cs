using Domain.Entities;

namespace Application.Dtos;

public class DoctorGetDto
{
    public long Id { get; set; }
    public string? Specialty { get; set; }
    public double? Rating { get; set; }
    public string? Description { get; set; }
    public string? Experience { get; set; }

    public TimeOnly? WorkStartTime { get; set; }
    public TimeOnly? WorkEndTime { get; set; }

    public UserDto User { get; set; }
    public HospitalGetDto? Hospital { get; set; }
}
