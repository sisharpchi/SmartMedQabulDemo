namespace Domain.Enums;

public enum AppointmentStatus
{
    Pending,     // Yangi yaratilgan, hali tasdiqlanmagan
    Confirmed,   // Doktor tomonidan tasdiqlangan
    Cancelled,   // Bekor qilingan
    Completed    // Uchrashuv o‘tgan va yakunlangan
}