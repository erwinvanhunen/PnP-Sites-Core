using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class JsonToStringConverter : JsonConverter<string>
    {
        public override string ReadJson(JsonReader reader, Type objectType, string existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jsonData = serializer.Deserialize(reader);
            return jsonData.ToString();
        }

        public override void WriteJson(JsonWriter writer, string value, JsonSerializer serializer)
        {
            var o = JsonConvert.DeserializeObject(value);
            serializer.Serialize(writer, o);
        }
    }
}
