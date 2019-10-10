using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class FieldCollectionConverter : JsonConverter<FieldCollection>
    {
        public override FieldCollection ReadJson(JsonReader reader, Type objectType, FieldCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new FieldCollection(null);
            }

            var values = serializer.Deserialize<string[]>(reader);
            if (values != null && values.Length > 0)
            {
                foreach (var value in values)
                {
                    existingValue.Add(new Field() { SchemaXml = value });
                }
                return existingValue;
            }
            throw new Exception("Cannot unmarshal type User");
        }

        public override void WriteJson(JsonWriter writer, FieldCollection value, JsonSerializer serializer)
        {
            var list = new List<string>();
            foreach (var field in value)
            {
                list.Add(field.SchemaXml);
            }
            serializer.Serialize(writer, list);
        }
    }
}
