using MiTutor.Models.GestionUsuarios;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class UserGenericConverter : JsonConverter<UserGeneric>
{
    public override UserGeneric Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;
            string type = root.GetProperty("Type").GetString();

            switch (type)
            {
                case "MANAGER":
                    return JsonSerializer.Deserialize<UserGenericManager>(root.GetRawText(), options);
                case "TUTOR":
                    return JsonSerializer.Deserialize<UserGenericTutor>(root.GetRawText(), options);
                case "STUDENT":
                    return JsonSerializer.Deserialize<UserStudent>(root.GetRawText(), options);
                default:
                    throw new JsonException($"Unknown type: {type}");
            }
        }
    }

    public override void Write(Utf8JsonWriter writer, UserGeneric value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}