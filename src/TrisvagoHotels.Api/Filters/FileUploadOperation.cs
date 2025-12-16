using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TrisvagoHotels.Api.Filters;

public class FileUploadOperation : IOperationFilter {
    [Consumes("multipart/form-data")]
    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        if (operation.OperationId.ToLower().Contains("upload")) {
            operation.Parameters.Clear();
            operation.Parameters.Add(new OpenApiParameter {
                Name = "uploadedFile",
                In = ParameterLocation.Header,
                Description = "Upload File",
                Required = true,
                Schema = new OpenApiSchema {
                    Type = JsonSchemaType.Object
                }
            });
            operation.Parameters.Add(new OpenApiParameter {
                Name = "userId",
                In = ParameterLocation.Query,
                Description = "",
                Required = true,
                Schema = new OpenApiSchema {
                    Type = JsonSchemaType.Number
                }
            });
        }
    }
}