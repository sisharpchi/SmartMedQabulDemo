using AppointmentApi.Domain.Entities;
using AppointmentApi.Domain.Enums;
using AppointmentApi.Infrastructure.Repositories;

namespace AppointmentApi.Application.Services;

public class BookingService
{
    private readonly IScheduleSlotRepository _slots;
    private readonly IAppointmentRepository _appointments;
    private readonly IPatientRepository _patients;
    private readonly IDoctorRepository _doctors;
    private readonly PaymentService _payments;

    public BookingService(IScheduleSlotRepository slots, IAppointmentRepository appointments,
        IPatientRepository patients, IDoctorRepository doctors, PaymentService payments)
    {
        _slots = slots;
        _appointments = appointments;
        _patients = patients;
        _doctors = doctors;
        _payments = payments;
    }

    public async Task<Appointment> BookSlot(long patientId, long slotId, PaymentMethod method)
    {
        var slot = await _slots.GetByIdAsync(slotId) ?? throw new ArgumentException("Slot not found");
        if (slot.IsBooked) throw new InvalidOperationException("Slot already booked");
        var patient = await _patients.GetByIdAsync(patientId) ?? throw new ArgumentException("Patient not found");
        var doctor = await _doctors.GetByIdAsync(slot.DoctorId) ?? throw new ArgumentException("Doctor not found");
        var duration = slot.EndTimeUtc - slot.StartTimeUtc;
        if (duration.TotalMinutes != doctor.ConsultationDurationMinutes)
            throw new InvalidOperationException("Slot duration mismatch");

        var appointment = new Appointment
        {
            DoctorId = doctor.Id,
            PatientId = patient.Id,
            ScheduleSlotId = slot.Id,
            AppointmentDateUtc = slot.StartTimeUtc,
            Status = AppointmentStatus.Pending,
            CreatedAtUtc = DateTime.UtcNow
        };
        await _appointments.AddAsync(appointment);
        await _appointments.SaveChangesAsync();

        var payment = await _payments.ChargeAsync(appointment, method);
        appointment.Payment = payment;
        if (payment.Status == PaymentStatus.Succeeded)
        {
            slot.IsBooked = true;
            slot.AppointmentId = appointment.Id;
            await _slots.UpdateAsync(slot);
            await _slots.SaveChangesAsync();
        }
        else
        {
            appointment.Status = AppointmentStatus.Cancelled;
            await _appointments.UpdateAsync(appointment);
            await _appointments.SaveChangesAsync();
        }
        return appointment;
    }
}
