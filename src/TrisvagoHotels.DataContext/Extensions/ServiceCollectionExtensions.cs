using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TrisvagoHotels.DataContext.Context;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration) =>
			services
				.AddDbContext<MyDataContext>(options =>
					options.UseMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"], 
							// new MySqlServerVersion(new Version(8, 0, 22)), // use MariaDbServerVersion for MariaDB
							MySqlServerVersion.LatestSupportedServerVersion,
							mySqlOptions => mySqlOptions
								.CharSetBehavior(CharSetBehavior.NeverAppend))
						// Everything from this point on is optional but helps with debugging.
						.EnableSensitiveDataLogging()
						.EnableDetailedErrors(),
					ServiceLifetime.Transient);
		
		public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration) =>
			services.AddHealthChecks()
				.AddMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"], name: "DataAccessMySqlProvider", 
					failureStatus: HealthStatus.Degraded, tags: new[] { "DataAccessMySqlProviderDependencies" })
				.AddCheck("self", () => HealthCheckResult.Healthy())
				.Services;
	}
}