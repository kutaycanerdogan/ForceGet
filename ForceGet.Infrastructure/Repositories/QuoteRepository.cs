using ForceGet.Domain.Entities;
using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.Data;

namespace ForceGet.Infrastructure.Repositories;

public class QuoteRepository : Repository<Quote>, IQuoteRepository
{
    public QuoteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Quote>> GetQuotesByUserIdAsync(int userId)
    {
        return await FindAsync(q => q.UserId == userId);
    }
}
