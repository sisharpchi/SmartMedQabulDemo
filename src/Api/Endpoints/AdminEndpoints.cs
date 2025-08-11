using Application.Abstractions.Services;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/api/admin")
                   .WithTags("AdminManagement");

        userGroup.MapPost("/add-Hospital",
        async (HospitalCreateDto hospital,HttpContext httpContext, IHospitalService hospitalService) =>
        {
            return Results.Ok(await hospitalService.AddHospitalAsync(hospital));
        })
        .WithName("AddHospital");

        userGroup.MapGet("/get-un-confirmed-doctors",
        async (HttpContext httpContext, IDoctorService doctorService) =>
        {
            return Results.Ok(await doctorService.GetAllUnConfirmedDoctorsAsync());
        })
        .WithName("GetUnConfirmedDoctors");

        userGroup.MapDelete("/delete{userId}", [Authorize(Roles = "Admin, SuperAdmin")]
        async (long userId, HttpContext httpContext, IUserService userService) =>
        {
            var role = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            await userService.DeleteUserByIdAsync(userId, role);
            return Results.Ok();
        })
        .WithName("DeleteUser");

        userGroup.MapPatch("/update-role", [Authorize(Roles = "SuperAdmin")]
        async (long userId, string userRole, IUserService userService) =>
        {
            await userService.UpdateUserRoleAsync(userId, userRole);
            return Results.Ok();
        })
        .WithName("UpdateUserRole");
    }
}
