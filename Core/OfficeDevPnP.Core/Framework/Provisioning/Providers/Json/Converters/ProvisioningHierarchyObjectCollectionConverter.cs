using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class ProvisioningHierarchyObjectCollectionConverter<ElementType> : JsonConverter<BaseProvisioningHierarchyObjectCollection<ElementType>> where ElementType : BaseHierarchyModel
    {
        public override BaseProvisioningHierarchyObjectCollection<ElementType> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var collectionObject = GetInstance(typeof(ElementType));
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            foreach (var arrayItem in jsonElement.EnumerateArray())
            {
                var deserializedObject = JsonSerializer.Deserialize<ElementType>(arrayItem.ToString(), options);
                collectionObject.Add(deserializedObject);
            }
            return collectionObject;
        }

        public override void Write(Utf8JsonWriter writer, BaseProvisioningHierarchyObjectCollection<ElementType> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private BaseProvisioningHierarchyObjectCollection<ElementType> GetInstance(Type elementType)
        {
            var collectionClassName = $"{elementType.Namespace}.{elementType.Name}Collection";
            var type = Type.GetType(collectionClassName);
            return (BaseProvisioningHierarchyObjectCollection<ElementType>)Activator.CreateInstance(type);
        }
    }
}
