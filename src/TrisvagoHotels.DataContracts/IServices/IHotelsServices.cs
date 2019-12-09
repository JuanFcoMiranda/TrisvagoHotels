#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.DataContracts.IServices {
	public interface IHotelsServices {
		Task<IEnumerable<Hotel>> GetAllHotels();
		Task<Hotel?> GetHotel(int id);
		Task AddHotelAsync(Hotel hotel);
		Task UpdateHotelAsync(Hotel hotel);
		Task RemoveHotelAsync(int id);
	}
}