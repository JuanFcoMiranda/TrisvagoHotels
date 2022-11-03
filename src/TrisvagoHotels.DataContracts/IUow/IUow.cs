using System.Threading.Tasks;
using TrisvagoHotels.DataContracts.IRepository;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.DataContracts.IUow;

public interface IUow {
    // Save pending changes to the data store.
    Task CommitAsync();
    Task EnsureCreated();
    Task EnsureDeleted();

    // Repositories
    IRepository<Hotel> Hotels { get; }
}