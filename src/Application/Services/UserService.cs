using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Mappers;
using Core.Errors;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class UserService(IUserRepository _userRepository, ICloudService _cloudService) : IUserService
{
    public async Task BanUserAsync(long userId, DateTime date)
    {
        var user = await _userRepository.GetUserByIdAync(userId);
        user.BanTime = date;
        await _userRepository.UpdateUserAsync(user);
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

    public async Task UpdateUserAsync(UserUpdateDto user, long userId)
    {
        var userEntity = await _userRepository.GetUserByIdAync(userId);
        userEntity.Address = user.Address;
        userEntity.UserName = user.UserName;
        userEntity.PhoneNumber = user.PhoneNumber;
        userEntity.Bio = user.Bio;
        userEntity.FirstName = user.FirstName;
        userEntity.LastName = user.LastName;
        await _userRepository.UpdateUserAsync(userEntity);
    }

    public async Task UpdateUserProfileUrlAsync(IFormFile img, long userId)
    {
        var user = await _userRepository.GetUserByIdAync(userId);
        user.ProfileImageUrl = await _cloudService.UploadProfileImageAsync(img);
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task UpdateUserRoleAsync(long userId, string userRole) => await _userRepository.UpdateUserRoleAsync(userId, userRole);
}