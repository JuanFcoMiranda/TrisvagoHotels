using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.Api.Filters;

namespace TrisvagoHotels.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomMvc(this IServiceCollection services) =>
        services
            .AddMvcCore(config =>
            {
                config.Filters.Add(typeof(ValidModelStateFilterAttribute));
                config.EnableEndpointRouting = false;
            })
            //.AddJsonOptions(config => config.JsonSerializerOptions.IgnoreNullValues = true)
            .AddJsonOptions(config => config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)
            .AddApiExplorer()
            .AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly)
            .Services;

    public static IServiceCollection AddCustomApiBehaviour(this IServiceCollection services)
    {
        return services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = false;
            options.SuppressInferBindingSourcesForParameters = false;

            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Type = $"https://httpstatuses.com/400",
                    Detail = "Please refer to the errors property for additional details."
                };
                return new BadRequestObjectResult(problemDetails)
                {
                    ContentTypes =
                    {
                        "application/problem+json",
                        "application/problem+xml"
                    }
                };
            };
        });
    }
}