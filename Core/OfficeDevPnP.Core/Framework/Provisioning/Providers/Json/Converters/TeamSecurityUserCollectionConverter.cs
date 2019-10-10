using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Model.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class TeamSecurityUserCollectionConverter : JsonConverter<TeamSecurityUserCollection>
    {
        public override TeamSecurityUserCollection ReadJson(JsonReader reader, Type objectType, TeamSecurityUserCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new TeamSecurityUserCollection(null);
            }

            var values = serializer.Deserialize<string[]>(reader);
            if (values != null && values.Length > 0)
            {
                foreach (var value in values)
                {
                    existingValue.Add(new TeamSecurityUser() { UserPrincipalName = value });
                }
                return existingValue;
            }
            throw new Exception("Cannot unmarshal type TeamSecurityUser");
        }

        public override void WriteJson(JsonWriter writer, TeamSecurityUserCollection value, JsonSerializer serializer)
        {
            List<string> values = new List<string>();
            foreach (var user in value)
            {
                values.Add(user.UserPrincipalName);
            }
            serializer.Serialize(writer, values);
        }
    }
}
