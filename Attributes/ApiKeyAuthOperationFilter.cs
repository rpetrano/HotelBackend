using HotelBackend.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class ApiKeyAuthOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            var hasApiKeyAuthAttribute = controllerActionDescriptor.MethodInfo
                .GetCustomAttributes(inherit: true)
                .OfType<ApiKeyAuthAttribute>()
                .Any();

            if (hasApiKeyAuthAttribute)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [ new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" } } ] = new string[] { }
                    }
                };
            }
        }
    }
}
