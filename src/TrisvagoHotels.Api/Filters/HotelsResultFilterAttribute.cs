using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TrisvagoHotels.Api.Filters;

public class HotelsResultFilterAttribute : ResultFilterAttribute {
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next) {
        var resultFromAction = context.Result as ObjectResult;
        if (resultFromAction?.Value == null || resultFromAction.StatusCode < 200 ||
            resultFromAction.StatusCode >= 300) {
            await next();
            return;
        }
            
        // Use AutoMapper or something for mapping the result of the value to collection
            
        await next();
    }
}