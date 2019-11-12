using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrisvagoHotels.DataContracts.IServices;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class HotelsController : ControllerBase {
		private readonly ILogger<HotelsController> logger;
		private readonly IHotelsServices hotelsServices;

		public HotelsController(ILogger<HotelsController> logger, IHotelsServices hotelsServices) {
			this.logger = logger;
			this.hotelsServices = hotelsServices;
		}

		// GET api/hotels
		[HttpGet]
		public IActionResult Get() {
			var hotels = hotelsServices.GetAllHotels();
			return Ok(hotels);
		}

		// GET api/hotels/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Hotel>> Get(int id) {
			var hotel = await hotelsServices.GetHotel(id);
			if (hotel is null) {
				return NotFound();
			}
			return hotel;
		}

		// POST api/hotels
		[HttpPost, DisableRequestSizeLimit]
		public async Task<IActionResult> PostAsync(Hotel hotel) {
			if (hotel is null) {
				return BadRequest();
			}

			await hotelsServices.AddHotelAsync(hotel);
			logger.LogInformation($"Hotel inserted: {hotel}");
			return CreatedAtAction("Get", new { id = hotel.Id }, hotel);
		}

		// PUT api/hotels/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, Hotel hotel) {
			if (hotel is null || hotel.Id != id) {
				return BadRequest();
			}
			var myhotel = await hotelsServices.GetHotel(id);
			if (myhotel is null) {
				return NotFound();
			}
			myhotel.Categoria = hotel.Categoria;
			myhotel.Descripcion = hotel.Descripcion;
			myhotel.Nombre = hotel.Nombre;
			myhotel.Destacado = hotel.Destacado;
			myhotel.Localidad = hotel.Localidad;
			myhotel.Caracteristicas = hotel.Caracteristicas;
			await hotelsServices.UpdateHotelAsync(myhotel);
			return CreatedAtAction("Get", new { id = myhotel.Id }, myhotel);
		}

		// DELETE api/hotels/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id) {
			var myhotel = hotelsServices.GetHotel(id);
			if (myhotel is null) {
				return NotFound();
			}
			hotelsServices.RemoveHotelAsync(id);
			return Ok();
		}

		[HttpPost("Upload")]
		public async Task<IActionResult> Upload(int hotelId, IFormFile file) {
			if (!(file is null)) {
				var folderName = Path.Combine("Resources", "HotelPics");
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

				if (!Directory.Exists(filePath)) {
					Directory.CreateDirectory(filePath);
				}

				var uniqueFileName = $"{hotelId}_picture.png";
				var dbPath = Path.Combine(folderName, uniqueFileName);

				await using (var fileStream = new FileStream(Path.Combine(filePath, uniqueFileName), FileMode.Create)) {
					await file.CopyToAsync(fileStream);
				}

				var hotel = await hotelsServices.GetHotel(hotelId);
				if (!(hotel is null)) {
					hotel.Foto = dbPath;
					await hotelsServices.UpdateHotelAsync(hotel);
				}
			}
			return Ok();
		}
	}
}