using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TrisvagoHotels.DataContext.Context;

namespace Microsoft.Extensions.DependencyInjection {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContextPool<MyDataContext>(options =>
                    options.UseMySQL(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"]));

        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration) =>
            services.AddHealthChecks()
                .AddMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"], name: "DataAccessMySqlProvider",
                    failureStatus: HealthStatus.Degraded, tags: new[] { "DataAccessMySqlProviderDependencies" })
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<MyDataContext>()
                .Services;
    }
}