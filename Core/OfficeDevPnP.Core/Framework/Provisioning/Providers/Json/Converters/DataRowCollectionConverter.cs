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
    internal class DataRowCollectionConverter : JsonConverter<DataRowCollection>
    {
        public override DataRowCollection ReadJson(JsonReader reader, Type objectType, DataRowCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new DataRowCollection(null);
            }
            var jObject = serializer.Deserialize<JObject>(reader);

            existingValue.KeyColumn = jObject.Value<string>("keyColumn");
            switch (jObject.Value<string>("updateBehavior"))
            {
                case "overwrite":
                    {
                        existingValue.UpdateBehavior = UpdateBehavior.Overwrite;
                        break;
                    }
                case "skip":
                    {
                        existingValue.UpdateBehavior = UpdateBehavior.Skip;
                        break;
                    }
            }
            var rows = jObject.Value<JArray>("rows");
            foreach (var row in rows)
            {
                var values = row.Value<JObject>("values");
                DataRow dataRow = new DataRow();
                foreach (var value in values)
                {
                    dataRow.Values.Add(value.Key, value.Value.ToString());
                }
                var breakRoleInheritance = row.Value<JObject>("breakRoleInheritance");
                dataRow.Security.ClearSubscopes = breakRoleInheritance.Value<bool>("clearSubScopes");
                dataRow.Security.CopyRoleAssignments = breakRoleInheritance.Value<bool>("copyRoleAssignments");
                var roleAssignments = breakRoleInheritance.Value<JArray>("roleAssignments");
                if (roleAssignments != null)
                {
                    foreach (var roleAssignment in roleAssignments)
                    {
                        dataRow.Security.RoleAssignments.Add(new RoleAssignment() { Principal = roleAssignment.Value<string>("principal"), RoleDefinition = roleAssignment.Value<string>("roleDefinition"), Remove = roleAssignment.Value<bool>("remove") });
                    }
                }
                var attachments = row.Value<JArray>("attachments");
                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        dataRow.Attachments.Add(new Model.SharePoint.InformationArchitecture.DataRowAttachment() { Name = attachment.Value<string>("name"), Src = attachment.Value<string>("src"), Overwrite = attachment.Value<bool>("overwrite") });
                    }
                }
                existingValue.Add(dataRow);
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, DataRowCollection value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            if (!string.IsNullOrEmpty(value.KeyColumn))
            {
                writer.WritePropertyName("keyColumn");
                writer.WriteValue(value.KeyColumn);
            }
            writer.WritePropertyName("updateBehavior");
            writer.WriteValue(value.UpdateBehavior.ToString());
            writer.WritePropertyName("rows");
            writer.WriteStartArray();
            foreach (var row in value)
            {
                writer.WriteStartObject();

                if (row.Values != null && row.Values.Count > 0)
                {
                    writer.WritePropertyName("values");
                    writer.WriteStartObject();
                    foreach (var rowValue in row.Values)
                    {
                        writer.WritePropertyName(rowValue.Key);
                        writer.WriteValue(rowValue.Value);
                    }
                    writer.WriteEndObject();
                }
                if (row.Security != null)
                {
                    writer.WritePropertyName("breakRoleInheritance");
                    writer.WriteRawValue(JsonConvert.SerializeObject(row.Security));
                }
                if (row.Attachments != null && row.Attachments.Count > 0)
                {
                    writer.WritePropertyName("attachments");
                    writer.WriteRawValue(JsonConvert.SerializeObject(row.Attachments));
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
