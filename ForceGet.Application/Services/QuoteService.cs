using ForceGet.Application.Interfaces;
using ForceGet.Domain.Entities;
using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.DTOs;
using ForceGet.Infrastructure.Interfaces;

namespace ForceGet.Application.Services;

public class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly ICountryService _countryService;

    public QuoteService(IQuoteRepository quoteRepository, ICountryService countryService)
    {
        _quoteRepository = quoteRepository;
        _countryService = countryService;
    }

    public async Task<QuoteDto> CreateQuoteAsync(QuoteDto q)
    {
        var quote = new Quote
        {
            Id = q.Id,
            UserId = q.UserId,
            Country = q.Country,
            City = q.City,
            Mode = q.Mode,
            MovementType = q.MovementType,
            Incoterms = q.Incoterms,
            PackageType = q.PackageType,
            FromCurrency = q.FromCurrency,
            OriginalAmount = q.OriginalAmount,
            ToCurrency = q.ToCurrency,
            ConvertedAmount = q.ConvertedAmount,
            CreatedAt = q.CreatedAt
        };
        quote = await _quoteRepository.AddAsync(quote);

        return new QuoteDto
        {
            Id = quote.Id,
            UserId = quote.UserId,
            Country = quote.Country,
            City = quote.City,
            Mode = quote.Mode,
            MovementType = quote.MovementType,
            Incoterms = quote.Incoterms,
            PackageType = quote.PackageType,
            FromCurrency = quote.FromCurrency,
            OriginalAmount = quote.OriginalAmount,
            ToCurrency = quote.ToCurrency,
            ConvertedAmount = quote.ConvertedAmount,
            CreatedAt = quote.CreatedAt
        };
    }

    public async Task<IEnumerable<QuoteDto>> GetQuotesByUserIdAsync(int userId)
    {
        var quotes = await _quoteRepository.GetQuotesByUserIdAsync(userId);
        var quoteDtos = quotes.Select(q => new QuoteDto
        {
            Id = q.Id,
            UserId = q.UserId,
            Country = q.Country,
            City = q.City,
            Mode = q.Mode,
            MovementType = q.MovementType,
            Incoterms = q.Incoterms,
            PackageType = q.PackageType,
            FromCurrency = q.FromCurrency,
            OriginalAmount = q.OriginalAmount,
            ToCurrency = q.ToCurrency,
            ConvertedAmount = q.ConvertedAmount,
            CreatedAt = q.CreatedAt
        });
        return quoteDtos;
    }

    public async Task<IEnumerable<QuoteDto>> GetAllQuotesAsync()
    {
        var quotes = await _quoteRepository.GetAllAsync();
        var quoteDtos = quotes.Select(q => new QuoteDto
        {
            Id = q.Id,
            UserId = q.UserId,
            Country = q.Country,
            City = q.City,
            Mode = q.Mode,
            MovementType = q.MovementType,
            Incoterms = q.Incoterms,
            PackageType = q.PackageType,
            FromCurrency = q.FromCurrency,
            OriginalAmount = q.OriginalAmount,
            ToCurrency = q.ToCurrency,
            ConvertedAmount = q.ConvertedAmount,
            CreatedAt = q.CreatedAt
        });
        return quoteDtos;
    }
}
