using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrisvagoHotels.Api.Commands;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Handlers {
    public class UpdateHotelHandler : IRequestHandler<UpdateHotelCommand, Hotel> {
        private readonly IHotelsServices hotelsServices;

        public UpdateHotelHandler(IHotelsServices hotelsServices) {
            this.hotelsServices = hotelsServices;
        }
        
        public async Task<Hotel> Handle(UpdateHotelCommand request, CancellationToken cancellationToken) {
            var myhotel = await hotelsServices.GetHotel(request.Id);
            myhotel.Categoria = request.Categoria;
            myhotel.Descripcion = request.Descripcion;
            myhotel.Nombre = request.Nombre;
            myhotel.Destacado = request.Destacado;
            myhotel.Localidad = request.Localidad;
            myhotel.Caracteristicas = request.Caracteristicas;
            await hotelsServices.UpdateHotelAsync(myhotel);
            return await Task.FromResult(myhotel);
        }
    }
}