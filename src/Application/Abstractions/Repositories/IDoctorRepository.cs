using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IDoctorRepository
{
    Task<long> AddDoctorAsync(Doctor doctor);
    Task<Doctor> GetDoctorByIdAsync(long id);
    Task<ICollection<Doctor>> GetAllDoctorsAsync();
    Task<ICollection<Doctor>> GetAllUnConfirmedDoctorsAsync();
    Task UpdateDoctorAsync(Doctor doctor);
    Task ConfirmDoctorAsync(long doctorId,long hospitalId);
    Task<ICollection<Doctor>> GetAllDoctorsByHospitalIdAsync(long hospitalId);
}
