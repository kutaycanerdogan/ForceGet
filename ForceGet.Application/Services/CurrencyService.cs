using ForceGet.Application.DTOs;
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
            var response = await _httpClient.GetAsync($"https://api.api-ninjas.com/v1/convertcurrency?have={fromCurrency}&want=USD&amount=${amount}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var currencyDto = JsonSerializer.Deserialize<CurrencyExchangeDto>(content);

                if (currencyDto != null)
                {
                    return currencyDto.NewAmount;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Currency conversion error: {ex.Message}");
        }

        return amount;
    }
}