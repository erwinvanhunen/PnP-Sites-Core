using OfficeDevPnP.Core.Framework.Provisioning.Model.Teams;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class TeamAppInstanceCollectionConverter : JsonConverter<TeamAppInstanceCollection>
    {
        public override TeamAppInstanceCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var appCollection = new TeamAppInstanceCollection(null);
            var values = JsonSerializer.Deserialize<string[]>(ref reader);
            if (values != null && values.Length > 0)
            {
                foreach (var value in values)
                {
                    appCollection.Add(new TeamAppInstance() { AppId = value });
                }
            }
            return appCollection;
        }

        public override void Write(Utf8JsonWriter writer, TeamAppInstanceCollection value, JsonSerializerOptions options)
        {
            var appIds = new List<string>();
            foreach (var app in value)
            {
                appIds.Add(app.AppId);
            }
            JsonSerializer.Serialize(writer, appIds);
        }
    }
}
