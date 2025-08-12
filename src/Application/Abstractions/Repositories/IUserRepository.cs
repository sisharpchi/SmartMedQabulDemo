using Domain.Entities;

namespace Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<long> AddUserAync(User user);
    Task AddConfirmerAsync(UserConfirmer confirmer);
    Task<User> GetUserByIdAync(long id);
    Task<ICollection<User>> GetUsersByRoleAsync(string roleName);
    Task UpdateUserAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUserNameAync(string userName);
    Task UpdateUserRoleAsync(long userId, string userRole);
    Task DeleteUserByIdAsync(long userId);
    Task<bool> CheckUserByIdAsync(long userId);
    Task<bool> CheckUsernameExistsAsync(string username);
    Task<long?> CheckEmailExistsAsync(string email);
    Task<bool> CheckPhoneNumberExistsAsync(string phoneNum);
}
