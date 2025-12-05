using ForceGet.Application.Interfaces;
using ForceGet.Infrastructure.DTOs;

using Microsoft.Extensions.Caching.Distributed;

using System.Text.Json;

namespace ForceGet.Application.Services;

public class CountryService : ICountryService
{
    private readonly IDistributedCache _cache;
    private readonly IApiNinjasService _apiNinjasService;

    public CountryService(IDistributedCache cache, IApiNinjasService apiNinjasService)
    {
        _cache = cache;
        _apiNinjasService = apiNinjasService;
    }

    public async Task<IEnumerable<CountryDto>> GetCountriesBySearchAsync(string countrySearch)
    {
        var cacheKey = $"countries_{countrySearch}";
        //_cache.Remove(cacheKey);

        var cachedCountries = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedCountries))
        {
            var countries = JsonSerializer.Deserialize<List<CountryDto>>(cachedCountries);
            return countries ?? Enumerable.Empty<CountryDto>();
        }

        try
        {

            ////api ninjac country arama yalınca 1 sonuç veriyor, limit eklense bile yine 1 sonuç dönüyor. autocomplete için uygun değil
            //var content = await _apiNinjasService.ResponseFromRequest($"country?name={countrySearch}&limit=30");

            ////apininjas örnek veri
            ////var content = """
            ////    [{"gdp": 39895.0, "sex_ratio": 98.4, "surface_area": 163610.0, "life_expectancy_male": 74.4, "unemployment": 16.2, "imports": 22453.0, "homicide_rate": 3.1, "currency": {"code": "TND", "name": "Tunisian Dinar"}, "iso2": "TN", "employment_services": 54.8, "employment_industry": 32.5, "urban_population_growth": 1.6, "secondary_school_enrollment_female": 99.3, "employment_agriculture": 12.7, "capital": "Tunis", "co2_emissions": 26.2, "forested_area": 6.7, "tourists": 8299.0, "exports": 15489.0, "life_expectancy_female": 78.5, "post_secondary_enrollment_female": 41.2, "post_secondary_enrollment_male": 22.8, "primary_school_enrollment_female": 114.9, "infant_mortality": 12.7, "gdp_growth": 2.5, "threatened_species": 107.0, "population": 11819.0, "urban_population": 69.3, "secondary_school_enrollment_male": 86.9, "name": "Tunisia", "pop_growth": 1.1, "region": "Northern Africa", "pop_density": 76.1, "internet_users": 64.2, "gdp_per_capita": 3449.6, "fertility": 2.2, "refugees": 2.0, "primary_school_enrollment_male": 115.9, "telephone_country_codes": ["216"]}]
            ////    """;
            ///


            //eldeki countries json datası ile arama yapılıyor
            JsonDocument doc;
            using (StreamReader r = new StreamReader("JsonData/countries.json"))
            {
                string json = r.ReadToEnd();
                doc = JsonDocument.Parse(json);
            }

            var countries = doc.RootElement
                .EnumerateArray()
                .Where(x => x.GetProperty("name").GetString()!.Contains(countrySearch, StringComparison.OrdinalIgnoreCase))
                .Select(x => new CountryDto
                {
                    Name = x.GetProperty("name").GetString(),
                    CurrencyCode = x.GetProperty("currency").GetString()
                    //CurrencyCode = x.GetProperty("currency").GetProperty("code").GetString() //api ninjas için
                })
                .ToList();

            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365) // ülkelerin isimleri ya da para birimleri sık değişmediği için uzun süreli cache
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(countries), cacheOptions);

            return countries;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Country service error: {ex.Message}");
        }

        return Enumerable.Empty<CountryDto>();
    }
}