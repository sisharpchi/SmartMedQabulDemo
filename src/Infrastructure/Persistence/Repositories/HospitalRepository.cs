using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Persistence.Repositories;

public class HospitalRepository(AppDbContext _context) : IHospitalRepository
{
    public async Task<long> AddHospitalAsync(Hospital hospital)
    {
        await _context.Hospitals.AddAsync(hospital);
        await _context.SaveChangesAsync();
        return hospital.Id;
    }
}
