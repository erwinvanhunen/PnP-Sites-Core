using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class AuditMaskTypeConverter : JsonConverter<AuditMaskType>
    {
        public override AuditMaskType ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, AuditMaskType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var auditMaskArray = serializer.Deserialize<JArray>(reader);
            foreach (var auditMaskString in auditMaskArray.Values<string>())
            {
                if (Enum.TryParse<AuditMaskType>(auditMaskString, out AuditMaskType auditMaskType))
                {
                    existingValue |= auditMaskType;
                }
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, AuditMaskType value, JsonSerializer serializer)
        {
            List<String> types = new List<String>();

            foreach (var pk in (AuditMaskType[])Enum.GetValues(typeof(AuditMaskType)))
            {
                if (value.Has(pk) && pk != AuditMaskType.None)
                {
                    types.Add(pk.ToString());
                }
            }

            serializer.Serialize(writer, types);
        }
    }
}
