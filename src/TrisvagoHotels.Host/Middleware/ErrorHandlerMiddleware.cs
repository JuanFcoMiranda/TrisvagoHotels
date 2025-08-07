using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TrisvagoHotels.Api.HttpErrors;

namespace TrisvagoHotels.Host.Middleware;

public class ErrorHandlerMiddleware {
    private readonly RequestDelegate next;
    private readonly IHttpErrorFactory httpErrorFactory;
    private readonly ILogger<TrisvagoHotels> logger;

    public ErrorHandlerMiddleware(
        RequestDelegate next,
        IHttpErrorFactory httpErrorFactory,
        ILogger<TrisvagoHotels> logger) {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.httpErrorFactory = httpErrorFactory ?? throw new ArgumentNullException(nameof(httpErrorFactory));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context) {
        try {
            await next(context);
        } catch (Exception exception) {
            logger.LogError(exception.HResult, exception, exception.Message);
            await CreateHttpError(context, exception);
        }
    }

    private async Task CreateHttpError(HttpContext context, Exception exception) {
        var error = httpErrorFactory.CreateFrom(exception);

        await WriteResponseAsync(
            context,
            JsonSerializer.Serialize(error),
            "application/json",
            error.Status);
    }

    private Task WriteResponseAsync(
        HttpContext context,
        string content,
        string contentType,
        int statusCode) {
        context.Response.Headers["Content-Type"] = new[] { contentType };
        context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store, must-revalidate" };
        context.Response.Headers["Pragma"] = new[] { "no-cache" };
        context.Response.Headers["Expires"] = new[] { "0" };
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(content);
    }
}
