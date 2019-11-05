using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TrisvagoHotels.DataContext {
	public class MyDataContext : DbContext {
		public MyDataContext(DbContextOptions<MyDataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			Assembly[] assemblies = {
                // Add your assembiles here.
                //typeof(EmpleadoMapping).Assembly
			};
			foreach (var assembly in assemblies.Distinct()) {
				modelBuilder.ApplyConfigurationsFromAssembly(assembly);
			}
		}
	}
}