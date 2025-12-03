using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.Data;
using ForceGet.Infrastructure.Interceptors;
using ForceGet.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;

namespace ForceGet.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>((serviceProvider , options) =>
        {
            var interceptor = serviceProvider.GetRequiredService<LoggingSaveChangesInterceptor>();
            options.UseNpgsql(connectionString);
            options.AddInterceptors(interceptor);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
    }
}
