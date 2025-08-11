using Domain.Enums;

namespace Application.Dtos;

public class HospitalGetDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public HospitalType Type { get; set; }
}
