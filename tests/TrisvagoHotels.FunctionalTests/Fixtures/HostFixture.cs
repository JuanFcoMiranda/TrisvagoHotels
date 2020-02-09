using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace TrisvagoHotels.FunctionalTests.Fixtures {
	public class HostFixture {
		public TestServer Server { get; }

		public HostFixture() {
			Server = new TestServer(
				new WebHostBuilder()
				.ConfigureServices(services => {

				})
				.UseStartup<TestStartup>()
			);
		}
	}
}
