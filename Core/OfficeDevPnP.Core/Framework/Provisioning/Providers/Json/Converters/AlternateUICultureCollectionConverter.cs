using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class AlternateUICultureCollectionConverter : JsonConverter<AlternateUICultureCollection>
    {
        public override AlternateUICultureCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var collection = new AlternateUICultureCollection(null);

            var values = JsonSerializer.Deserialize<int[]>(ref reader, options);
            foreach (var value in values)
            {
                collection.Add(new AlternateUICulture() { LCID = value });
            }
            return collection;
        }

        public override void Write(Utf8JsonWriter writer, AlternateUICultureCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var languages in value)
            {
                writer.WriteNumberValue(languages.LCID);
            }
            writer.WriteEndArray();
        }
    }
}
