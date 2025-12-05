using ForceGet.API.Extensions;
using ForceGet.Application.Interfaces;
using ForceGet.Infrastructure.DTOs;
using ForceGet.Infrastructure.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForceGet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuoteController : ControllerBase
{
    private readonly IQuoteService _quoteService;
    private readonly ICountryService _countryService;

    public QuoteController(IQuoteService quoteService, ICountryService countryService)
    {
        _quoteService = quoteService;
        _countryService = countryService;
    }

    [HttpPost]
    public async Task<ActionResult<QuoteDto>> CreateQuote(QuoteDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        model.UserId = User.GetUserId();
        model.CreatedAt = DateTime.UtcNow;
        try
        {

            var quote = await _quoteService.CreateQuoteAsync(model);

            //Normalde Automapper, custom mapping, reflection vs ile yapıyoruz
            //direkt olarak OK dönmüyoruz., error handling ve response middleware eklenmeli
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
                FromCurrency = quote.FromCurrency,
                OriginalAmount = quote.OriginalAmount,
                ToCurrency = quote.ToCurrency,
                ConvertedAmount = quote.ConvertedAmount,
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
        try
        {
            var quotes = await _quoteService.GetQuotesByUserIdAsync(User.GetUserId());

            return Ok(quotes);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
