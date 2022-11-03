using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IUow;

namespace TrisvagoHotels.FunctionalTests.Fixtures;

public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected BaseWebApplicationFactory()
    {
        this.HttpClient = this.CreateClient();
    }

    public HttpClient HttpClient { get; }

    protected IServiceProvider ServiceProvider { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(appBuilder =>
        {
            appBuilder.AddJsonFile("appsettings.Testing.json", false);
            appBuilder.AddEnvironmentVariables("ASPNETCORE");
        }).UseEnvironment("Testing");

        builder.ConfigureServices((configuration, services) =>
        {
            services.AddDbContextPool<MyDataContext>(options =>
            {
                options.UseSqlite(configuration.Configuration.GetConnectionString("DefaultConnection"));
            });

            ServiceProvider = services.BuildServiceProvider();

            var context = ServiceProvider.GetRequiredService<IUow>();
            context.EnsureDeleted();
            context.EnsureCreated();
        });
    }

    protected virtual void SeedDatabase(IUow uow)
    {
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            switch (ServiceProvider)
            {
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }
        }

        base.Dispose(disposing);
    }
}