using AppointmentApi.Domain.Entities;

namespace AppointmentApi.Infrastructure.Repositories;

public interface IScheduleSlotRepository : IRepository<ScheduleSlot>
{
    Task<List<ScheduleSlot>> ListAvailableByDoctorAndDate(long doctorId, DateOnly date);
    Task<bool> ExistsOverlap(long doctorId, DateTime startUtc, DateTime endUtc);
}
