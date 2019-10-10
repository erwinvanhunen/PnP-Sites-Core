using Newtonsoft.Json;
using OfficeDevPnP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class StringToSecureStringConverter : JsonConverter<SecureString>
    {
        public override SecureString ReadJson(JsonReader reader, Type objectType, SecureString existingValue, bool hasExistingValue, JsonSerializer serializer)
        {

            var value = serializer.Deserialize<string>(reader);
            return EncryptionUtility.ToSecureString(value);
        }

        public override void WriteJson(JsonWriter writer, SecureString value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, EncryptionUtility.ToInsecureString(value));
        }
    }
}
