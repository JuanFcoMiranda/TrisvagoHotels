using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Mappings.EntityMappings {
	public class HotelDetailMapping : IEntityTypeConfiguration<HotelDetail> {
		public void Configure(EntityTypeBuilder<HotelDetail> builder) {
			// Table
			builder.ToTable("Descripciones");

			// Primary Key
			builder.HasKey(t => t.Id);

			// Properties
			builder.Property(t => t.Nombre).IsRequired().HasMaxLength(200);
			builder.Property(t => t.Ciudad).HasMaxLength(100);
			builder.Property(t => t.Imagen).HasMaxLength(250);
			builder.Property(t => t.Descripcion).HasMaxLength(2000);
			builder.Property(t => t.Caracteristicas).HasMaxLength(2000);
			builder.Property(t => t.IdHotel).IsRequired();

			// Column Mappings
			builder.Property(t => t.Id).HasColumnName("Id");
			builder.Property(t => t.Nombre).HasColumnName("Nombre");
			builder.Property(t => t.Ciudad).HasColumnName("Ciudad");
			builder.Property(t => t.Descripcion).HasColumnName("Descripcion");
			builder.Property(t => t.Imagen).HasColumnName("Imagen");
			builder.Property(t => t.Caracteristicas).HasColumnName("Caracteristicas");
			builder.Property(t => t.IdHotel).HasColumnName("IdHotel");
		}
	}
}