using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    public class ProvisioningHierarchyObjectCollectionConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            var baseType = typeToConvert.BaseType;
            if (baseType != null && baseType.Name != null)
            {
                return baseType.Name == typeof(BaseProvisioningHierarchyObjectCollection<>).Name;
            }
            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var keyType = typeToConvert.BaseType.GenericTypeArguments[0];
            var converterType = typeof(BaseProvisioningHierarchyObjectCollection<>).MakeGenericType(keyType);
            return (JsonConverter)Activator.CreateInstance(converterType);
        }
    }
}
