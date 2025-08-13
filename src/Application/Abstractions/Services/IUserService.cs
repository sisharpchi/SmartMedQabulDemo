using Application.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services;

public interface IUserService
{
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task UpdateUserAsync(UserUpdateDto user, long userId);
    Task DeleteUserByIdAsync(long userId, string userRole);
    Task<ICollection<UserGetDto>> GetUsersByRoleAsync(string roleName);
    Task BanUserAsync(long userId, DateTime date);
    Task UpdateUserProfileUrlAsync(IFormFile img,long userId);
}