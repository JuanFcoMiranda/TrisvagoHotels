using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using TrisvagoHotels.Host.Middleware;

namespace TrisvagoHotels.Host.Filters {
	public class HostStartupFilter : IStartupFilter {
		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) {
			return builder => {
				builder.UseMiddleware<ErrorHandlerMiddleware>();
				next(builder);
			};
		}
	}
}