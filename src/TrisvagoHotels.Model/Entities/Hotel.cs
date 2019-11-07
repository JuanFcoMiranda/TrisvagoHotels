﻿namespace TrisvagoHotels.Model.Entities {
	public class Hotel {
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Categoria { get; set; }
		public string Descripcion { get; set; }
		public string Foto { get; set; }
		public bool? Destacado { get; set; }
	}
}