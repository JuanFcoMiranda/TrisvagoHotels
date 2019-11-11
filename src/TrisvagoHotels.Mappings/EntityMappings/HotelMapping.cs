using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Mappings.EntityMappings {
	public class HotelMapping : IEntityTypeConfiguration<Hotel> {
		public void Configure(EntityTypeBuilder<Hotel> builder) {
			// Table
			builder.ToTable("Hoteles");

			// Primary Key
			builder.HasKey(t => t.Id);

			// Properties
			builder.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
			builder.Property(t => t.Categoria).HasMaxLength(50);
			builder.Property(t => t.Descripcion).HasMaxLength(300);
			builder.Property(t => t.Localidad).HasMaxLength(100);
			builder.Property(t => t.Caracteristicas).HasMaxLength(3000);
			builder.Property(t => t.Foto).HasMaxLength(200);
			builder.Property(t => t.Destacado);

			// Column Mappings
			builder.Property(t => t.Id).HasColumnName("Id");
			builder.Property(t => t.Nombre).HasColumnName("Nombre");
			builder.Property(t => t.Categoria).HasColumnName("Categoria");
			builder.Property(t => t.Descripcion).HasColumnName("Descripcion");
			builder.Property(t => t.Foto).HasColumnName("Foto");
			builder.Property(t => t.Localidad).HasColumnName("Localidad");
			builder.Property(t => t.Caracteristicas).HasColumnName("Caracteristicas");
			builder.Property(t => t.Destacado).HasColumnName("Destacado");
		}
	}
}