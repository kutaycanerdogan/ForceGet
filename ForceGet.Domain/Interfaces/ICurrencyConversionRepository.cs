using ForceGet.Domain.Entities;

namespace ForceGet.Domain.Interfaces;

public interface ICurrencyConversionRepository : IRepository<CurrencyConversion>
{
    Task<IEnumerable<CurrencyConversion>> GetCurrencyConversionsByUserIdAsync(int userId);
}
