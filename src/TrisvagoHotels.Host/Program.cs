using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrisvagoHotels.Api.Configuration;
using TrisvagoHotels.Host.Extensions;
using TrisvagoHotels.Host.Filters;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;
var services = builder.Services;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

services.AddLogging(logBuilder => logBuilder.SetMinimumLevel(LogLevel.Debug));

ApiConfiguration.ConfigureServices(services, environment)
    .AddCustomServices()
    .AddOpenApi()
    .AddCors(options => options.AddPolicy("CorsApi",
        builder => builder.WithOrigins(
                "http://localhost:4200",
                "http://localhost:3000",
                "http://localhost:8080")
            .AllowAnyHeader().AllowAnyMethod()))
    .AddEntityFrameworkCore(configuration)
    .AddMediatr()
    .AddCustomHealthChecks(configuration)
    .AddHashids(setup =>
    {
        setup.Salt = "your_salt";
        setup.MinHashLength = 16;
    })
    .AddSwaggerGen(c => c.OperationFilter<HashIdsOperationFilter>())
    .AddResponseCompression();

var app = builder.Build();

ApiConfiguration.Configure(app, host => host
    .UseIf(environment.IsDevelopment(), appBuilder => appBuilder.UseDeveloperExceptionPage())
    .UseSwagger()
    .UseIf(environment.IsDevelopment(), appBuilder => appBuilder.UseSwaggerUI(setup => {
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", nameof(TrisvagoHotels));
    }))
    .UseIf(!environment.IsDevelopment(), appBuilder => appBuilder.UseExceptionHandler("/error"))
    .UseHttpsRedirection()
    .UseCustomHealthchecks()
    .UseRouting()
    .UseCors("CorsApi")
    .UseHeaderDiagnostics()
    .UseHealthChecksUI()
    .UseResponseCompression()
);

await app.RunAsync();

public partial class Program {}