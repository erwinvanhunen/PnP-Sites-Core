using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class AlternateUICultureCollectionConverter : JsonConverter<AlternateUICultureCollection>
    {
        public override AlternateUICultureCollection ReadJson(JsonReader reader, Type objectType, AlternateUICultureCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new AlternateUICultureCollection(null);
            }

            var values = serializer.Deserialize<int[]>(reader);
            if (values != null && values.Length > 0)
            {
                foreach (var value in values)
                {
                    existingValue.Add(new AlternateUICulture() { LCID = value });
                }
                return existingValue;
            }
            throw new Exception("Cannot unmarshal type User");
        }

        public override void WriteJson(JsonWriter writer, AlternateUICultureCollection value, JsonSerializer serializer)
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
