using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class FieldDefaultsConverter : JsonConverter<Dictionary<string, string>>
    {
        public override Dictionary<string, string> ReadJson(JsonReader reader, Type objectType, Dictionary<string, string> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if(existingValue == null)
            {
                existingValue = new Dictionary<string, string>();
            }
            var jObject = serializer.Deserialize<JObject>(reader);
            foreach(var f in jObject)
            {
                existingValue.Add(f.Key, f.Value.ToString());
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, Dictionary<string, string> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var v in value)
            {
                if (!string.IsNullOrEmpty(v.Key) && !string.IsNullOrEmpty(v.Value))
                {
                    writer.WritePropertyName(v.Key);
                    writer.WriteValue(v.Value);
                }
            }
            writer.WriteEndObject();
        }
    }
}
