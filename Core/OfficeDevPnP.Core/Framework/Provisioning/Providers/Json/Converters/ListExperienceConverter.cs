using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListExperience = Microsoft.SharePoint.Client.ListExperience;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class ListExperienceConverter : JsonConverter<ListExperience>
    {
        public override ListExperience ReadJson(JsonReader reader, Type objectType, ListExperience existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            return (ListExperience)Enum.Parse(typeof(ListExperience), value);
        }

        public override void WriteJson(JsonWriter writer, ListExperience value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
