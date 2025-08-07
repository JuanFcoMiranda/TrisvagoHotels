using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using TrisvagoHotels.Api.Commands;
using TrisvagoHotels.Api.Controllers;
using TrisvagoHotels.Api.Requests;
using TrisvagoHotels.Model.CodeGen;
using TrisvagoHotels.Model.Entities;
using Xunit;

namespace TrisvagoHotels.FunctionalTests.TrisvagoHotels.Api;

public class HotelsControllerTests
{
    private readonly HotelsController sut;
    private readonly ILogger<HotelsController> logger = Substitute.For<ILogger<HotelsController>>();
    private readonly IMediator mediator = Substitute.For<IMediator>();

    public HotelsControllerTests()
    {
        sut = new HotelsController(logger, mediator);
    }

    [Fact]
    public async Task Get_ReturnsAllHotels_WhenHotelsExists()
    {
        // Arrange
        var items =  new Hotel[] {
            new() { Nombre = "Prueba", Id = 1 },
            new() { Nombre = "Prueba2", Id = 2 },
            new() { Nombre = "Prueba3", Id = 3 },
        };
        
        var dtos = items.Select(x => new Hotel{ Nombre = x.Nombre, Id = x.Id });
        
        mediator.Send(Arg.Any<GetMyHotelsRequest>(), Arg.Any<CancellationToken>()).Returns(dtos);

        // Act
        var actionResult = await sut.Get() as OkObjectResult;

        // Assert
        actionResult.Value.Should().NotBeNull();
        actionResult.Value.As<IEnumerable<Hotel>>().Should().HaveCount(items.Length);
        actionResult.Value.Should().BeEquivalentTo(items);
    }
    
    [Fact]
    public async Task Get_ReturnsEmptyList_WhenHotelsNotExists()
    {
        // Arrange
        mediator.Send(Arg.Any<GetMyHotelsRequest>())
            .Returns(Enumerable.Empty<Hotel>());

        // Act
        var actionResult = await sut.Get() as OkObjectResult;

        // Assert
        actionResult.Value.Should().NotBeNull();
        actionResult.Value.As<IEnumerable<Hotel>>().Should().BeEmpty();
    }
    
    [Fact]
    public async Task Get_ReturnsHotel_WhenHotelExists()
    {
        // Arrange
        var hotel =  new Hotel {
             Nombre = "Prueba", 
             Id = 1
        };
        var request = new GetHotelByIdRequest(hotel.Id);
        
        mediator.Send(Arg.Do<GetHotelByIdRequest>(arg => request = arg))
            .Returns(hotel);

        // Act
        var actionResult = await sut.Get(hotel.Id) as OkObjectResult;

         // Assert
         actionResult.Value.Should().NotBeNull();
         actionResult.Value.As<Hotel>().Should().BeEquivalentTo(hotel);
         actionResult.Value.As<Hotel>().Id.Should().Be(hotel.Id);
         actionResult.Value.As<Hotel>().Nombre.Should().Be(hotel.Nombre);

         actionResult.Should().NotBeNull();
         actionResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
    
    [Fact]
    public async Task Get_ReturnsNotFound_WhenHotelNotExists()
    {
        // Arrange
        var itemId = 10;

        mediator.Send(Arg.Is(new GetHotelByIdRequest(itemId)))
            .Returns(null as Hotel);

        // Act
        var actionResult = await sut.Get(itemId) as NotFoundResult;

        // Assert
        actionResult.Should().NotBeNull();
        actionResult?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    [Fact]
    public async Task Post_ReturnsCreated_WhenHotelCreated()
    {
        // Arrange
        var item = new NewHotelCommand
        {
            Nombre = "Prueba",
            Id = 1,
            Categoria = "Categoria",
            Descripcion = "Descripcion",
            Foto = "Foto.jpg",
            Localidad = "Localidad",
            Caracteristicas = "Caracteristicas",
            Destacado = true
        };
        
        var command = item.Adapt<NewHotelCommand>();

        mediator.Send(Arg.Is(item), Arg.Any<CancellationToken>()) .Returns(item.Adapt<Hotel>());

        // Act
        var actionResult = await sut.PostAsync(command) as CreatedAtActionResult;

        // Assert
        actionResult.Should().NotBeNull();
        actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }
    
    [Fact]
    public async Task Post_ReturnsBadRequest_WhenHotelNotCreated()
    {
        // Arrange
        var item =  new Hotel {
            Nombre = "Prueba",
            Id = 1
        };
        
        NewHotelCommand command = null;

        mediator.Send(Arg.Any<NewHotelCommand>())
            .Returns(null as Hotel);

        // Act
        var actionResult = await sut.PostAsync(command) as BadRequestResult;

        // Assert
        actionResult.Should().NotBeNull();
        actionResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    /*[Fact]
    public async Task Put_ReturnsCreated_WhenHotelUpdated()
    {
        // Arrange
        var item =  new Hotel {
            Nombre = "Prueba",
            Id = 1
        };
        
        var command = item.Adapt<UpdateHotelCommand>();

        mediator.Send<(Arg.Any<UpdateHotelCommand>(), Arg.Any<CancellationToken>()).Returns(item);

        // Act
        var actionResult = await sut.Put(command) as CreatedAtActionResult;

        // Assert
        actionResult.Should().NotBeNull();
        actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }*/
    
    [Fact]
    public async Task Put_ReturnsBadRequest_WhenHotelNotUpdated()
    {
        // Arrange
        var item =  new Hotel {
            Nombre = "Prueba",
            Id = 1
        };
        
        UpdateHotelCommand command = null;

        mediator.Send(Arg.Any<UpdateHotelCommand>()).Returns(null as Hotel);

        // Act
        var actionResult = await sut.Put(command) as BadRequestResult;

        // Assert
        actionResult.Should().NotBeNull();
        actionResult?.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }
    
    [Fact]
    public async Task Delete_ReturnsOk_WhenHotelDeleted()
    {
        // Arrange
        var hotel =  new Hotel {
            Nombre = "Prueba",
            Id = 1
        };
        
        var getHotelByIdRequest = new GetHotelByIdRequest(hotel.Id);
        mediator.Send(Arg.Do<GetHotelByIdRequest>(arg => getHotelByIdRequest = arg)).Returns(hotel);
        
        var deleteHotelCommand = new DeleteHotelCommand(hotel);
        mediator.Send(Arg.Do<DeleteHotelCommand>(param => deleteHotelCommand = param)).Returns(hotel);

        // Act
        var actionResult = await sut.Delete(hotel.Id) as OkResult;

        // Assert
        actionResult.Should().NotBeNull();
        actionResult?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}