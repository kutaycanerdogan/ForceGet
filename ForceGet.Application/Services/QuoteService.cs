using ForceGet.Application.Interfaces;
using ForceGet.Domain.Entities;
using ForceGet.Domain.Interfaces;
using ForceGet.Domain.Enums;
using ForceGet.Infrastructure.Interfaces;

namespace ForceGet.Application.Services;

public class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly ICityService _cityService;
    private readonly ICurrencyService _currencyService;

    public QuoteService(IQuoteRepository quoteRepository, ICityService cityService, ICurrencyService currencyService)
    {
        _quoteRepository = quoteRepository;
        _cityService = cityService;
        _currencyService = currencyService;
    }

    public async Task<Quote> CreateQuoteAsync(int userId, string country, string city, ShippingMode mode, 
        MovementType movementType, Incoterms incoterms, PackageType packageType, 
        CurrencyType currency, decimal originalAmount, decimal convertedUSD)
    {
        var quote = new Quote
        {
            UserId = userId,
            Country = country,
            City = city,
            Mode = mode,
            MovementType = movementType,
            Incoterms = incoterms,
            PackageType = packageType,
            Currency = currency,
            OriginalAmount = originalAmount,
            ConvertedUSD = convertedUSD,
            CreatedAt = DateTime.UtcNow
        };

        await _quoteRepository.AddAsync(quote);
        return quote;
    }

    public async Task<IEnumerable<Quote>> GetQuotesByUserIdAsync(int userId)
    {
        return await _quoteRepository.GetQuotesByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Quote>> GetAllQuotesAsync()
    {
        return await _quoteRepository.GetAllAsync();
    }
}
