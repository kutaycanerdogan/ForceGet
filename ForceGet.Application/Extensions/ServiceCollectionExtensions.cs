using ForceGet.Application.Interfaces;
using ForceGet.Application.Services;
using ForceGet.Infrastructure.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace ForceGet.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IQuoteService, QuoteService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
    }
}
