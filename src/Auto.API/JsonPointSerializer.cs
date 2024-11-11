using NetTopologySuite.Geometries;
using System.Text.Json;
using System.Text.Json.Serialization;

public class PointJsonConverter : JsonConverter<Point>
{
    // Deserialize JSON to Point
    public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }

        double? latitude = null;
        double? longitude = null;

        // Read properties
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString();
                reader.Read();

                if (propertyName == "latitude")
                {
                    latitude = reader.GetDouble();
                }
                else if (propertyName == "longitude")
                {
                    longitude = reader.GetDouble();
                }
            }
        }

        if (!latitude.HasValue || !longitude.HasValue)
        {
            throw new JsonException("Missing latitude or longitude values.");
        }

        return new Point(longitude.Value, latitude.Value) { SRID = 4326 }; // 4326 is the SRID for WGS84
    }

    // Serialize Point to JSON
    public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("latitude", value.Y); // Y represents latitude
        writer.WriteNumber("longitude", value.X); // X represents longitude
        writer.WriteEndObject();
    }
}