using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DataAccessServicesExtension
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddDbContext<ApiDbContext>(options => options.UseSqlite(@"Data Source=mydatabase.db;"));
        services.AddScoped<IClientRepository, ClientRepository>();
    }
}