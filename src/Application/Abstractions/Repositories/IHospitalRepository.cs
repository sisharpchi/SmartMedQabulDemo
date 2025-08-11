using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IHospitalRepository
{
    Task<long> AddHospitalAsync(Hospital hospital);
}
