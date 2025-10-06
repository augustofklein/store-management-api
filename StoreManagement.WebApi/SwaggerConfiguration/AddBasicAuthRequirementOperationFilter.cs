using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StoreManagement.WebApi.SwaggerConfiguration;

public class RequireBasicAuthAttribute : Attribute { }

public class AddBasicAuthRequirementOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAttribute = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<RequireBasicAuthAttribute>()
            .Any();

        if (!hasAttribute)
            return;

        operation.Security ??= new List<OpenApiSecurityRequirement>();

        var scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basicAuth" }
        };

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            [ scheme ] = new string[] { } // no scopes for basic
        });
    }
}