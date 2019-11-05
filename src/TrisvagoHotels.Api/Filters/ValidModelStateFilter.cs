using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrisvagoHotels.Api.HttpErrors;

namespace TrisvagoHotels.Api.Filters {
	internal class ValidModelStateFilter : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext context) {
			if (context.ModelState.IsValid) {
				return;
			}

			var validationErrors = context.ModelState
				.Keys
				.SelectMany(k => context.ModelState[k].Errors)
				.Select(e => e.ErrorMessage)
				.ToArray();

			var error = HttpError.CreateHttpValidationError(
				status: HttpStatusCode.BadRequest,
				userMessage: new[] { "There are validation errors" },
				validationErrors: validationErrors);

			context.Result = new BadRequestObjectResult(error);
		}
	}
}