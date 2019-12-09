using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrisvagoHotels.Api.Commands;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Handlers {
    public class NewHotelHandler : IRequestHandler<NewHotelCommand,Hotel> {
        private readonly IHotelsServices hotelsServices;

        public NewHotelHandler(IHotelsServices hotelsServices) {
            this.hotelsServices = hotelsServices;
        }
        
        public Task<Hotel> Handle(NewHotelCommand request, CancellationToken cancellationToken) {
            var myhotel = new Hotel {
                Categoria = request.Categoria,
                Descripcion = request.Descripcion,
                Nombre = request.Nombre,
                Destacado = request.Destacado,
                Localidad = request.Localidad,
                Caracteristicas = request.Caracteristicas
            };
            hotelsServices.AddHotelAsync(myhotel);
            return Task.FromResult(myhotel);
        }
    }
}