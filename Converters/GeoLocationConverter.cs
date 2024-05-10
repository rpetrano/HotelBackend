using System.Text.Json;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

public class GeoLocationConverter : JsonConverter<Point>
{
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected a JSON object for Point deserialization."); 
        }

        double lon = 0, lat = 0;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject) 
            {
                return new Point(lon, lat); // Construct your Point
            } 

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException("Expected a property name.");
            }

            string propertyName = reader.GetString();
            reader.Read(); // Move to the value

            if (propertyName.Equals("lon", StringComparison.OrdinalIgnoreCase))
            {
                lon = reader.GetDouble(); 
            }
            else if (propertyName.Equals("lat", StringComparison.OrdinalIgnoreCase))
            {
                lat = reader.GetDouble();
            }
            else
            {
                reader.Skip(); // Ignore unknown properties 
            }
        }

        throw new JsonException("Unexpected end of JSON."); 
    }

    public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("lon");
        writer.WriteNumberValue(value.X);
        writer.WritePropertyName("lat");
        writer.WriteNumberValue(value.Y);
        writer.WriteEndObject();
    }
}
