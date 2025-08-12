using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Mappers;
using Core.Errors;

namespace Application.Services;

public class UserService(IUserRepository _userRepository) : IUserService
{
    public async Task BanUserAsync(long userId, DateTime date)
    {
        var user =await _userRepository.GetUserByIdAync(userId);
        user.BanTime = date;
        await _userRepository.UpdateUser(user);
    }

    public async Task DeleteUserByIdAsync(long userId, string userRole)
    {
        if (userRole == "SuperAdmin")
        {
            await _userRepository.DeleteUserByIdAsync(userId);
        }
        else if (userRole == "Admin")
        {
            var user = await _userRepository.GetUserByIdAync(userId);
            if (user.Role.Name == "User")
            {
                await _userRepository.DeleteUserByIdAsync(userId);
            }
            else
            {
                throw new NotAllowedException("Admin can not delete Admin or SuperAdmin");
            }
        }
    }

    public async Task<ICollection<UserGetDto>> GetUsersByRoleAsync(string roleName)
    {
        var users = await _userRepository.GetUsersByRoleAsync(roleName);
        return users.Select(MapperService.Converter).ToList();
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole) => await _userRepository.UpdateUserRoleAsync(userId, userRole);
}