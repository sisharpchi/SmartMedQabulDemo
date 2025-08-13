using AppointmentApi.Domain.Enums;
using AppointmentApi.Infrastructure.Repositories;

namespace AppointmentApi.Application.Services;

public class PatientService
{
    private readonly IAppointmentRepository _appointments;
    private readonly IScheduleSlotRepository _slots;
    private readonly PaymentService _paymentService;

    public PatientService(IAppointmentRepository appointments, IScheduleSlotRepository slots,
        PaymentService paymentService)
    {
        _appointments = appointments;
        _slots = slots;
        _paymentService = paymentService;
    }

    public async Task<bool> CancelAsync(long patientId, long appointmentId)
    {
        var appt = await _appointments.GetWithPayment(appointmentId) ?? throw new ArgumentException("Not found");
        if (appt.PatientId != patientId) throw new UnauthorizedAccessException();
        if (appt.Status != AppointmentStatus.Pending && appt.Status != AppointmentStatus.Approved)
            throw new InvalidOperationException();
        var slot = await _slots.GetByIdAsync(appt.ScheduleSlotId) ?? throw new ArgumentException("Slot");
        bool refunded = false;
        if (DateTime.UtcNow < slot.StartTimeUtc && appt.Payment != null && appt.Payment.Status == PaymentStatus.Succeeded)
        {
            refunded = await _paymentService.RefundAsync(appt.Payment);
        }
        appt.Status = AppointmentStatus.Cancelled;
        slot.IsBooked = false;
        slot.AppointmentId = null;
        await _appointments.UpdateAsync(appt);
        await _slots.UpdateAsync(slot);
        await _slots.SaveChangesAsync();
        await _appointments.SaveChangesAsync();
        return refunded;
    }
}
