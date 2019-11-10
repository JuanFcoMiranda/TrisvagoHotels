#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Services.Hotels {
	public class HotelsServices : IHotelsServices {
		private readonly IUow uow;

		public HotelsServices(IUow uow) {
			this.uow = uow;
		}

		public async Task AddHotelAsync(Hotel hotel) {
			uow.Hotels.Add(hotel);
			await uow.CommitAsync();
		}

		public IAsyncEnumerable<Hotel> GetAllHotels() => uow.Hotels.GetAll();

		public async Task<Hotel?> GetHotel(int id) => await uow.Hotels.GetById(id);

		public async Task RemoveHotelAsync(int id) {
			uow.Hotels.Delete(id);
			await uow.CommitAsync();
		}

		public async Task UpdateHotelAsync(Hotel hotel) {
			uow.Hotels.Update(hotel);
			await uow.CommitAsync();
		}
	}
}