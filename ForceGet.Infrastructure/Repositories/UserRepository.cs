using ForceGet.Domain.Entities;
using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.Data;

namespace ForceGet.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await FirstOrDefaultAsync(u => u.Email == email);
    }
}
