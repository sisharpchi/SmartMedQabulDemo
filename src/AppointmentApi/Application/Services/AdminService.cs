using AppointmentApi.Domain.Entities;
using AppointmentApi.Domain.Enums;
using AppointmentApi.Infrastructure.Repositories;

namespace AppointmentApi.Application.Services;

public class AdminService
{
    private readonly IDoctorRepository _doctors;
    private readonly IPatientRepository _patients;
    private readonly IAppointmentRepository _appointments;
    private readonly IPaymentRepository _payments;
    private readonly IScheduleSlotRepository _slots;
    private readonly PaymentService _paymentService;

    public AdminService(IDoctorRepository doctors, IPatientRepository patients,
        IAppointmentRepository appointments, IPaymentRepository payments,
        IScheduleSlotRepository slots, PaymentService paymentService)
    {
        _doctors = doctors;
        _patients = patients;
        _appointments = appointments;
        _payments = payments;
        _slots = slots;
        _paymentService = paymentService;
    }

    public Task<List<Doctor>> ListDoctors() => _doctors.ListAsync();
    public Task<List<Patient>> ListPatients() => _patients.ListAsync();
    public Task<List<Appointment>> ListAppointments() => _appointments.ListAsync();
    public Task<List<Payment>> ListPayments() => _payments.ListAsync();

    public async Task SetAppointmentStatus(long id, AppointmentStatus status)
    {
        var appt = await _appointments.GetWithPayment(id) ?? throw new ArgumentException("not found");
        appt.Status = status;
        await _appointments.UpdateAsync(appt);
        await _appointments.SaveChangesAsync();
    }

    public async Task<bool> RefundPayment(long paymentId)
    {
        var payment = await _payments.GetByIdAsync(paymentId) ?? throw new ArgumentException("not found");
        return await _paymentService.RefundAsync(payment);
    }
}
