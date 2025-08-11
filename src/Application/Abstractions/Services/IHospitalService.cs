using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IHospitalService
{
    Task<long> AddHospitalAsync(HospitalCreateDto hospital);
    Task<ICollection<HospitalGetDto>> GetAllHospitalsAsync();
}
