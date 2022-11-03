using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.DataContracts.IUow;

namespace TrisvagoHotels.FunctionalTests.Fixtures;

public sealed class TestWebApplicationFactory : BaseWebApplicationFactory<Program>
{
    private IUow uow;
    public IUow Uow => uow ??= ServiceProvider.GetRequiredService<IUow>();

    protected override void SeedDatabase(IUow uow)
    {
        
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Uow.EnsureDeleted();
        }

        base.Dispose(disposing);
    }
}