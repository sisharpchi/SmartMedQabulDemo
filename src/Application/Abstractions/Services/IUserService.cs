using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IUserService
{
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId, string userRole);
    Task<ICollection<UserGetDto>> GetUsersByRoleAsync(string roleName);
    Task BanUserAsync(long userId,DateTime date);
}