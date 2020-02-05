using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TrisvagoHotels.DataContext.Context;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration) =>
			services
				.AddDbContext<MyDataContext>(options =>
					options.UseMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"])),
				                ServiceLifetime.Transient);
		
		public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration) {
			return services.AddHealthChecks()
				.AddMySql(connectionString: configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"], name: "DataAccessMySqlProvider", failureStatus: HealthStatus.Degraded, tags: new[] { "DataAccessMySqlProviderDependencies" })
				.AddCheck("self", () => HealthCheckResult.Healthy())
				.Services;
		}
	}
} 
