using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class HospitalRepository(AppDbContext _context) : IHospitalRepository
{
    public async Task<long> AddHospitalAsync(Hospital hospital)
    {
        await _context.Hospitals.AddAsync(hospital);
        await _context.SaveChangesAsync();
        return hospital.Id;
    }

    public async Task<ICollection<Hospital>> GetAllHospitalsAsync()
    {
        return await _context.Hospitals.ToListAsync();
    }
}
