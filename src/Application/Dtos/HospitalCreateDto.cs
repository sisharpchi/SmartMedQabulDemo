using Domain.Enums;

namespace Application.Dtos;

public class HospitalCreateDto
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public HospitalType Type { get; set; }
}
