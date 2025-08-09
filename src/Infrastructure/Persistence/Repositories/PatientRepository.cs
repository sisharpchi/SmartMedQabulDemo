using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Persistence.Repositories;

public class PatientRepository(AppDbContext _context) : IPatientRepository
{
    public async Task<long> AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient.Id;
    }
}
