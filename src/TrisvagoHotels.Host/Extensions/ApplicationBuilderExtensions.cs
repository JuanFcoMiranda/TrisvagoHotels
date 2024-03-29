﻿using System;
using System.Diagnostics;
using System.Linq;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace TrisvagoHotels.Host.Extensions;

public static class ApplicationBuilderExtensions {
    public static IApplicationBuilder UseIf(this IApplicationBuilder app, bool condition, Func<IApplicationBuilder, IApplicationBuilder> action) =>
        condition ? action(app) : app;

    public static IApplicationBuilder UseCustomHealthchecks(this IApplicationBuilder app) {
        app.UseHealthChecks("/health", new HealthCheckOptions {
            Predicate = registration => registration.Name.Equals("self"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app.UseHealthChecks("/ready", new HealthCheckOptions {
            Predicate = registration => registration.Tags.Contains("dependencies"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }

    public static IApplicationBuilder UseHeaderDiagnostics(this IApplicationBuilder app) {
        if (app.ApplicationServices.GetService(typeof(DiagnosticListener)) is DiagnosticListener listener && listener.IsEnabled()) {
            return app.Use((context, next) => {
                var headers = string.Join("|", context.Request.Headers.Values.Select(h => h.ToString()));
                listener.Write("Api.Diagnostics.Headers", new { Headers = headers, HttpContext = context });
                return next();
            });
        }
        return app;
    }
}