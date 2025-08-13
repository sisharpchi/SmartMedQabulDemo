using AppointmentApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentApi.Infrastructure.Repositories;

public class ScheduleSlotRepository : Repository<ScheduleSlot>, IScheduleSlotRepository
{
    public ScheduleSlotRepository(AppDbContext db) : base(db)
    {
    }

    public async Task<List<ScheduleSlot>> ListAvailableByDoctorAndDate(long doctorId, DateOnly date)
    {
        var start = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var end = date.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc);
        return await _db.ScheduleSlots
            .Where(s => s.DoctorId == doctorId && !s.IsBooked && s.StartTimeUtc >= start && s.StartTimeUtc < end)
            .OrderBy(s => s.StartTimeUtc)
            .ToListAsync();
    }

    public async Task<bool> ExistsOverlap(long doctorId, DateTime startUtc, DateTime endUtc)
    {
        return await _db.ScheduleSlots.AnyAsync(s => s.DoctorId == doctorId &&
            ((startUtc < s.EndTimeUtc) && (endUtc > s.StartTimeUtc)));
    }
}
