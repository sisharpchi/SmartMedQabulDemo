using AppointmentApi.Domain.Entities;

namespace AppointmentApi.Infrastructure.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<List<Appointment>> ListByDoctor(long doctorId);
    Task<List<Appointment>> ListByPatient(long patientId);
    Task<Appointment?> GetWithPayment(long id);
}
