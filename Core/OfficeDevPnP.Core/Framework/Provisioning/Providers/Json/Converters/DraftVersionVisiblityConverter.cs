using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class DraftVersionVisibilityConverter : JsonConverter<int>
    {
        public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Approver":
                    return 2;
                case "Author":
                    return 1;
                case "Reader":
                    return 0;
            }
            throw new Exception("Cannot unmarshal type DraftVersionVisibility");
        }

        public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer)
        {
            switch (value)
            {
                case 2:
                    serializer.Serialize(writer, "Approver");
                    return;
                case 1:
                    serializer.Serialize(writer, "Author");
                    return;
                case 0:
                    serializer.Serialize(writer, "Reader");
                    return;
            }
            throw new Exception("Cannot marshal type DraftVersionVisibility");
        }
    }
}
