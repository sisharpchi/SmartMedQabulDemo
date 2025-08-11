using Application.Abstractions.Services;
using Application.Dtos;

namespace Api.Endpoints;

public static class HospitalEndpoints
{
    public static void MapHospitalEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/hospital")
                   .RequireAuthorization()
                   .WithTags("HospitalManagement");

        userGroup.MapPost("/get-all",
        async (IHospitalService hospitalService) =>
        {
            return Results.Ok(await hospitalService.GetAllHospitalsAsync());
        })
        .WithName("GetAllHospital");

    }
}
