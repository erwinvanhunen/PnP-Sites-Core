﻿using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class ProvisioningTemplateObjectCollectionConverter<ElementType> : JsonConverter<BaseProvisioningTemplateObjectCollection<ElementType>> where ElementType : BaseModel
    {
        public override BaseProvisioningTemplateObjectCollection<ElementType> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

        public override void Write(Utf8JsonWriter writer, BaseProvisioningTemplateObjectCollection<ElementType> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private BaseProvisioningTemplateObjectCollection<ElementType> GetInstance(Type elementType)
        {
            var collectionClassName = $"{elementType.Namespace}.{elementType.Name}Collection";
            var type = Type.GetType(collectionClassName);
            return (BaseProvisioningTemplateObjectCollection<ElementType>) Activator.CreateInstance(type);
        }
    }
}
