using ForceGet.Domain.Entities;
using ForceGet.Domain.Enums;

namespace ForceGet.Infrastructure.Interfaces;

public interface IQuoteService
{
    Task<Quote> CreateQuoteAsync(int userId, string country, string city, ShippingMode mode, 
        MovementType movementType, Incoterms incoterms, PackageType packageType, 
        CurrencyType currency, decimal originalAmount, decimal convertedUSD);
    Task<IEnumerable<Quote>> GetQuotesByUserIdAsync(int userId);
    Task<IEnumerable<Quote>> GetAllQuotesAsync();
}
