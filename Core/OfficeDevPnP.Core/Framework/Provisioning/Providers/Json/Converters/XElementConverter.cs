using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class XElementConverter : JsonConverter<XElement>
    {

        public override XElement ReadJson(JsonReader reader, Type objectType, XElement existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            if(!string.IsNullOrEmpty(value))
            {
                return XElement.Parse($"<XmlDocuments>{value}</XmlDocuments>");
            }
            throw new Exception("Cannot unmarshal type XElement");
        }

        public override void WriteJson(JsonWriter writer, XElement value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
          //  throw new NotImplementedException();
        }
    }
}
