#nullable enable

using System.Collections.Generic;
using System.Threading.Tasks;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.DataContracts.IUow;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Services.Hotels;

public class HotelsServices(IUow uow) : IHotelsServices
{
    public async Task AddHotelAsync(Hotel hotel)
    {
        await uow.Hotels.Add(hotel);
        await uow.CommitAsync();
    }

    public async Task<IEnumerable<Hotel>> GetAllHotels() => await uow.Hotels.GetAll();

    public async Task<Hotel?> GetHotel(int id) => await uow.Hotels.GetById(id);

    public async Task RemoveHotelAsync(int id)
    {
        await uow.Hotels.Delete(id);
        await uow.CommitAsync();
    }

    public async Task UpdateHotelAsync(Hotel hotel)
    {
        await uow.Hotels.Update(hotel);
        await uow.CommitAsync();
    }
}