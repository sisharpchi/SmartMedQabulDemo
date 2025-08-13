using AppointmentApi.Domain.Enums;

namespace AppointmentApi.Application.DTOs;

public record DoctorCreateRequest(string FirstName, string LastName, string Email, string Specialty, decimal DefaultSlotPrice);
public record DoctorWorkHoursRequest(TimeOnly WorkStartTime, TimeOnly WorkEndTime);
public record DoctorLunchRequest(TimeOnly LunchStart, TimeOnly LunchEnd);
public record DoctorConsultationRequest(int ConsultationDurationMinutes, decimal DefaultSlotPrice);
public record DoctorResponse(long Id, string FirstName, string LastName, string Specialty, decimal DefaultSlotPrice,
    TimeOnly WorkStartTime, TimeOnly WorkEndTime, TimeOnly LunchStart, TimeOnly LunchEnd, int ConsultationDurationMinutes);

public record GenerateSlotsRequest(DateOnly Date, decimal? CustomPrice);
public record SlotResponse(long Id, long DoctorId, DateTime StartTimeUtc, DateTime EndTimeUtc, decimal Price, bool IsBooked);

public record BookSlotRequest(long PatientId, long SlotId, PaymentMethod PaymentMethod);
public record AppointmentResponse(long Id, long DoctorId, long PatientId, DateTime StartUtc, AppointmentStatus Status,
    decimal Price, PaymentStatus PaymentStatus);

public record ApproveDeclineRequest(long AppointmentId);
public record CancelAppointmentRequest(long AppointmentId);
public record SetAppointmentStatusRequest(AppointmentStatus Status);
public record RefundPaymentRequest(long PaymentId);
