using AspNetCore.Hashids.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.OpenApi;

namespace TrisvagoHotels.Host.Filters;

public class HashIdsOperationFilter : IOperationFilter {
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        var hashids = context
            .ApiDescription
            .ParameterDescriptions
            .Where(x => x?.ModelMetadata?.BinderType == typeof(HashidsModelBinder))
            .ToDictionary(d => d.Name, d => d);

        foreach (var parameter in operation.Parameters) {
            if (hashids.TryGetValue(parameter.Name, out var apiParameter)) {
                
            }
        }

        foreach (var schema in context.SchemaRepository.Schemas.Values) {
            foreach (var property in schema.Properties) {
                if (hashids.TryGetValue(property.Key, out var apiParameter)) {
                   
                }
            }
        }
    }
}