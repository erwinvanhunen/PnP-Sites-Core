using OfficeDevPnP.Core.Framework.Provisioning.Model.Teams;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class TeamSecurityUserCollectionConverter : JsonConverter<TeamSecurityUserCollection>
    {
        public override TeamSecurityUserCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var userCollection = new TeamSecurityUserCollection(null);

            var values = JsonSerializer.Deserialize<string[]>(ref reader, options);
            foreach (var value in values)
            {
                userCollection.Add(new TeamSecurityUser() { UserPrincipalName = value });
            }
            return userCollection;
        }

        public override void Write(Utf8JsonWriter writer, TeamSecurityUserCollection value, JsonSerializerOptions options)
        {
            List<string> values = new List<string>();
            foreach (var user in value)
            {
                values.Add(user.UserPrincipalName);
            }
            JsonSerializer.Serialize(writer, values);
        }

    }
}
