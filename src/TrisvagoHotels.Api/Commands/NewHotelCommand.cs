using MediatR;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Commands {
    public class NewHotelCommand : IRequest<Hotel> {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public string Localidad { get; set; }
        public string Caracteristicas { get; set; }
        public bool? Destacado { get; set; }
    }
}