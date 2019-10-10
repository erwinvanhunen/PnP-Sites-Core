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
    internal class BasePermissionsConverter : JsonConverter<BasePermissions>
    {
        public override BasePermissions ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, BasePermissions existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new BasePermissions();
            }
            var basePermissionsArray = serializer.Deserialize<JArray>(reader);
            foreach (var basePermissionsString in basePermissionsArray.Values<string>())
            {
                if (Enum.TryParse<PermissionKind>(basePermissionsString, out PermissionKind permissionKind))
                {
                    existingValue.Set(permissionKind);
                }
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, BasePermissions value, JsonSerializer serializer)
        {
            List<String> permissions = new List<String>();

            BasePermissions basePermissions =
                value as BasePermissions;
            if (basePermissions != null)
            {
                
                foreach (var pk in (PermissionKind[])Enum.GetValues(typeof(PermissionKind)))
                {
                    if (basePermissions.Has(pk) && pk != PermissionKind.EmptyMask)
                    {
                        permissions.Add(pk.ToString());
                    }
                }

            }
            serializer.Serialize(writer, permissions);
        }
    }
}
