using AppointmentApi.Domain.Enums;

namespace AppointmentApi.Domain.Entities;

public class Payment
{
    public long Id { get; set; }
    public long AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public PaymentMethod Method { get; set; }
    public string? ExternalRef { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
