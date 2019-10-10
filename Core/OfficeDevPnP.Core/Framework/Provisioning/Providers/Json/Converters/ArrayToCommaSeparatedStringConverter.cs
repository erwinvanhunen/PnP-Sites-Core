using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class ArrayToCommaSeparatedStringConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var values = serializer.Deserialize<string[]>(reader);
            return String.Join(",", values);
        }

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.Split(new char[] { ',' }));
        }
    }
}
