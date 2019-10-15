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
    internal class ProvisioningHierarchyObjectCollectionConverter<ElementType, CollectionType> : JsonConverter<CollectionType> where ElementType : BaseHierarchyModel where CollectionType : BaseProvisioningHierarchyObjectCollection<ElementType>
    {
        public override CollectionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var collection = GetInstance();
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader);
            foreach (var arrayItem in jsonElement.EnumerateArray())
            {
                var deserializedObject = JsonSerializer.Deserialize<ElementType>(arrayItem.ToString(),options);
                collection.Add(deserializedObject);
            }
            return collection;
        }

        public override void Write(Utf8JsonWriter writer, CollectionType value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value,options);
        }

        private CollectionType GetInstance()
        {
            return (CollectionType)Activator.CreateInstance(typeof(CollectionType));
        }
    }
}
