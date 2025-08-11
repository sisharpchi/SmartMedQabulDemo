using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IDoctorService
{
    Task<ICollection<UnConfirmedDoctorGetDto>> GetAllUnConfirmedDoctorsAsync();
}
