using Domain.Entities;

namespace Application.Dtos;

public class UnConfirmedDoctorGetDto
{
    public long Id { get; set; }
    public bool IsConfirmedByAdmin { get; set; }
    public UserGetDto User { get; set; }
}
