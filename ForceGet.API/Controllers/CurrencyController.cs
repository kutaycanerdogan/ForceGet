using ForceGet.Application.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForceGet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet("convert/{fromCurrency}/{amount}")]
    public async Task<IActionResult> ConvertToUsd(string fromCurrency, decimal amount)
    {
        var result = await _currencyService.ConvertToUsdAsync(fromCurrency, amount);
        return Ok(new { Amount = result });
    }
}
