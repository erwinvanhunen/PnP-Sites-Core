using Newtonsoft.Json;
using System;
using UserCustomActionRegistrationType = Microsoft.SharePoint.Client.UserCustomActionRegistrationType;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class UserCustomActionRegistrationTypeConverter : JsonConverter<UserCustomActionRegistrationType>
    {
        public override UserCustomActionRegistrationType ReadJson(JsonReader reader, Type objectType, UserCustomActionRegistrationType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            return (UserCustomActionRegistrationType)Enum.Parse(typeof(UserCustomActionRegistrationType), value);
        }

        public override void WriteJson(JsonWriter writer, UserCustomActionRegistrationType value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }
    }
}
