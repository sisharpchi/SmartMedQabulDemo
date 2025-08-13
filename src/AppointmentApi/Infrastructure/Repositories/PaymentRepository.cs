using AppointmentApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApi.Infrastructure.Repositories;

public class PaymentRepository : Repository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<Payment?> GetByAppointmentId(long appointmentId)
        => await _db.Payments.FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);
}
