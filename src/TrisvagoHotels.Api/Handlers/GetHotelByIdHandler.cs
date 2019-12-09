using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrisvagoHotels.Api.Requests;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Handlers {
    public class GetHotelByIdHandler : IRequestHandler<GetHotelByIdRequest, Hotel> {
        private readonly IHotelsServices hotelsServices;

        public GetHotelByIdHandler(IHotelsServices hotelsServices) {
            this.hotelsServices = hotelsServices;
        }

        public Task<Hotel> Handle(GetHotelByIdRequest request, CancellationToken cancellationToken) => hotelsServices.GetHotel(request.Id);
    }
}