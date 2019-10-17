using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class XElementConverter : JsonConverter<XElement>
    {
        public override XElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (!value.ToLower().StartsWith("<xmldocuments>") && !value.ToLower().EndsWith("</xmldocuments>"))
            {
                return XElement.Parse($"<XmlDocuments>{reader.GetString()}</XmlDocuments>");
            } else
            {
                return XElement.Parse(reader.GetString());
            }
        }

        public override void Write(Utf8JsonWriter writer, XElement value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
