using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrisvagoHotels.Api.Commands;
using TrisvagoHotels.Api.Filters;
using TrisvagoHotels.Api.Requests;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class HotelsController : ControllerBase {
		private readonly ILogger<HotelsController> logger;
		private readonly IMediator mediator;

		public HotelsController(ILogger<HotelsController> logger, IMediator mediator) {
			this.logger = logger;
			this.mediator = mediator;
		}

		// GET api/hotels
		[HttpGet]
		public async Task<IActionResult> Get() {
			var hotels = await mediator.Send(new GetMyHotelsRequest());
			return Ok(hotels);
		}

		// GET api/hotels/5
		[HttpGet("{id}"), HotelResultFilter]
		public async Task<ActionResult<Hotel>> Get(int id) {
			var hotel = await mediator.Send(new GetHotelByIdRequest(id));
			if (hotel is null) {
				return NotFound();
			}
			return hotel;
		}

		// POST api/hotels
		[HttpPost, DisableRequestSizeLimit]
		public async Task<IActionResult> PostAsync(NewHotelCommand hotel) {
			if (hotel is null) {
				return BadRequest();
			}
			var savedHotel = await mediator.Send(hotel);
			logger.LogInformation($"Hotel inserted: {hotel}");
			return CreatedAtAction("Get", new { id = savedHotel.Id }, hotel);
		}

		// PUT api/hotels/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(UpdateHotelCommand hotel) {
			await mediator.Send(hotel);
			return CreatedAtAction("Get", new { id = hotel.Id }, hotel);
		}

		// DELETE api/hotels/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) {
			var myhotel = await mediator.Send(new GetHotelByIdRequest(id));
			if (myhotel is null) {
				return NotFound();
			}
			await mediator.Send(new DeleteHotelCommand(myhotel));
			return Ok();
		}

		[HttpPost("Upload")]
		public async Task<IActionResult> Upload(int hotelId, IFormFile file) {
			if (file is null) return BadRequest();
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

			var hotel = await mediator.Send(new GetHotelByIdRequest(hotelId));
			if (!(hotel is null)) {
				hotel.Foto = dbPath;
				//await hotelsServices.UpdateHotelAsync(hotel);
			}
			return Ok();
		}
	}
}