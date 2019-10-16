using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    public class ProvisioningTemplateObjectCollectionConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            var baseType = typeToConvert.BaseType;
            if (baseType != null && baseType.Name != null)
            {
                return baseType.Name == typeof(BaseProvisioningTemplateObjectCollection<>).Name;
            }
            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var keyType = typeToConvert.BaseType.GenericTypeArguments[0];
            var converterType = typeof(ProvisioningTemplateObjectCollectionConverter<>).MakeGenericType(keyType);
            return (JsonConverter)Activator.CreateInstance(converterType);
        }
    }
}
