using Application.Abstractions.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Api.Endpoints;

public static class PatientEndpoints
{
    public static void MapPatientEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/patient")
            .RequireAuthorization()
            .WithTags("PatientManagement");

        userGroup.MapGet("/get-all", [Authorize(Roles = "Admin,SuperAdmin")]
        async (IPatientService patientService) =>
        {
            return Results.Ok(await patientService.GetAllPatientsAsync());
        })
        .WithName("GetAllPatients");
    }
}
