namespace Application.Abstractions.Services;

public interface IUserService
{
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId, string userRole);
}