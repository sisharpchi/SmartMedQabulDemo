using AppointmentApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApi.Infrastructure.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<Appointment>> ListByDoctor(long doctorId)
        => await _db.Appointments.Where(a => a.DoctorId == doctorId)
            .Include(a => a.ScheduleSlot)
            .Include(a => a.Patient).ToListAsync();

    public async Task<List<Appointment>> ListByPatient(long patientId)
        => await _db.Appointments.Where(a => a.PatientId == patientId)
            .Include(a => a.ScheduleSlot)
            .Include(a => a.Doctor).ToListAsync();

    public async Task<Appointment?> GetWithPayment(long id)
        => await _db.Appointments.Include(a => a.Payment)
            .Include(a => a.ScheduleSlot)
            .FirstOrDefaultAsync(a => a.Id == id);
}
