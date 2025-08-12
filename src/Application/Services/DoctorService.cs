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
        return doctors.Select(MapperService.GetDoctorConverter).ToList();
    }

    public async Task<ICollection<DoctorGetDto>> GetAllDoctorsByHospitalIdAsync(long hospitalId)
    {
        var doctors = await _doctorRepo.GetAllDoctorsByHospitalIdAsync(hospitalId);
        return doctors.Select(MapperService.GetDoctorConverter).ToList();
    }

    public async Task<ICollection<UnConfirmedDoctorGetDto>> GetAllUnConfirmedDoctorsAsync()
    {
        var doctors = await _doctorRepo.GetAllUnConfirmedDoctorsAsync();
        return doctors.Select(MapperService.Converter).ToList();
    }

    public async Task<DoctorGetDto> GetDoctorByIdAsync(long id)
    {
        return MapperService.GetDoctorConverter(await _doctorRepo.GetDoctorByIdAsync(id));
    }

    public async Task UpdateDoctorAsync(DoctorUpdateDto doctor, long doctorId)
    {
        var doctorEntity = await _doctorRepo.GetDoctorByIdAsync(doctorId);
        doctorEntity.Experience = doctor.Experience;
        doctor.Specialty = doctor.Specialty;
        doctor.WorkStartTime = doctor.WorkStartTime;
        doctor.WorkEndTime = doctor.WorkEndTime;
        doctor.Description = doctor.Description;
        await _doctorRepo.UpdateDoctorAsync(doctorEntity);
    }
}
