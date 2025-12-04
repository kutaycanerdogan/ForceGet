using ForceGet.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ForceGet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet("country/{country}")]
    public async Task<IActionResult> GetCitiesByCountry(string country)
    {
        var cities = await _countryService.GetCountriesBySearchAsync(country);
        return Ok(cities);
    }
}
