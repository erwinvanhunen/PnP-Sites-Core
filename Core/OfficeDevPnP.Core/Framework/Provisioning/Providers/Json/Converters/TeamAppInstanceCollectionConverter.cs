using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class TeamAppInstanceCollectionConverter : JsonConverter<TeamAppInstanceCollection>
    {
        public override TeamAppInstanceCollection ReadJson(JsonReader reader, Type objectType, TeamAppInstanceCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new TeamAppInstanceCollection(null);
            }

            if (reader.TokenType == JsonToken.Null) return null;
            var values = serializer.Deserialize<string[]>(reader);
            if (values != null && values.Length > 0)
            {
                foreach (var value in values)
                {
                    existingValue.Add(new TeamAppInstance() { AppId = value });
                }
                return existingValue;
            }
            throw new Exception("Cannot unmarshal type TeamAppInstanceCollection");
        }

        public override void WriteJson(JsonWriter writer, TeamAppInstanceCollection value, JsonSerializer serializer)
        {
            var appIds = new List<string>();
            foreach(var app in value)
            {
                appIds.Add(app.AppId);
            }
            serializer.Serialize(writer, appIds);
        }
    }
}
