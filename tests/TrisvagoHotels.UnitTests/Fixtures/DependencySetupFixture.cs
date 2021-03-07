using Microsoft.Extensions.DependencyInjection;

namespace TrisvagoHotels.UnitTests.Fixtures {
    public class DependencySetupFixture {
        public DependencySetupFixture() {
            var serviceCollection = new ServiceCollection();

            var context = new DatabaseFixture();
            
            serviceCollection.AddSingleton(p => context.Uow);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}