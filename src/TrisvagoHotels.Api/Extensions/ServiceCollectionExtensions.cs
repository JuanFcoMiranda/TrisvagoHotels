﻿using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrisvagoHotels.Api.Filters;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services, IWebHostEnvironment environment) =>
			services
				.AddProblemDetails(configure => {
					configure.IncludeExceptionDetails = _ => environment.EnvironmentName == "Development";
				});
		
		public static IServiceCollection AddCustomMvc(this IServiceCollection services) =>
			services
				.AddMvcCore(config => {
					config.Filters.Add(typeof( ValidModelStateFilterAttribute));
					config.EnableEndpointRouting = false;
				})
				.AddJsonOptions(config => config.JsonSerializerOptions.IgnoreNullValues = true)
				.AddApiExplorer()
				.AddApplicationPart(typeof(ServiceCollectionExtensions).Assembly)
				.Services;

		public static IServiceCollection AddCustomApiBehaviour(this IServiceCollection services) {
			return services.Configure<ApiBehaviorOptions>(options => {
				options.SuppressModelStateInvalidFilter = false;
				options.SuppressInferBindingSourcesForParameters = false;

				options.InvalidModelStateResponseFactory = context => {
					var problemDetails = new ValidationProblemDetails(context.ModelState) {
						Instance = context.HttpContext.Request.Path,
						Status = StatusCodes.Status400BadRequest,
						Type = $"https://httpstatuses.com/400",
						Detail = "Please refer to the errors property for additional details."
					};
					return new BadRequestObjectResult(problemDetails) {
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
}