using Application.Dtos;

namespace Application.Abstractions.Services;

public interface IHospitalService
{
    Task<long> AddHospitalAsync(HospitalCreateDto hospital);
}
