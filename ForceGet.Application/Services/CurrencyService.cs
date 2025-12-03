using ForceGet.Application.Interfaces;
using System.Text.Json;

namespace ForceGet.Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly HttpClient _httpClient;

    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> ConvertToUsdAsync(string fromCurrency, decimal amount)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://api.api-ninjas.com/v1/currency?from={fromCurrency}&to=USD&amount={amount}");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var currencies = JsonSerializer.Deserialize<List<CurrencyResponse>>(content);
            
            return currencies?.FirstOrDefault()?.ConvertedAmount ?? 0;
        }
        catch
        {
            return 0;
        }
    }

    private class CurrencyResponse
    {
        public decimal ConvertedAmount { get; set; }
    }
}
