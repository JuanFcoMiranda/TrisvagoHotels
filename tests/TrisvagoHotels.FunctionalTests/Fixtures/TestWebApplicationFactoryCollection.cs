using Xunit;

namespace TrisvagoHotels.FunctionalTests.Fixtures;

[CollectionDefinition(IntegrationTestConstants.TestWebApplicationFactoryCollection)]
public class TestWebApplicationFactoryCollection : ICollectionFixture<TestWebApplicationFactory>
{
}