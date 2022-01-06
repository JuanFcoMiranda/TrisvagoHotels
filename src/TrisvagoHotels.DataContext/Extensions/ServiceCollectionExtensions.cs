using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TrisvagoHotels.DataContext.Context;

namespace TrisvagoHotels.DataContext.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddDbContextPool<MyDataContext>(options =>
                //options.UseMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"],
                //        new MySqlServerVersion(new Version(8, 0, 22)), // use MariaDbServerVersion for MariaDB
                //                                                       //ServerVersion.AutoDetect(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"]),
                //        mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend))
                //    // Everything from this point on is optional but helps with debugging.
                //    .EnableSensitiveDataLogging()
                //    .EnableDetailedErrors());
                options.UseMySQL(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"]));

    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration) =>
        services.AddHealthChecks()
            .AddMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"], name: "DataAccessMySqlProvider",
                failureStatus: HealthStatus.Degraded, tags: new[] { "DataAccessMySqlProviderDependencies" })
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddDbContextCheck<MyDataContext>()
            .Services;
}