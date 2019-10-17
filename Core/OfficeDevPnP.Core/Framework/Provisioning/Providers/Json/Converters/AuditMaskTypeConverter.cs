using Microsoft.SharePoint.Client;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class AuditMaskTypeConverter : JsonConverter<AuditMaskType>
    {
        public override AuditMaskType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var auditMask = AuditMaskType.None;

            var values = JsonSerializer.Deserialize<string[]>(ref reader, options);
            foreach(var value in values)
            {
                if (Enum.TryParse<AuditMaskType>(value, out AuditMaskType auditMaskType))
                {
                    auditMask |= auditMaskType;
                }
            }
            return auditMask;
        }

        public override void Write(Utf8JsonWriter writer, AuditMaskType value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var pk in (AuditMaskType[])Enum.GetValues(typeof(AuditMaskType)))
            {
                if (value.Has(pk) && pk != AuditMaskType.None)
                {
                    writer.WriteStringValue(pk.ToString());
                }
            }
            writer.WriteEndArray();
        }
    }
}
