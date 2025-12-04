using ForceGet.Application.DTOs;
using ForceGet.Application.Interfaces;
using ForceGet.Domain.Entities;
using ForceGet.Infrastructure.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForceGet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuotesController : ControllerBase
{
    private readonly IQuoteService _quoteService;
    private readonly ICountryService _countryService;
    private readonly ICurrencyService _currencyService;

    public QuotesController(IQuoteService quoteService, ICountryService countryService, ICurrencyService currencyService)
    {
        _quoteService = quoteService;
        _countryService = countryService;
        _currencyService = currencyService;
    }

    [HttpPost]
    public async Task<ActionResult<QuoteDto>> CreateQuote(QuoteDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Kullanıcı ID'sini JWT token'dan al
        model.UserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
        model.CreatedAt = DateTime.UtcNow;
        try
        {
            // Döviz çevirisi
            var convertedUSD = await _currencyService.ConvertToUsdAsync(model.Currency.ToString(), model.OriginalAmount);

            //Normalde Automapper ile yapıyoruz
            var quote = new Quote
            {
                UserId = model.UserId,
                Country = model.Country,
                City = model.City,
                Mode = model.Mode,
                MovementType = model.MovementType,
                Incoterms = model.Incoterms,
                PackageType = model.PackageType,
                Currency = model.Currency,
                OriginalAmount = model.OriginalAmount,
                ConvertedUSD = model.ConvertedUSD,
                CreatedAt = model.CreatedAt
            };

            quote = await _quoteService.CreateQuoteAsync(quote);

            //Normalde Automapper ile yapıyoruz
            return Ok(new QuoteDto
            {
                Id = quote.Id,
                UserId = quote.UserId,
                Country = quote.Country,
                City = quote.City,
                Mode = quote.Mode,
                MovementType = quote.MovementType,
                Incoterms = quote.Incoterms,
                PackageType = quote.PackageType,
                Currency = quote.Currency,
                OriginalAmount = quote.OriginalAmount,
                ConvertedUSD = quote.ConvertedUSD,
                CreatedAt = quote.CreatedAt
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuoteDto>>> GetQuotes()
    {
        // Kullanıcı ID'sini JWT token'dan al
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

        try
        {
            var quotes = await _quoteService.GetQuotesByUserIdAsync(userId);

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
                Currency = q.Currency,
                OriginalAmount = q.OriginalAmount,
                ConvertedUSD = q.ConvertedUSD,
                CreatedAt = q.CreatedAt
            });

            return Ok(quoteDtos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
