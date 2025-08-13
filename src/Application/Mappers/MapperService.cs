using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers;

public static class MapperService
{
    public static UnConfirmedDoctorGetDto Converter(Doctor doctor)
    {
        return new UnConfirmedDoctorGetDto
        {
            Id = doctor.Id,
            IsConfirmedByAdmin = doctor.IsConfirmedByAdmin,
            User = Converter(doctor.User)
        };
    }

    public static UserGetDto Converter(User user)
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
            BanDate = user.BanTime,
            ProfilUrl = user.ProfileImageUrl
        };
    }
    public static PatientGetDto Converter(Patient patiend)
    {
        return new PatientGetDto
        {
            DateOfBirth = patiend.DateOfBirth,
            Gender = patiend.Gender,
            Id = patiend.Id,
            UserId = patiend.UserId,
        };
    }
    public static UserDto UserDtoConverter(User user)
    {
        return new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
            BanDate = user.BanTime,
            ProfilUrl = user.ProfileImageUrl
        };
    }
    public static DoctorGetDto GetDoctorConverter(Doctor doctor)
    {
        return new DoctorGetDto
        {
            Description = doctor.Description,
            Experience = doctor.Experience,
            Hospital = Converter(doctor.Hospital!) ,
            User = UserDtoConverter(doctor.User),
            Id = doctor.Id,
            Rating = doctor.Rating,
            Specialty = doctor.Specialty,
            WorkEndTime = doctor.WorkEndTime,
            WorkStartTime = doctor.WorkStartTime,
        };
    }

    public static HospitalGetDto Converter(Hospital hospital)
    {
        return new HospitalGetDto
        {
            Address = hospital.Address,
            Id = hospital.Id,
            Name = hospital.Name,
            Type = hospital.Type,
        };
    }
}
