using AppointmentApi.Domain.Entities;

namespace AppointmentApi.Infrastructure.Repositories;

public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    public DoctorRepository(AppDbContext db) : base(db)
    {
    }
}
