using ForceGet.Infrastructure.DTOs;

namespace ForceGet.Infrastructure.Interfaces;

public interface ICurrencyConversionService
{
    Task<decimal> ConvertCurrencyAsync(CurrencyConversionDto currencyConversion);

}
