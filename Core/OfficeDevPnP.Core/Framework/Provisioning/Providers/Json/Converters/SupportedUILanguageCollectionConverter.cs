using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class SupportedUILanguageCollectionConverter : JsonConverter<SupportedUILanguageCollection>
    {
        public override SupportedUILanguageCollection ReadJson(JsonReader reader, Type objectType, SupportedUILanguageCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new SupportedUILanguageCollection(null);
            }

            var values = serializer.Deserialize<int[]>(reader);
            if (values != null && values.Length > 0)
            {
                foreach (var value in values)
                {
                    existingValue.Add(new SupportedUILanguage() { LCID = value });
                }
                return existingValue;
            }
            throw new Exception("Cannot unmarshal type User");
        }

        public override void WriteJson(JsonWriter writer, SupportedUILanguageCollection value, JsonSerializer serializer)
        {
            var list = new List<int>();
            foreach (var field in value)
            {
                list.Add(field.LCID);
            }
            serializer.Serialize(writer, list);
        }
    }
}
