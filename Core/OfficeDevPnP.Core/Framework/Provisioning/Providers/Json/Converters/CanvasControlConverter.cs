using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class CanvasControlConverter : JsonConverter<CanvasControl>
    {
        public override CanvasControl ReadJson(JsonReader reader, Type objectType, CanvasControl existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new CanvasControl();
            }
            var jObject = serializer.Deserialize<JObject>(reader);

            existingValue.Type = (WebPartType)Enum.Parse(typeof(WebPartType), jObject.Value<string>("type"));
            existingValue.CustomWebPartName = jObject.Value<string>("customWebPartName");
            existingValue.JsonControlData = jObject.Value<string>("controlData");
            existingValue.ControlId = Guid.Parse(jObject.Value<string>("controlId"));
            existingValue.Order = jObject.Value<int>("order");
            existingValue.Column = jObject.Value<int>("column");
            var properties = jObject.Value<JObject>("controlProperties");
            if (properties != null && properties.Count > 0)
            {
                existingValue.ControlProperties = new Dictionary<string, string>();
                foreach (var property in properties)
                {
                    existingValue.ControlProperties.Add(property.Key, property.Value.ToString());
                }
            }

            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, CanvasControl value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue(value.Type.ToString());
            if (!string.IsNullOrEmpty(value.CustomWebPartName))
            {
                writer.WritePropertyName("customWebPartName");
                writer.WriteValue(value.CustomWebPartName);
            }
            if (!string.IsNullOrEmpty(value.JsonControlData) && value.JsonControlData != "{}")
            {
                writer.WritePropertyName("controlData");
                writer.WriteRawValue(value.JsonControlData);
            }
            if (value.ControlId != Guid.Empty)
            {
                writer.WritePropertyName("controlId");
                writer.WriteValue(value.ControlId.ToString());
            }
            writer.WritePropertyName("order");
            writer.WriteValue(value.Order);
            writer.WritePropertyName("column");
            writer.WriteValue(value.Column);
            if (value.ControlProperties != null && value.ControlProperties.Count > 0)
            {
                writer.WritePropertyName("controlProperties");
                writer.WriteStartObject();
                foreach (var property in value.ControlProperties)
                {
                    writer.WritePropertyName(property.Key);
                    writer.WriteValue(property.Value);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
