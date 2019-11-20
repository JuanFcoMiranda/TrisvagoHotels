using System;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepository;

namespace TrisvagoHotels.DataContracts.IRepositoryProvider {
	public interface IRepositoryProvider {
		MyDataContext Context { get; set; }
		T GetRepository<T>(Func<MyDataContext, object> factory = null) where T : class;
		IRepository<T> GetRepositoryForEntityType<T>() where T : class;
		void SetRepository<T>(T repository);
	}
}