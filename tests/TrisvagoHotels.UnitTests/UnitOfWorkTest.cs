using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Model.Entities;
using TrisvagoHotels.UnitTests.Fixtures;
using Xunit;
using FluentAssertions;

namespace TrisvagoHotels.UnitTests {
    [Collection(Collections.UOW)]
    public class UnitOfWorkTest {
        private readonly IUow uow;

        public UnitOfWorkTest(DependencySetupFixture fixture) {
            uow = fixture.ServiceProvider.GetService<IUow>();
        }

        [Fact]
        public async Task Test1() {
            // Arrange
            var hotel = new Hotel {
                Nombre = "Prueba",
                Id = 0
            };

            // Act
            var previousCount = await uow.Hotels.CountAll();

            await uow.Hotels.Add(hotel);
            await uow.CommitAsync();

            var finalCount = await uow.Hotels.CountAll();
            
            // Assert
            uow.Should().NotBeNull();
            
            previousCount.Should().Be(0);
            finalCount.Should().Be(1);
        }
    }
}