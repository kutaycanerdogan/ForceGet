using ForceGet.Domain.Entities;
using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.Data;

namespace ForceGet.Infrastructure.Repositories;

public class CurrencyConversionRepository : Repository<CurrencyConversion>, ICurrencyConversionRepository
{
    public CurrencyConversionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CurrencyConversion>> GetCurrencyConversionsByUserIdAsync(int userId)
    {
        return await FindAsync(u => u.UserId == userId);
    }
}
