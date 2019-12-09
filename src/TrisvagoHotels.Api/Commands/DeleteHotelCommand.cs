using MediatR;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Commands {
    public class DeleteHotelCommand : IRequest<Hotel> {
        public DeleteHotelCommand(Hotel myhotel) {
            this.Hotel = myhotel;
        }

        public Hotel Hotel { get; }
    }
}