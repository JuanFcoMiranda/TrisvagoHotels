using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using TrisvagoHotels.Api.HttpErrors;
using TrisvagoHotels.Api.Requests;
using TrisvagoHotels.Api.Validations;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepositoryFactory;
using TrisvagoHotels.DataContracts.IRepositoryProvider;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Providers.RepositoryFactory;
using TrisvagoHotels.Providers.RepositoryProvider;
using TrisvagoHotels.Services.Hotels;

namespace TrisvagoHotels.Host.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddCustomServices(this IServiceCollection services) =>
        services
            .AddScoped<MyDataContext, MyDataContext>()
            .AddScoped<IUow, Uow.Uow.Uow>()
            .AddScoped<IRepositoryProvider, RepositoryProvider>()
            .AddScoped<IRepositoryFactory, RepositoryFactory>()
            .AddScoped<IHotelsServices, HotelsServices>()
            .AddSingleton<IHttpErrorFactory, DefaultHttpErrorFactory>();

    public static IServiceCollection AddOpenApi(this IServiceCollection services) =>
        services.AddSwaggerGen(setup => {
            setup.DescribeAllParametersInCamelCase();
            setup.SwaggerDoc("v1", new OpenApiInfo {
                Title = $"{nameof(TrisvagoHotels)} Api",
                Version = "v1",
                Description = "API",
                TermsOfService = null
            });
        });

    public static IServiceCollection AddMediatr(this IServiceCollection services) =>
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMyHotelsRequest).Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
}