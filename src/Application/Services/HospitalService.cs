using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class HospitalService(IHospitalRepository _hospitalRepo) : IHospitalService
{
    public async Task<long> AddHospitalAsync(HospitalCreateDto hospital)
    {
        return await _hospitalRepo.AddHospitalAsync(new Hospital
        {
            Address = hospital.Address,
            Name = hospital.Name,
            Type = hospital.Type,
        });
    }
}
