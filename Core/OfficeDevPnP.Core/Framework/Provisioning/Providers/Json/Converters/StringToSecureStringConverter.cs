using OfficeDevPnP.Core.Utilities;
using System;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class StringToSecureStringConverter : JsonConverter<SecureString>
    {
        public override SecureString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return EncryptionUtility.ToSecureString(value);
        }

        public override void Write(Utf8JsonWriter writer, SecureString value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(EncryptionUtility.ToInsecureString(value));
        }
    }
}
