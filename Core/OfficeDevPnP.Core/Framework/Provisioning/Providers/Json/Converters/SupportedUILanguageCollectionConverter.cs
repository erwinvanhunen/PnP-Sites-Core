using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class SupportedUILanguageCollectionConverter : JsonConverter<SupportedUILanguageCollection>
    {
        public override SupportedUILanguageCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var collection = new SupportedUILanguageCollection(null);

            var values = JsonSerializer.Deserialize<int[]>(ref reader, options);
            foreach (var value in values)
            {
                collection.Add(new SupportedUILanguage() { LCID = value });
            }
            return collection;
        }

        public override void Write(Utf8JsonWriter writer, SupportedUILanguageCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var v in value)
            {
                writer.WriteNumberValue(v.LCID);
            }
        }
    }
}
