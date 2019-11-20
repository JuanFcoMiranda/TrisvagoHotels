using System;
using TrisvagoHotels.DataContext.Context;

namespace TrisvagoHotels.DataContracts.IRepositoryFactory {
	public interface IRepositoryFactory {
		Func<MyDataContext, object> GetRepositoryFactoryForEntityType<T>() where T : class;
		Func<MyDataContext, object> GetRepositoryFactory<T>();
	}
}