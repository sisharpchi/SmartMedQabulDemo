using AppointmentApi.Application.DTOs;
using AppointmentApi.Application.Services;
using AppointmentApi.Domain.Entities;
using AppointmentApi.Domain.Enums;
using AppointmentApi.Infrastructure;
using AppointmentApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("app"));

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IScheduleSlotRepository, ScheduleSlotRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddScoped<IPaymentProvider, FakePaymentProvider>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<SchedulingService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<DoctorDecisionService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<AdminService>();

var app = builder.Build();

// Doctor endpoints
app.MapPost("/api/doctors", async ([FromBody] DoctorCreateRequest req, IDoctorRepository repo) =>
{
    var doctor = new Doctor
    {
        User = new User { FirstName = req.FirstName, LastName = req.LastName, Email = req.Email, Role = UserRole.Doctor },
        Specialty = req.Specialty,
        DefaultSlotPrice = req.DefaultSlotPrice
    };
    await repo.AddAsync(doctor);
    await repo.SaveChangesAsync();
    return Results.Ok(new { data = doctor });
});

app.MapPut("/api/doctors/{id}/work-hours", async (long id, DoctorWorkHoursRequest req, IDoctorRepository repo) =>
{
    var doctor = await repo.GetByIdAsync(id); if (doctor == null) return Results.NotFound();
    doctor.WorkStartTime = req.WorkStartTime;
    doctor.WorkEndTime = req.WorkEndTime;
    await repo.UpdateAsync(doctor);
    await repo.SaveChangesAsync();
    return Results.Ok(new { data = doctor });
});

app.MapPut("/api/doctors/{id}/lunch", async (long id, DoctorLunchRequest req, IDoctorRepository repo) =>
{
    var doctor = await repo.GetByIdAsync(id); if (doctor == null) return Results.NotFound();
    doctor.LunchStart = req.LunchStart;
    doctor.LunchEnd = req.LunchEnd;
    await repo.UpdateAsync(doctor);
    await repo.SaveChangesAsync();
    return Results.Ok(new { data = doctor });
});

app.MapPut("/api/doctors/{id}/consultation", async (long id, DoctorConsultationRequest req, IDoctorRepository repo) =>
{
    var doctor = await repo.GetByIdAsync(id); if (doctor == null) return Results.NotFound();
    doctor.ConsultationDurationMinutes = req.ConsultationDurationMinutes;
    doctor.DefaultSlotPrice = req.DefaultSlotPrice;
    await repo.UpdateAsync(doctor);
    await repo.SaveChangesAsync();
    return Results.Ok(new { data = doctor });
});

app.MapGet("/api/doctors", async (IDoctorRepository repo) =>
{
    var docs = await repo.ListAsync();
    return Results.Ok(new { data = docs });
});

app.MapGet("/api/doctors/{id}", async (long id, IDoctorRepository repo) =>
{
    var doc = await repo.GetByIdAsync(id);
    return doc == null ? Results.NotFound() : Results.Ok(new { data = doc });
});

// Scheduling
app.MapPost("/api/scheduling/{doctorId}/generate-slots", async (long doctorId, [FromBody] GenerateSlotsRequest req, SchedulingService svc) =>
{
    var slots = await svc.GenerateDailySlots(doctorId, req.Date, req.CustomPrice);
    return Results.Ok(new { data = slots });
});

app.MapGet("/api/slots/doctor/{doctorId}", async (long doctorId, DateOnly date, IScheduleSlotRepository repo) =>
{
    var slots = await repo.ListAvailableByDoctorAndDate(doctorId, date);
    return Results.Ok(new { data = slots });
});

// Booking / Appointments
app.MapPost("/api/appointments/book", async (BookSlotRequest req, BookingService svc) =>
{
    try
    {
        var appt = await svc.BookSlot(req.PatientId, req.SlotId, req.PaymentMethod);
        return Results.Ok(new { data = appt });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapPost("/api/appointments/{id}/approve", async (long id, DoctorDecisionService svc, long doctorId) =>
{
    try
    {
        await svc.ApproveAsync(doctorId, id);
        return Results.Ok(new { data = true });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapPost("/api/appointments/{id}/decline", async (long id, DoctorDecisionService svc, long doctorId) =>
{
    try
    {
        await svc.DeclineAsync(doctorId, id);
        return Results.Ok(new { data = true });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapPost("/api/appointments/{id}/cancel", async (long id, long patientId, PatientService svc) =>
{
    try
    {
        var refunded = await svc.CancelAsync(patientId, id);
        return Results.Ok(new { data = refunded });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapGet("/api/appointments/doctor/{doctorId}", async (long doctorId, IAppointmentRepository repo) =>
{
    var list = await repo.ListByDoctor(doctorId);
    return Results.Ok(new { data = list });
});

app.MapGet("/api/appointments/patient/{patientId}", async (long patientId, IAppointmentRepository repo) =>
{
    var list = await repo.ListByPatient(patientId);
    return Results.Ok(new { data = list });
});

// Payments
app.MapGet("/api/payments/appointment/{appointmentId}", async (long appointmentId, IPaymentRepository repo) =>
{
    var payment = await repo.GetByAppointmentId(appointmentId);
    return payment == null ? Results.NotFound() : Results.Ok(new { data = payment });
});

app.MapPost("/api/payments/{paymentId}/refund", async (long paymentId, PaymentService svc, IPaymentRepository repo) =>
{
    var payment = await repo.GetByIdAsync(paymentId);
    if (payment == null) return Results.NotFound();
    var ok = await svc.RefundAsync(payment);
    return Results.Ok(new { data = ok });
});

// Admin
app.MapGet("/api/admin/doctors", async (AdminService svc) => Results.Ok(new { data = await svc.ListDoctors() }));
app.MapGet("/api/admin/patients", async (AdminService svc) => Results.Ok(new { data = await svc.ListPatients() }));
app.MapGet("/api/admin/appointments", async (AdminService svc) => Results.Ok(new { data = await svc.ListAppointments() }));
app.MapGet("/api/admin/payments", async (AdminService svc) => Results.Ok(new { data = await svc.ListPayments() }));

app.MapPost("/api/admin/appointments/{id}/status", async (long id, SetAppointmentStatusRequest req, AdminService svc) =>
{
    await svc.SetAppointmentStatus(id, req.Status);
    return Results.Ok(new { data = true });
});

app.MapPost("/api/admin/payments/{id}/refund", async (long id, AdminService svc) =>
{
    var ok = await svc.RefundPayment(id);
    return Results.Ok(new { data = ok });
});

app.Run();
