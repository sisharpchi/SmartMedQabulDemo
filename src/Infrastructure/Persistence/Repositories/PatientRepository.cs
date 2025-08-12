using Application.Abstractions.Repositories;
using Core.Errors;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PatientRepository(AppDbContext _context) : IPatientRepository
{
    public async Task<long> AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient.Id;
    }

    public async Task<ICollection<Patient>> GetAllPatientsAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<Patient> GetPatientByUserIdAsync(long userId)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(x => x.UserId == userId);
        if (patient == null)
        {
            throw new EntityNotFoundException($"Patient not found with user id {userId}");
        }
        return patient;
    }

    public async Task UpdatePatientAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }
}
