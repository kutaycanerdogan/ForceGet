using ForceGet.Domain.Entities;
using ForceGet.Domain.Enums;

namespace ForceGet.Infrastructure.Interfaces;

public interface IUserService
{
    Task<User> RegisterAsync(string email, string password);
    Task<string> LoginAsync(string email, string password);
    Task<User> GetUserByEmailAsync(string email);
}
