using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class BasePermissionsConverter : JsonConverter<BasePermissions>
    {
        public override BasePermissions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            BasePermissions bp = new BasePermissions();
            var values = JsonSerializer.Deserialize<string[]>(ref reader, options);
            foreach (var value in values)
            {
                if (Enum.TryParse<PermissionKind>(value, out PermissionKind permissionKind))
                {
                    bp.Set(permissionKind);
                }
            }
            return bp;
        }

        public override void Write(Utf8JsonWriter writer, BasePermissions value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var pk in (PermissionKind[])Enum.GetValues(typeof(PermissionKind)))
            {
                if (value.Has(pk) && pk != PermissionKind.EmptyMask)
                {
                    writer.WriteStringValue(pk.ToString());
                }
            }
            writer.WriteEndArray();
        }
    }
}
