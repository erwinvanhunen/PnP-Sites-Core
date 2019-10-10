using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class GlobalNavigationTypeConverter : JsonConverter<GlobalNavigationType>
    {
        public override GlobalNavigationType ReadJson(JsonReader reader, Type objectType, GlobalNavigationType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            return (GlobalNavigationType)Enum.Parse(typeof(GlobalNavigationType), value);

        }

        public override void WriteJson(JsonWriter writer, GlobalNavigationType value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
