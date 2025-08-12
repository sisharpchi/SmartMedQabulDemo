using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IPatientRepository
{
    Task<long> AddPatientAsync(Patient patient);
    Task<Patient> GetPatientByUserIdAsync(long userId);
    Task<ICollection<Patient>> GetAllPatientsAsync();
    Task UpdatePatientAsync(Patient patient);
}
