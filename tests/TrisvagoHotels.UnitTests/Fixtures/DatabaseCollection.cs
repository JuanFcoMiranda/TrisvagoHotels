using Xunit;

namespace TrisvagoHotels.UnitTests.Fixtures;

[CollectionDefinition(Collections.UOW)]
public class DatabaseCollection : ICollectionFixture<DependencySetupFixture> {
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}