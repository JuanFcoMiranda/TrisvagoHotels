using Microsoft.EntityFrameworkCore;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Providers.RepositoryFactory;
using TrisvagoHotels.Providers.RepositoryProvider;

namespace TrisvagoHotels.UnitTests.Fixtures {
    public class DatabaseFixture {
        public DatabaseFixture() {
            var options = new DbContextOptionsBuilder<MyDataContext>().UseSqlite("DataSource=trisvagohotels.db").Options;

            var repositoryFactory = new RepositoryFactory();
            var provider = new RepositoryProvider(repositoryFactory);
            var context = new MyDataContext(options);
            
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Uow = new Uow.Uow.Uow(provider, context);
        }

        public IUow Uow { get; }
    }
}