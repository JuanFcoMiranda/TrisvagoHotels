using System;
using System.Threading.Tasks;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepository;
using TrisvagoHotels.DataContracts.IRepositoryProvider;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Uow.Uow {
	public class Uow : IUow, IDisposable {
		private readonly MyDataContext context;
		private IRepositoryProvider RepositoryProvider { get; }

		public Uow(IRepositoryProvider provider, MyDataContext context) {
			provider.Context = this.context = context;
			RepositoryProvider = provider;
		}

		public IRepository<Hotel> Hotels => GetStandardRepo<Hotel>();

		public async Task CommitAsync() {
			await context.SaveChangesAsync();
		}

		private IRepository<T> GetStandardRepo<T>() where T : class => RepositoryProvider.GetRepositoryForEntityType<T>();

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (disposing) {
				context?.Dispose();
			}
		}
	}
}
