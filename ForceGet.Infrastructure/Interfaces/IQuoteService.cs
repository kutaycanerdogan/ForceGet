using ForceGet.Infrastructure.DTOs;

namespace ForceGet.Infrastructure.Interfaces;

public interface IQuoteService
{
    Task<QuoteDto> CreateQuoteAsync(QuoteDto quote);
    Task<IEnumerable<QuoteDto>> GetQuotesByUserIdAsync(int userId);
    Task<IEnumerable<QuoteDto>> GetAllQuotesAsync();
}
