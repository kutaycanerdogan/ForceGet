using ForceGet.Application.DTOs;
using ForceGet.Application.Interfaces;
using ForceGet.Domain.Enums;
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
    private readonly ICityService _cityService;
    private readonly ICurrencyService _currencyService;

    public QuotesController(IQuoteService quoteService, ICityService cityService, ICurrencyService currencyService)
    {
        _quoteService = quoteService;
        _cityService = cityService;
        _currencyService = currencyService;
    }

    [HttpPost]
    public async Task<ActionResult<QuoteDto>> CreateQuote(QuoteRequestDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Kullanıcı ID'sini JWT token'dan al
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

        try
        {
            // Döviz çevirisi
            var convertedUSD = await _currencyService.ConvertToUsdAsync(model.Currency.ToString(), model.OriginalAmount);

            var quote = await _quoteService.CreateQuoteAsync(
                userId,
                model.Country,
                model.City,
                model.Mode,
                model.MovementType,
                model.Incoterms,
                model.PackageType,
                model.Currency,
                model.OriginalAmount,
                convertedUSD);

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

    [HttpGet("cities")]
    public async Task<ActionResult<IEnumerable<string>>> GetCities(string country)
    {
        try
        {
            var cities = await _cityService.GetCitiesByCountryAsync(country);
            return Ok(cities);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
