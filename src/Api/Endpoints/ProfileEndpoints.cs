using Application.Abstractions.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Api.Endpoints;

public static class ProfileEndpoints
{
    public static void MapProfileEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/profile")
            .RequireAuthorization()
            .WithTags("ProfileManagement");

        userGroup.MapPut("/update-user",
        async (UserUpdateDto user,IUserService userService,HttpContext context) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            await userService.UpdateUserAsync(user,long.Parse(userId));
            return Results.Ok();
        })
        .WithName("UpdateUserProfile");

        userGroup.MapPut("/update-patient",
        async (PatientUpdateDto patient,IPatientService patientService,HttpContext context) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            await patientService.UpdatePatientAsync(patient,long.Parse(userId));
            return Results.Ok();
        })
        .WithName("UpdatePatientProfile");

        userGroup.MapPut("/update-doctor", [Authorize(Roles = "Doctor")]
        async (DoctorUpdateDto doctor,IDoctorService doctorService,HttpContext context) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            await doctorService.UpdateDoctorAsync(doctor,long.Parse(userId));
            return Results.Ok();
        })
        .WithName("UpdateDoctorProfile");
    }
}