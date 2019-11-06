﻿using System;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.Api.Extensions;

namespace TrisvagoHotels.Api.Configuration {
	public class ApiConfiguration {
		public static IServiceCollection ConfigureServices(IServiceCollection services, IWebHostEnvironment environment) => services
				.AddHttpContextAccessor()
				.AddCustomMvc()
				.AddCustomProblemDetails(environment)
				.AddCustomApiBehaviour();

		public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configureHost) {
			return configureHost(app)
				.UseHttpsRedirection()
				.UseProblemDetails()
				.UseHsts()
				.UseMvc(routes => routes.MapRoute("swagger", "{controller=Values}/{action=Swagger}"))
				.UseRouting()
				.UseEndpoints(config => {
					config.MapHealthChecks("/healthz", new HealthCheckOptions {
						Predicate = _ => true,
						ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
					});
					config.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
					config.MapHealthChecksUI();
				});
		}
	}
}