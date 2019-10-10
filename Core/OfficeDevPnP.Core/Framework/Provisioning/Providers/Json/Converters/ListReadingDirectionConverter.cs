using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class ListReadingDirectionConverter : JsonConverter<ListReadingDirection>
    {
        public override ListReadingDirection ReadJson(JsonReader reader, Type objectType, ListReadingDirection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            return (ListReadingDirection)Enum.Parse(typeof(ListReadingDirection), value);
        }

        public override void WriteJson(JsonWriter writer, ListReadingDirection value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
