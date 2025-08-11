using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;

namespace Application.Services;

public class DoctorService(IDoctorRepository _doctorRepo) : IDoctorService
{
    public async Task<ICollection<UnConfirmedDoctorGetDto>> GetAllUnConfirmedDoctors()
    {
        var doctors  = await _doctorRepo.GetAllUnConfirmedDoctorsAsync();
        return doctors.Select(Converter).ToList();
    }

    private UnConfirmedDoctorGetDto Converter(Doctor doctor)
    {
        return new UnConfirmedDoctorGetDto
        {
            Id = doctor.Id,
            IsConfirmedByAdmin = doctor.IsConfirmedByAdmin,
            User = Converter(doctor.User)
        };
    }

    private UserGetDto Converter(User user)
    {
        return new UserGetDto
        {
            Email = user.Confirmer!.Gmail,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserId = user.UserId,
            UserName = user.UserName,
            Role = user.Role.Name,
        };
    }

}
