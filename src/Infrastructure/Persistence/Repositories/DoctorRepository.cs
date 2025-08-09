using Application.Abstractions.Repositories;
using Core.Errors;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class DoctorRepository(AppDbContext _context) : IDoctorRepository
{
    public async Task<long> AddDoctorAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
        return doctor.Id;
    }

    public async Task DeleteDoctorAsync(long id)
    {
        var doctor = await GetDoctorByIdAsync(id);
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors.ToListAsync();
    }

    public async Task<ICollection<Doctor>> GetAllDoctorsByHospitalIdAsync(long hospitalId)
    {
        return await _context.Doctors.Where(x => x.HospitalId == hospitalId).ToListAsync();
    }

    public async Task<Doctor> GetDoctorByIdAsync(long id)
    {
        var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        if (doctor == null)
        {
            throw new EntityNotFoundException($"Doctor not found with id {id}");
        }
        return doctor;
    }

    public async Task UpdateDoctorAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }
}
