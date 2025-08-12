using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IPatientRepository
{
    Task<long> AddPatientAsync(Patient patient);
    Task<Patient> GetPatientByIdAsync(long patientId);
    Task UpdatePatientAsync(Patient patient);
}
