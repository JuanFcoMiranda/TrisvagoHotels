using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrisvagoHotels.Model.Entities;

namespace TrisvagoHotels.Api.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase {
		private static readonly string[] Summaries = {
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger) {
			this.logger = logger;
		}

		[HttpGet]
		public IEnumerable<Hotel> Get() {
			var rng = new Random();
			logger.Log(LogLevel.Information, $"Random number: { rng }");
			return Enumerable.Range(1, 5).Select(index => new Hotel()).ToArray();
		}
	}
}