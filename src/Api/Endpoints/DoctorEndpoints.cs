using Application.Abstractions.Services;

namespace Api.Endpoints;

public static class DoctorEndpoints
{
    public static void MapDoctorEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/doctor")
            .RequireAuthorization()
            .WithTags("DoctorManagement");

        userGroup.MapPost("/get-all",
        async (IDoctorService doctorService) =>
        {
            return Results.Ok(await doctorService.GetAllDoctorsAsync());
        })
        .WithName("GetAllDoctors");

        userGroup.MapPost("/get-by-hospital",
        async (long hospitalId,IDoctorService doctorService) =>
        {
            return Results.Ok(await doctorService.GetAllDoctorsByHospitalIdAsync(hospitalId));
        })
        .WithName("GetAllDoctorsByHospitalId");

        userGroup.MapPost("/get{doctorId}",
        async (long doctorId,IDoctorService doctorService) =>
        {
            return Results.Ok(await doctorService.GetDoctorByIdAsync(doctorId));
        })
        .WithName("GetDoctorById");

    }
}