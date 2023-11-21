using DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class ServiceExtension
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddDataServices();
        services.AddScoped<IClientService, ClientService>();
    }
}