using MediatR;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Commands {
    public record NewHotelCommand : IRequest<Hotel> {
        public int Id { get; init; }
        public string Nombre { get; init; }
        public string Categoria { get; init; }
        public string Descripcion { get; init; }
        public string Foto { get; init; }
        public string Localidad { get; init; }
        public string Caracteristicas { get; init; }
        public bool? Destacado { get; init; }
    }
}