using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrisvagoHotels.Host.Filters;

namespace TrisvagoHotels {
	public static class Program {
		public static void Main(string[] args) {
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.ConfigureServices(services => {
					services.AddTransient<IStartupFilter, HostStartupFilter>();
					services.AddLogging(logBuilder => logBuilder.SetMinimumLevel(LogLevel.Debug));
				})
				.UseStartup<Startup>();
	}
}