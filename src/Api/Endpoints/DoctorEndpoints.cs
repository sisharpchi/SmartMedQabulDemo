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

    }
}