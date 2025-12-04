using ForceGet.Application.DTOs;

namespace ForceGet.Application.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetCountriesBySearchAsync(string countrySearch);
}
