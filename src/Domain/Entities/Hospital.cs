using Domain.Enums;

namespace Domain.Entities;

public class Hospital
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public HospitalType Type { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
}