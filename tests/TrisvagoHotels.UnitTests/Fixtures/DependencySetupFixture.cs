using Microsoft.Extensions.DependencyInjection;

namespace TrisvagoHotels.UnitTests.Fixtures;

public class DependencySetupFixture {
    public DependencySetupFixture()
    {
        Init();
    }

    private void Init()
    {
        var serviceCollection = new ServiceCollection();

        var context = new DatabaseFixture();

        serviceCollection.AddScoped(p => context.Uow);
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; private set; }
}