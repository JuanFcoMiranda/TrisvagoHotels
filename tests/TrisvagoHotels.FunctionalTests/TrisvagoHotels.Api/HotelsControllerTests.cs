using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TrisvagoHotels.FunctionalTests.Fixtures;
using Xunit;

namespace TrisvagoHotels.FunctionalTests.TrisvagoHotels.Api;

[Collection(IntegrationTestConstants.TestWebApplicationFactoryCollection)]
public class HotelsControllerTests
{
    private readonly TestWebApplicationFactory _factory;

    public HotelsControllerTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ShouldGetWeatherForecast()
    {
        var client = _factory.HttpClient;

        var response = await client.GetAsync("api/hotels");

        response.IsSuccessStatusCode.Should().Be(true);
        response.Should().BeOfType<HttpResponseMessage>();
        //var value = (MapModel)((OkObjectResult)actionResult.Result).Value;
        //value.Should().NotBeNull();
    }
}