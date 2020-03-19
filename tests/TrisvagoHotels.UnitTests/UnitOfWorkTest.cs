using System.Threading.Tasks;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Model.Entities;
using TrisvagoHotels.UnitTests.Fixtures;
using Xunit;

namespace TrisvagoHotels.UnitTests {
    [Collection(Collections.UOW)]
    public class UnitOfWorkTest {
        private DependencySetupFixture fixture;
        private readonly IUow uow;

        public UnitOfWorkTest(DependencySetupFixture fixture) {
            this.fixture = fixture;
            uow = fixture.ServiceProvider.GetService(typeof(IUow)) as IUow;
        }

        [Fact]
        public async Task Test1() {
            // Arrange
            var hotel = new Hotel {
                Nombre = "Prueba",
                Id = 0
            };

            // Act
            await uow.Hotels.Add(hotel);
            await uow.CommitAsync();

            var number = await uow.Hotels.CountAll();
            
            // Assert
            Assert.NotNull(uow);
            Assert.Equal(0, number);
        }
    }
}