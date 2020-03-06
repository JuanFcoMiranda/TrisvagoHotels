using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepositoryFactory;
using TrisvagoHotels.DataContracts.IRepositoryProvider;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Providers.RepositoryFactory;
using TrisvagoHotels.Providers.RepositoryProvider;

namespace TrisvagoHotels.UnitTests.Fixtures {
    public class DependencySetupFixture {
        public DependencySetupFixture() {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyDataContext>(options => options.UseSqlite("Data Source=blogging.db"));
            serviceCollection.AddScoped<MyDataContext, MyDataContext>();
            serviceCollection.AddScoped<IUow, Uow.Uow.Uow>();
            serviceCollection.AddScoped<IRepositoryProvider, RepositoryProvider>();
            serviceCollection.AddScoped<IRepositoryFactory, RepositoryFactory>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; }
    }
}