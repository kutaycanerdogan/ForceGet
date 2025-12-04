using ForceGet.Application.Interfaces;
using ForceGet.Domain.Entities;
using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.Interfaces;

namespace ForceGet.Application.Services;

public class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly ICountryService _countryService;
    private readonly ICurrencyService _currencyService;

    public QuoteService(IQuoteRepository quoteRepository, ICountryService countryService, ICurrencyService currencyService)
    {
        _quoteRepository = quoteRepository;
        _countryService = countryService;
        _currencyService = currencyService;
    }

    public async Task<Quote> CreateQuoteAsync(Quote quote)
    {
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
