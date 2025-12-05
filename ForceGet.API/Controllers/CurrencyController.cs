using ForceGet.API.Extensions;
using ForceGet.Infrastructure.DTOs;
using ForceGet.Infrastructure.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForceGet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyConversionService _currencyConversionService;

    public CurrencyController(ICurrencyConversionService currencyService)
    {
        _currencyConversionService = currencyService;
    }

    [HttpPost("convert")]
    public async Task<IActionResult> ConvertCurrencyAsync(CurrencyConversionDto model)
    {
        model.UserId = User.GetUserId();
        model.ConvertedAt = DateTime.UtcNow;
        var result = await _currencyConversionService.ConvertCurrencyAsync(model);
        return Ok(new { Amount = result });
    }
}
