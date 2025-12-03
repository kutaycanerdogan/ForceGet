using ForceGet.Application.Extensions;
using ForceGet.Infrastructure.Extensions;

namespace ForceGet.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, string connectionString)
    {
        services.AddInfrastructure(connectionString);
        services.AddApplication();
    }
}
