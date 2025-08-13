using AppointmentApi.Domain.Entities;
using AppointmentApi.Infrastructure.Repositories;

namespace AppointmentApi.Application.Services;

public class SchedulingService
{
    private readonly IDoctorRepository _doctors;
    private readonly IScheduleSlotRepository _slots;

    public SchedulingService(IDoctorRepository doctors, IScheduleSlotRepository slots)
    {
        _doctors = doctors;
        _slots = slots;
    }

    public async Task<List<ScheduleSlot>> GenerateDailySlots(long doctorId, DateOnly date, decimal? customPrice = null)
    {
        var doctor = await _doctors.GetByIdAsync(doctorId) ?? throw new ArgumentException("Doctor not found");
        var slots = new List<ScheduleSlot>();
        var current = date.ToDateTime(doctor.WorkStartTime, DateTimeKind.Utc);
        var end = date.ToDateTime(doctor.WorkEndTime, DateTimeKind.Utc);
        var lunchStart = date.ToDateTime(doctor.LunchStart, DateTimeKind.Utc);
        var lunchEnd = date.ToDateTime(doctor.LunchEnd, DateTimeKind.Utc);
        var duration = TimeSpan.FromMinutes(doctor.ConsultationDurationMinutes);
        while (current + duration <= end)
        {
            var next = current + duration;
            if (!(current < lunchEnd && next > lunchStart))
            {
                if (!await _slots.ExistsOverlap(doctorId, current, next))
                {
                    var slot = new ScheduleSlot
                    {
                        DoctorId = doctorId,
                        StartTimeUtc = current,
                        EndTimeUtc = next,
                        Price = customPrice ?? doctor.DefaultSlotPrice,
                        IsBooked = false
                    };
                    await _slots.AddAsync(slot);
                    slots.Add(slot);
                }
            }
            current = next;
        }
        if (slots.Any())
            await _slots.SaveChangesAsync();
        return slots;
    }
}
