using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TrisvagoHotels.Mappings.EntityMappings;

namespace TrisvagoHotels.DataContext.Context {
	public class MyDataContext : DbContext {
		public MyDataContext(DbContextOptions<MyDataContext> options) : base(options) {
			Database.Migrate();
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			Assembly[] assemblies = {
                // Add your assembiles here.
                typeof(HotelMapping).Assembly
			};
			foreach (var assembly in assemblies.Distinct()) {
				modelBuilder.ApplyConfigurationsFromAssembly(assembly);
			}
		}
	}
}