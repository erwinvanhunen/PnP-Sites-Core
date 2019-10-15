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
    internal class Bla : JsonConverter<BaseProvisioningTemplateObjectCollection<BaseModel>>
    {
        public override BaseProvisioningTemplateObjectCollection<BaseModel> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, BaseProvisioningTemplateObjectCollection<BaseModel> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

    internal class ProvisioningTemplateObjectCollectionConverter<ElementType, CollectionType> : JsonConverter<CollectionType> where ElementType : BaseModel where CollectionType : BaseProvisioningTemplateObjectCollection<ElementType>
    {
        public override CollectionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var collection = GetInstance();
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            foreach (var arrayItem in jsonElement.EnumerateArray())
            {
                var deserializedObject = JsonSerializer.Deserialize<ElementType>(arrayItem.ToString(), options);
                collection.Add(deserializedObject);
            }
            return collection;
        }

        public override void Write(Utf8JsonWriter writer, CollectionType value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private CollectionType GetInstance()
        {
            return (CollectionType)Activator.CreateInstance(typeof(CollectionType));
        }
    }
}
