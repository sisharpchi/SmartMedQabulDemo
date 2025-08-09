using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IPatientRepository
{
    Task<long> AddPatientAsync(Patient patient);
}
