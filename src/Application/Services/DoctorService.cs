using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Mappers;

namespace Application.Services;

public class DoctorService(IDoctorRepository _doctorRepo) : IDoctorService
{
    public async Task ConfirmDoctorAsync(long doctorId, long hospitalId)
    {
        await _doctorRepo.ConfirmDoctorAsync(doctorId, hospitalId);
    }

    public async Task<ICollection<DoctorGetDto>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorRepo.GetAllDoctorsAsync();
        return doctors.Select(MapperService.GetAllDoctorsConverter).ToList();
    }

    public async Task<ICollection<UnConfirmedDoctorGetDto>> GetAllUnConfirmedDoctorsAsync()
    {
        var doctors = await _doctorRepo.GetAllUnConfirmedDoctorsAsync();
        return doctors.Select(MapperService.Converter).ToList();
    }
}
