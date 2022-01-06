using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrisvagoHotels.Api.Commands;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Handlers;

public class DeleteHotelHandler : IRequestHandler<DeleteHotelCommand, Hotel> {
    private readonly IHotelsServices hotelsServices;

    public DeleteHotelHandler(IHotelsServices hotelsServices) {
        this.hotelsServices = hotelsServices;
    }
        
    public async Task<Hotel> Handle(DeleteHotelCommand request, CancellationToken cancellationToken) {
        await hotelsServices.RemoveHotelAsync(request.Hotel.Id);
        return await Task.FromResult(request.Hotel);
    }
}