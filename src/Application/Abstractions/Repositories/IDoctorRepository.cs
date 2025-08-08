using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IDoctorRepository
{
    Task<long> AddDoctorAsync(Doctor doctor);
    Task<Doctor> GetDoctorByIdAsync(long id);
    Task<ICollection<Doctor>> GetAllDoctorsAsync();
    Task UpdateDoctorAsync(Doctor doctor);
    Task DeleteDoctorAsync(long id);

    Task<Doctor> GetAllDoctorsByHospitalIdAsync(long hospitalId);
}
