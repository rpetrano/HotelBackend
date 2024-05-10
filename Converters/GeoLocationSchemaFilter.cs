using Swashbuckle.AspNetCore.SwaggerGen;
using NetTopologySuite.Geometries;
using Microsoft.OpenApi.Models;

public class GeoLocationSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(Point))
        {
            schema.Properties = new Dictionary<string, OpenApiSchema>
            {
                ["lat"] = new OpenApiSchema() { Type = "number", Format = "double" },
                ["lon"] = new OpenApiSchema() { Type = "number", Format = "double" }
            };
        }
    }
}