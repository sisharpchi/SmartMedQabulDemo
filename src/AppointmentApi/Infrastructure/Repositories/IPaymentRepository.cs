using AppointmentApi.Domain.Entities;

namespace AppointmentApi.Infrastructure.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment?> GetByAppointmentId(long appointmentId);
}
