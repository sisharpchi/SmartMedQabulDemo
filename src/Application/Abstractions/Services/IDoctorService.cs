using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IDoctorService
{
    Task<ICollection<UnConfirmedDoctorGetDto>> GetAllUnConfirmedDoctorsAsync();
    Task ConfirmDoctorAsync(long doctorId, long hospitalId);
    Task<ICollection<DoctorGetDto>> GetAllDoctorsAsync();
    Task<DoctorGetDto> GetDoctorByIdAsync(long id);
    //Task UpdateDoctorAsync(Doctor doctor);
    Task<ICollection<DoctorGetDto>> GetAllDoctorsByHospitalIdAsync(long hospitalId);
}
