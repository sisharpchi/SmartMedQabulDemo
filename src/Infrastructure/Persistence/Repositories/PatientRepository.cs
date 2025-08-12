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

    public async Task<Patient> GetPatientByIdAsync(long patientId)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(x=>x.Id == patientId);
        if(patient == null)
        {
            throw new EntityNotFoundException($"Patient not found with id {patientId}");
        }
        return patient;
    }

    public async Task UpdatePatientAsync(Patient patient)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync();
    }
}
