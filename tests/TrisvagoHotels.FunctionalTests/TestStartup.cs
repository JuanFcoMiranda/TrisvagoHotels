using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.Api.Configuration;

namespace TrisvagoHotels.FunctionalTests {
	public class TestStartup {
		public void ConfigureServices(IServiceCollection services) {
			ApiConfiguration.ConfigureServices(services);
		}

		public void Configure(IApplicationBuilder app) {
			ApiConfiguration.Configure(app, host => {
				return host;
			});
		}
	}
}