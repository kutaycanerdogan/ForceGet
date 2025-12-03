using System.Text.Json.Nodes;

namespace ForceGet.Application.Interfaces;

public interface ICityService
{
    Task<IEnumerable<string>> GetCitiesByCountryAsync(string country);
}
