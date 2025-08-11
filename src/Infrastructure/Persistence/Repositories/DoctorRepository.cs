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

    public async Task ConfirmDoctorAsync(long doctorId, long hospitalId)
    {
        var hospitalExists = await _context.Hospitals.AnyAsync(x=>x.Id == hospitalId);
        if (!hospitalExists)
        {
            throw new EntityNotFoundException($"Hospital not found with id {hospitalId}");
        }
        var doctor = await GetDoctorByIdAsync(doctorId);
        doctor.HospitalId = hospitalId;
        doctor.IsConfirmedByAdmin = true;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDoctorAsync(long id)
    {
        var doctor = await GetDoctorByIdAsync(id);
        _context.Doctors.Remove(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Doctor>> GetAllDoctorsAsync()
    {
        return await _context.Doctors.Include(x=>x.User).Include(x=>x.Hospital).ToListAsync();
    }

    public async Task<ICollection<Doctor>> GetAllDoctorsByHospitalIdAsync(long hospitalId)
    {
        return await _context.Doctors.Where(x => x.HospitalId == hospitalId).ToListAsync();
    }

    public async Task<ICollection<Doctor>> GetAllUnConfirmedDoctorsAsync()
    {
        return await _context.Doctors
            .Include(x=>x.User)
            .ThenInclude(x=>x.Confirmer)
            .Include(x=>x.User).ThenInclude(x=>x.Role)
            .Where(x => x.IsConfirmedByAdmin == false && x.User!.Confirmer!.IsConfirmed == true)
            .ToListAsync();
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
