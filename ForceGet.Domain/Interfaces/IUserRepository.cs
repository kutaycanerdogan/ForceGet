using ForceGet.Domain.Entities;

namespace ForceGet.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmailAsync(string email);
}
