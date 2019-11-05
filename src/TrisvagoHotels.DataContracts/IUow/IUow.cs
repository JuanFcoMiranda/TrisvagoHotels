using TrisvagoHotels.DataContracts.IRepository;

namespace TrisvagoHotels.DataContracts.IUow {
	public interface IUow {
		// Save pending changes to the data store.
		void Commit();

		// Repositories
	}
}