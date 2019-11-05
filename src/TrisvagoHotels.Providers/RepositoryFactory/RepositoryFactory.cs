using System;
using System.Collections.Generic;
using TrisvagoHotels.DataContext;
using TrisvagoHotels.DataContracts.IRepositoryFactory;
using TrisvagoHotels.Repositories;

namespace TrisvagoHotels.Providers.RepositoryFactory {
	public class RepositoryFactory : IRepositoryFactory {
		private readonly IDictionary<Type, Func<MyDataContext, object>> repositoryFactory;

		private IDictionary<Type, Func<MyDataContext, object>> GetFactories() {
			return new Dictionary<Type, Func<MyDataContext, object>> {
			};
		}

		public RepositoryFactory() {
			repositoryFactory = GetFactories();
		}

		public Func<MyDataContext, object> GetRepositoryFactoryForEntityType<T>() where T : class {
			return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
		}

		public Func<MyDataContext, object> GetRepositoryFactory<T>() {
			repositoryFactory.TryGetValue(typeof(T), out var factory);
			return factory;
		}

		private Func<MyDataContext, object> DefaultEntityRepositoryFactory<T>() where T : class {
			return context => new EFRepository<T>(context);
		}
	}
}