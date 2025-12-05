using ForceGet.Application.DTOs;
using ForceGet.Application.Interfaces;
using ForceGet.Domain.Entities;
using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.DTOs;
using ForceGet.Infrastructure.Interfaces;

using System.Text.Json;

namespace ForceGet.Application.Services;

public class CurrencyConversionService : ICurrencyConversionService
{
    private readonly IApiNinjasService _apiNinjasService;
    private readonly ICurrencyConversionRepository _currencyConversionRepository;

    public CurrencyConversionService(IApiNinjasService apiNinjasService, ICurrencyConversionRepository currencyConversionRepository)
    {
        _apiNinjasService = apiNinjasService;
        _currencyConversionRepository = currencyConversionRepository;
    }

    public async Task<decimal> ConvertCurrencyAsync(CurrencyConversionDto currencyConversion)
    {
        try
        {
            //majör pariteler ücretli. ücretsiz olanlar için api nin döndürdüğü json a göre modelleme yapıldı.
            var content = await _apiNinjasService.ResponseFromRequest($"convertcurrency?have={currencyConversion.FromCurrency}&want={currencyConversion.ToCurrency}&amount={currencyConversion.OriginalAmount}");
            //var content = """
            //    {"new_amount": 1663.41, "new_currency": "CNY", "old_currency": "TRY", "old_amount": 10000.0}
            //    """;
            var currencyDto = JsonSerializer.Deserialize<CurrencyExchangeDto>(content);

            if (currencyDto != null)
            {
                currencyConversion.ConvertedAmount = currencyDto.NewAmount;

                //normalde burda automapper ile ya da custom bir mapping helper yazılıp dto dan entity e mapleyip repo ile db ye kaydetmek lazim
                var user = new CurrencyConversion
                {
                    ConvertedAmount = currencyDto.NewAmount,
                    FromCurrency = currencyConversion.FromCurrency,
                    ToCurrency = currencyConversion.ToCurrency,
                    OriginalAmount = currencyConversion.OriginalAmount,
                    ConvertedAt = DateTime.UtcNow,
                    UserId = currencyConversion.UserId
                };

                await _currencyConversionRepository.AddAsync(user);

                return currencyDto.NewAmount;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Currency conversion error: {ex.Message}");
        }

        return currencyConversion.OriginalAmount;
    }
}