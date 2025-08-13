using AppointmentApi.Domain.Entities;

namespace AppointmentApi.Infrastructure.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(AppDbContext db) : base(db)
    {
    }
}
