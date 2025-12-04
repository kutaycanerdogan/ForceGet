using ForceGet.Application.Interfaces;

using Microsoft.Extensions.Configuration;

namespace ForceGet.Application.Services
{
    public class ApiNinjasService : IApiNinjasService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ApiNinjasService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient("ApiNinjas");
            _apiKey = config["ApiNinjas:ApiKey"] ?? throw new InvalidOperationException("ApiNinjas:ApiKey configuration value is missing.");
        }
        public async Task<string> ResponseFromRequest(string requestUri)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get, requestUri
            );

            request.Headers.Add("X-Api-Key", _apiKey);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
