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
    internal class SiteCollectionCollectionConverter : JsonConverter<SiteCollectionCollection>
    {
        public override SiteCollectionCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var collection = new SiteCollectionCollection(null);
            var sitesCollections = JsonSerializer.Deserialize<JsonElement[]>(ref reader);
            foreach (var siteCollection in sitesCollections)
            {
                switch (siteCollection.GetProperty("type").GetString())
                {
                    case "CommunicationSite":
                        {
                            var site = JsonSerializer.Deserialize<CommunicationSiteCollection>(siteCollection.ToString(), options);
                            collection.Add(site);
                            break;
                        }
                    case "TeamSite":
                        {
                            var site = JsonSerializer.Deserialize<TeamSiteCollection>(siteCollection.ToString(), options);
                            collection.Add(site);
                            break;
                        }
                    case "TeamSiteNoGroup":
                        {
                            var site = JsonSerializer.Deserialize<TeamNoGroupSiteCollection>(siteCollection.ToString(), options);
                            collection.Add(site);
                            break;
                        }
                }
            }

            return collection;
        }


        public override void Write(Utf8JsonWriter writer, SiteCollectionCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var site in value)
            {
                switch (site)
                {
                    case CommunicationSiteCollection cs:
                        WriteSiteObject(writer, options, cs, "CommunicationSite");
                        //JsonSerializer.Serialize(writer, cs, options);
                        break;
                    case TeamSiteCollection ts:
                        WriteSiteObject(writer, options, ts, "TeamSite");
                        //JsonSerializer.Serialize(writer, ts, options);
                        break;
                    case TeamNoGroupSiteCollection tngs:
                        WriteSiteObject(writer, options, tngs, "TeamSiteNoGroup");
                        //JsonSerializer.Serialize(writer, tngs, options);
                        break;
                }
            }
            writer.WriteEndArray();
        }

        private void WriteSiteObject<T>(Utf8JsonWriter writer, JsonSerializerOptions options, T mySite, string siteTypeValue) where T : SiteCollection
        {
            var jsonString = JsonSerializer.Serialize<T>(mySite, options);
            var jsonDocument = JsonDocument.Parse(jsonString);
            writer.WriteStartObject();
            writer.WriteString("type", siteTypeValue);
            foreach (var property in jsonDocument.RootElement.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.Array)
                {
                    if (property.Value.GetArrayLength() > 0)
                    {
                        property.WriteTo(writer);
                    }
                }
                else
                {
                    property.WriteTo(writer);
                }
            }
            writer.WriteEndObject();
        }
    }
}
