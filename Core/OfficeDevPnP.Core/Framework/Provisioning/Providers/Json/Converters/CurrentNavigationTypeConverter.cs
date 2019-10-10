using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class CurrentNavigationTypeConverter : JsonConverter<CurrentNavigationType>
    {
        public override CurrentNavigationType ReadJson(JsonReader reader, Type objectType, CurrentNavigationType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            return (CurrentNavigationType)Enum.Parse(typeof(CurrentNavigationType), value);

        }

        public override void WriteJson(JsonWriter writer, CurrentNavigationType value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
