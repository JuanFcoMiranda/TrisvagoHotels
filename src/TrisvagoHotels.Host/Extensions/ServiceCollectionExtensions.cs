using Microsoft.OpenApi.Models;
using TrisvagoHotels.Api.Filters;
using TrisvagoHotels.Api.HttpErrors;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepositoryFactory;
using TrisvagoHotels.DataContracts.IRepositoryProvider;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Providers.RepositoryFactory;
using TrisvagoHotels.Providers.RepositoryProvider;
using TrisvagoHotels.Services.Hotels;
using TrisvagoHotels.Uow;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddCustomServices(this IServiceCollection services) {
			services
				.AddScoped<MyDataContext, MyDataContext>()
				.AddScoped<IUow, Uow>()
				.AddScoped<IRepositoryProvider, RepositoryProvider>()
				.AddScoped<IRepositoryFactory, RepositoryFactory>()
				.AddScoped<IHotelsServices, HotelsServices>()
				.AddSingleton<IHttpErrorFactory, DefaultHttpErrorFactory>();
			return services;
		}

		public static IServiceCollection AddOpenApi(this IServiceCollection services) {
			services.AddSwaggerGen(setup => {
				setup.OperationFilter<FileUploadOperation>();
				setup.DescribeAllParametersInCamelCase();
				setup.SwaggerDoc("v1", new OpenApiInfo {
					Title = $"{nameof(TrisvagoHotels)} Api",
					Version = "v1",
					Description = "API",
					TermsOfService = null
				});
			});
			return services;
		}
	}
}