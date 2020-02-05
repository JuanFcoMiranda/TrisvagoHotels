using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using TrisvagoHotels.Api.Configuration;

namespace TrisvagoHotels.Host {
	public class Startup {
		public IConfiguration Configuration { get; }

		private IWebHostEnvironment Environment { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
			Configuration = configuration;
			Environment = environment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			ApiConfiguration.ConfigureServices(services, Environment, Configuration)
				.AddCustomServices()
				.AddOpenApi()
				.AddMediatr()
				// In production, the Angular files will be served from this directory
				.AddSpaStaticFiles(configuration => {
					configuration.RootPath = "ClientApp/dist";
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			ApiConfiguration.Configure(app, host => host
					.UseIf(env.IsDevelopment(), appBuilder => appBuilder.UseDeveloperExceptionPage())
					.UseSwagger()
					.UseIf(env.IsDevelopment(), appBuilder => appBuilder.UseSwaggerUI(setup => {
						setup.SwaggerEndpoint("/swagger/v1/swagger.json", nameof(TrisvagoHotels));
					}))
					.UseHttpsRedirection()
					.UseStaticFiles(new StaticFileOptions {
						FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
						RequestPath = new PathString("/Resources")
					})
					.UseCustomHealthchecks()
					.UseRouting()
					.UseHeaderDiagnostics()
					.UseHealthChecksUI()
			);
			if (!env.IsDevelopment()) {
				app.UseSpaStaticFiles();
			}
			app.UseSpa(spa => {
				spa.Options.SourcePath = "ClientApp";
				if (env.IsDevelopment()) {
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}
	}
}