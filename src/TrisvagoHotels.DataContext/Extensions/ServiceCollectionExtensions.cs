using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TrisvagoHotels.DataContext.Context;

namespace Microsoft.Extensions.DependencyInjection {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration) =>
			services
				.AddDbContext<MyDataContext>(options => {
					options.UseMySql(configuration["AppSettings:ConnectionStrings:DataAccessMySqlProvider"]);
				});
	}
}