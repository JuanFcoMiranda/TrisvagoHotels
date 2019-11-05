using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using TrisvagoHotels.Api.HttpErrors;
using TrisvagoHotels.DataContext;
using TrisvagoHotels.DataContracts.IRepositoryFactory;
using TrisvagoHotels.DataContracts.IRepositoryProvider;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Providers.RepositoryFactory;
using TrisvagoHotels.Providers.RepositoryProvider;
using TrisvagoHotels.Uow;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddCustomServices(this IServiceCollection services) {
			services
				.AddScoped<MyDataContext, MyDataContext>()
				.AddScoped<IUow, Uow>()
				.AddScoped<IRepositoryProvider, RepositoryProvider>()
				.AddScoped<IRepositoryFactory, RepositoryFactory>()
				.AddSingleton<IHttpErrorFactory, DefaultHttpErrorFactory>();
			return services;
		}

		public static IServiceCollection AddOpenApi(this IServiceCollection services) {
			services.AddSwaggerGen(setup => {
				setup.DescribeAllParametersInCamelCase();
				//setup.DescribeStringEnumsInCamelCase();
				setup.SwaggerDoc("v1", new OpenApiInfo {
					Title = $"{nameof(TrisvagoHotels)} Api",
					Version = "v1",
					Description = "API",
					TermsOfService = null
				});
			});
			return services;
		}

		public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration) =>
			services
				.AddDbContext<MyDataContext>(options => {
					options.UseSqlServer(configuration.GetConnectionString("m4e"));
				});

		public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration) {
			return services.AddHealthChecks()
				.AddDiskStorageHealthCheck(options => { options.AddDrive(@"C:\", 100); }, name: "My Drive", HealthStatus.Unhealthy, tags: new[] { "Drive space" })
				.AddSqlServer(configuration.GetConnectionString("m4e"), healthQuery: "SELECT 1;", "M4E", failureStatus: HealthStatus.Degraded, tags: new[] { "M4Edependencies" })
				.AddCheck("self", () => HealthCheckResult.Healthy())
				.Services;
		}
	}
}