using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TrisvagoHotels.DataContext.Context;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
    {
        return configuration["Entorno"] != "Testing"
            ? services
                .AddDbContextPool<MyDataContext>(options =>
                    options.UseMySQL(connectionString: configuration.GetConnectionString("DataAccessMySqlProvider"),
                        options => { }))
            : services
                .AddDbContext<MyDataContext>(options =>
                    options.UseSqlite(connectionString: configuration.GetConnectionString("DefaultConnection")));
    }

    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration) =>
        services.AddHealthChecks()
            .AddMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"], name: "DataAccessMySqlProvider",
                failureStatus: HealthStatus.Degraded, tags: new[] { "DataAccessMySqlProviderDependencies" })
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDbContextCheck<MyDataContext>()
            .Services;
}