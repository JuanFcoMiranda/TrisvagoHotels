using System;
using System.Threading.Tasks;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepository;
using TrisvagoHotels.DataContracts.IRepositoryProvider;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Uow {
	public class Uow : IUow, IDisposable {
		private MyDataContext Context { get; set; }
		protected IRepositoryProvider RepositoryProvider { get; set; }

		public Uow(IRepositoryProvider provider, MyDataContext entities) {
			CreateDbContext(entities);
			provider.Context = Context;
			RepositoryProvider = provider;
		}

		private void CreateDbContext(MyDataContext esEntities) {
			Context = esEntities;
			Context.Database.BeginTransaction();
		}

		public IRepository<Hotel> Hotels => GetStandardRepo<Hotel>();

		public async Task CommitAsync() {
			await Context.SaveChangesAsync();
			Context.Database.CommitTransaction();
		}

		private T GetRepo<T>() where T : class {
			return RepositoryProvider.GetRepository<T>();
		}

		private IRepository<T> GetStandardRepo<T>() where T : class {
			return RepositoryProvider.GetRepositoryForEntityType<T>();
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				Context?.Dispose();
			}
		}
	}
}