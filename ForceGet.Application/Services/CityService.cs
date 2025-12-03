using ForceGet.Application.Interfaces;
using System.Text.Json;

namespace ForceGet.Application.Services;

public class CityService : ICityService
{
    private readonly HttpClient _httpClient;

    public CityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<string>> GetCitiesByCountryAsync(string country)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://api.api-ninjas.com/v1/cities?country={country}");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var cities = JsonSerializer.Deserialize<List<CityResponse>>(content);
            
            return cities?.Select(c => c.Name) ?? Enumerable.Empty<string>();
        }
        catch
        {
            return Enumerable.Empty<string>();
        }
    }

    private class CityResponse
    {
        public string Name { get; set; } = string.Empty;
    }
}
