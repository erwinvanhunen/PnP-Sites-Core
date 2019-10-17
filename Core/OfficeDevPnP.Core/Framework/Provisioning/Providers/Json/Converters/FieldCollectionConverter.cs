using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class FieldCollectionConverter : JsonConverter<FieldCollection>
    {
        public override FieldCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var fieldCollection = new FieldCollection(null);
            var values = JsonSerializer.Deserialize<string[]>(ref reader, options);
            foreach (var value in values)
            {
                fieldCollection.Add(new Field()
                {
                    SchemaXml = value
                });
            }
            return fieldCollection;
        }

        public override void Write(Utf8JsonWriter writer, FieldCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var field in value)
            {
                writer.WriteStringValue(field.SchemaXml);
            }
            writer.WriteEndArray();
        }
    }
}
