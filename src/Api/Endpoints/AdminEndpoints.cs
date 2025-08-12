using Application.Abstractions.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Endpoints;

public static class AdminEndpoints
{
    private record BanUser(long userId,DateTime date);
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/admin")
                   .RequireAuthorization()
                   .WithTags("AdminManagement");


        userGroup.MapPost("/add-hospital", [Authorize(Roles = "Admin, SuperAdmin")]
        async (HospitalCreateDto hospital,IHospitalService hospitalService) =>
        {
            return Results.Ok(await hospitalService.AddHospitalAsync(hospital));
        })
        .WithName("AddHospital");

        userGroup.MapPatch("/ban-user", [Authorize(Roles = "Admin, SuperAdmin")]
        async (BanUser informations,IUserService userService) =>
        {
            await userService.BanUserAsync(informations.userId,informations.date);
            return Results.Ok();
        })
        .WithName("BanUser");

        userGroup.MapPatch("/confirm-doctor", [Authorize(Roles = "Admin, SuperAdmin")]
        async (long doctorId,long hospitalId,IDoctorService doctorService) =>
        {
            await doctorService.ConfirmDoctorAsync(doctorId,hospitalId);
            return Results.Ok();
        })
        .WithName("ConfirmDoctor");

        userGroup.MapGet("/get-un-confirmed-doctors", [Authorize(Roles = "Admin, SuperAdmin")]
        async (HttpContext httpContext, IDoctorService doctorService) =>
        {
            return Results.Ok(await doctorService.GetAllUnConfirmedDoctorsAsync());
        })
        .WithName("GetUnConfirmedDoctors");

        userGroup.MapGet("/get-users{roleName}", [Authorize(Roles = "Admin, SuperAdmin")]
        async (string roleName, IUserService userService) =>
        {
            return Results.Ok(await userService.GetUsersByRoleAsync(roleName));
        })
        .WithName("GetUsersByRole");

        userGroup.MapPatch("/update-role", [Authorize(Roles = "Admin,SuperAdmin")]
        async (long userId, string userRole, IUserService userService) =>
        {
            await userService.UpdateUserRoleAsync(userId, userRole);
            return Results.Ok();
        })
        .WithName("UpdateUserRole");
    }
}
