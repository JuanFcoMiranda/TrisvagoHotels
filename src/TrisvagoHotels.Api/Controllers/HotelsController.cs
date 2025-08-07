using System.IO;
using System.Threading.Tasks;
using AspNetCore.Hashids.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrisvagoHotels.Api.Commands;
using TrisvagoHotels.Api.Filters;
using TrisvagoHotels.Api.Requests;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController(ILogger<HotelsController> logger, IMediator mediator) : ControllerBase
{
    // GET api/hotels
    [HttpGet]
    public async Task<IActionResult> Get() {
        var hotels = await mediator.Send(new GetMyHotelsRequest());
        return Ok(hotels);
    }

    // GET api/hotels/5
    [HttpGet("{id:hashids}"), HotelResultFilter]
    public async Task<IActionResult> Get([FromRoute][ModelBinder(typeof(HashidsModelBinder))]int id) {
        var hotel = await mediator.Send(new GetHotelByIdRequest(id));
        if (hotel is null) {
            return NotFound();
        }
        return Ok(hotel);
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
    [HttpPut]
    public async Task<IActionResult> Put(UpdateHotelCommand hotelCommand)
    {
        var hotel = await mediator.Send(hotelCommand);
        if (hotel is null)
        {
            return BadRequest();
        }

        return CreatedAtAction("Get", new { id = hotel.Id }, hotel);
    }

    // DELETE api/hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id) {
        var myhotel = await mediator.Send(new GetHotelByIdRequest(id));
        if (myhotel is null) {
            return NotFound();
        }
        await mediator.Send(new DeleteHotelCommand(myhotel));
        return Ok();
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> Upload([FromRoute][ModelBinder(typeof(HashidsModelBinder))]int hotelId, IFormFile file) {
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
        if (hotel is not null) {
            hotel.Foto = dbPath;
        }
        return Ok();
    }
}