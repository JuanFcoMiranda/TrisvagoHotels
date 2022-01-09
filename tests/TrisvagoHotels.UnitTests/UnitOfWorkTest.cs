using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Model.Entities;
using TrisvagoHotels.UnitTests.Fixtures;
using Xunit;
using FluentAssertions;

namespace TrisvagoHotels.UnitTests;

[Collection(Collections.UOW)]
public class UnitOfWorkTest : IDisposable
{
    private readonly IUow uow;

    public UnitOfWorkTest(DependencySetupFixture fixture)
    {
        uow = fixture.ServiceProvider.GetService<IUow>();
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        var hotel = new Hotel
        {
            Nombre = "Prueba",
            Id = 0
        };

        // Act
        await uow.Hotels.Add(hotel);
        await uow.CommitAsync();

        var number = await uow.Hotels.CountAll();

        // Assert
        uow.Should().NotBeNull();
        number.Should().Be(1);
    }

    [Fact]
    public async Task Uow_Add_One_Element()
    {
        // Arrange
        var hotel = new Hotel
        {
            Nombre = "Prueba"
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

    [Fact]
    public async Task Uow_Add_One_Element_Then_Remove_It()
    {
        // Arrange
        var hotel = new Hotel
        {
            Nombre = "Prueba"
        };

        // Act
        var initialCount = await uow.Hotels.CountAll();

        await uow.Hotels.Add(hotel);
        await uow.CommitAsync();

        var intermediateCount = await uow.Hotels.CountAll();

        await uow.Hotels.Delete(hotel);
        await uow.CommitAsync();

        var finalCount = await uow.Hotels.CountAll();

        // Assert
        uow.Should().NotBeNull();

        initialCount.Should().Be(0);
        intermediateCount.Should().Be(1);
        finalCount.Should().Be(0);
    }

    public async void Dispose()
    {
        if (uow != null)
        {
            var hotels = await uow.Hotels.GetAll();
            foreach (var hotel in hotels)
            {
                await uow.Hotels.Delete(hotel);
            }

            await uow.CommitAsync();
        }
    }
}