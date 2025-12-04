using ForceGet.Domain.Entities;

namespace ForceGet.Infrastructure.Interfaces;

public interface IQuoteService
{
    Task<Quote> CreateQuoteAsync(Quote quote);
    Task<IEnumerable<Quote>> GetQuotesByUserIdAsync(int userId);
    Task<IEnumerable<Quote>> GetAllQuotesAsync();
}
