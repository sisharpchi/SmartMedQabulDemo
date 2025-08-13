using AppointmentApi.Domain.Entities;
using AppointmentApi.Domain.Enums;
using AppointmentApi.Infrastructure.Repositories;

namespace AppointmentApi.Application.Services;

public class PaymentService
{
    private readonly IPaymentProvider _provider;
    private readonly IPaymentRepository _payments;

    public PaymentService(IPaymentProvider provider, IPaymentRepository payments)
    {
        _provider = provider;
        _payments = payments;
    }

    public async Task<Payment> ChargeAsync(Appointment appointment, PaymentMethod method)
    {
        var payment = new Payment
        {
            AppointmentId = appointment.Id,
            Amount = appointment.ScheduleSlot.Price,
            Currency = "USD",
            Method = method,
            Status = PaymentStatus.Pending,
            CreatedAtUtc = DateTime.UtcNow,
            UpdatedAtUtc = DateTime.UtcNow
        };
        await _payments.AddAsync(payment);
        var ok = await _provider.ChargeAsync(payment.Amount, payment.Currency);
        payment.Status = ok ? PaymentStatus.Succeeded : PaymentStatus.Failed;
        payment.UpdatedAtUtc = DateTime.UtcNow;
        await _payments.SaveChangesAsync();
        return payment;
    }

    public async Task<bool> RefundAsync(Payment payment)
    {
        var ok = await _provider.RefundAsync(payment.Id);
        if (ok)
        {
            payment.Status = PaymentStatus.Refunded;
            payment.UpdatedAtUtc = DateTime.UtcNow;
            await _payments.UpdateAsync(payment);
            await _payments.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
