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

    public async Task<ICollection<HospitalGetDto>> GetAllHospitalsAsync()
    {
        var hospitals = await _hospitalRepo.GetAllHospitalsAsync();
        return hospitals.Select(Converter).ToList();
    }

    private HospitalGetDto Converter(Hospital hospital)
    {
        return new HospitalGetDto
        {
            Address =hospital.Address,
            Id = hospital.Id,
            Name = hospital.Name,
            Type = hospital.Type,
        };
    }
}
