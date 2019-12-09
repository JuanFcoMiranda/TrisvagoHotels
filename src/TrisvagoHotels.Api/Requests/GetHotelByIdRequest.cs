using MediatR;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Requests {
    public class GetHotelByIdRequest : IRequest<Hotel> {
        public int Id { get; }
        
        public GetHotelByIdRequest(int id) {
            this.Id = id;
        }
    }
}