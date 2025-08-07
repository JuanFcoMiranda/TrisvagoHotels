using TrisvagoHotels.Model.CodeGen;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Model.CodeGen
{
    public static partial class HotelMapper
    {
        public static HotelCodeGenDto AdaptToCodeGenDto(this Hotel p1)
        {
            return p1 == null ? null : new HotelCodeGenDto()
            {
                Id = p1.Id,
                Nombre = p1.Nombre,
                Categoria = p1.Categoria,
                Descripcion = p1.Descripcion,
                Foto = p1.Foto,
                Localidad = p1.Localidad,
                Caracteristicas = p1.Caracteristicas,
                Destacado = p1.Destacado
            };
        }
        public static HotelCodeGenDto AdaptTo(this Hotel p2, HotelCodeGenDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            HotelCodeGenDto result = p3 ?? new HotelCodeGenDto();
            
            result.Id = p2.Id;
            result.Nombre = p2.Nombre;
            result.Categoria = p2.Categoria;
            result.Descripcion = p2.Descripcion;
            result.Foto = p2.Foto;
            result.Localidad = p2.Localidad;
            result.Caracteristicas = p2.Caracteristicas;
            result.Destacado = p2.Destacado;
            return result;
            
        }
    }
}