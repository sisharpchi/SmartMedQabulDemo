using AppointmentApi.Domain.Enums;
using AppointmentApi.Infrastructure.Repositories;

namespace AppointmentApi.Application.Services;

public class DoctorDecisionService
{
    private readonly IAppointmentRepository _appointments;
    private readonly IScheduleSlotRepository _slots;
    private readonly IPaymentRepository _payments;
    private readonly PaymentService _paymentService;

    public DoctorDecisionService(IAppointmentRepository appointments, IScheduleSlotRepository slots,
        IPaymentRepository payments, PaymentService paymentService)
    {
        _appointments = appointments;
        _slots = slots;
        _payments = payments;
        _paymentService = paymentService;
    }

    public async Task ApproveAsync(long doctorId, long appointmentId)
    {
        var appt = await _appointments.GetWithPayment(appointmentId) ?? throw new ArgumentException("Not found");
        if (appt.DoctorId != doctorId) throw new UnauthorizedAccessException();
        if (appt.Status != AppointmentStatus.Pending) throw new InvalidOperationException();
        appt.Status = AppointmentStatus.Approved;
        await _appointments.UpdateAsync(appt);
        await _appointments.SaveChangesAsync();
    }

    public async Task DeclineAsync(long doctorId, long appointmentId)
    {
        var appt = await _appointments.GetWithPayment(appointmentId) ?? throw new ArgumentException("Not found");
        if (appt.DoctorId != doctorId) throw new UnauthorizedAccessException();
        if (appt.Status != AppointmentStatus.Pending) throw new InvalidOperationException();
        appt.Status = AppointmentStatus.Cancelled;
        await _appointments.UpdateAsync(appt);

        var slot = await _slots.GetByIdAsync(appt.ScheduleSlotId) ?? throw new ArgumentException("slot");
        slot.IsBooked = false;
        slot.AppointmentId = null;
        await _slots.UpdateAsync(slot);

        if (appt.Payment != null && appt.Payment.Status == PaymentStatus.Succeeded)
        {
            await _paymentService.RefundAsync(appt.Payment);
        }

        await _slots.SaveChangesAsync();
        await _appointments.SaveChangesAsync();
    }
}
