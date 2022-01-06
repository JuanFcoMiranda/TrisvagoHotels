using System;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.Api.Extensions;

namespace TrisvagoHotels.Api.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection ConfigureServices(IServiceCollection services, IWebHostEnvironment environment) => services
        .AddHttpContextAccessor()
        .AddCustomMvc()
        .AddCustomApiBehaviour();

    public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configureHost) =>
        configureHost(app)
            .UseHttpsRedirection()
            .UseHsts()
            .UseRouting()
            .UseEndpoints(config =>
            {
                config.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

                });
                config.MapControllers();
                config.MapHealthChecksUI();
            });
}