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
    public class SiteCollectionCollectionConverter : JsonConverter<SiteCollectionCollection>
    {
        public override SiteCollectionCollection ReadJson(JsonReader reader, Type objectType, SiteCollectionCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new SiteCollectionCollection(null);
            }
            var sitesCollections = serializer.Deserialize<JArray>(reader);
            foreach (var siteCollection in sitesCollections)
            {
                switch (siteCollection.Value<string>("type"))
                {
                    case "CommunicationSite":
                        {
                            var site = JsonConvert.DeserializeObject<CommunicationSiteCollection>(siteCollection.ToString());
                            existingValue.Add(site);
                            break;
                        }
                    case "TeamSite":
                        {
                            var site = JsonConvert.DeserializeObject<TeamSiteCollection>(siteCollection.ToString());
                            existingValue.Add(site);
                            break;
                        }
                    case "TeamSiteNoGroup":
                        {
                            var site = JsonConvert.DeserializeObject<TeamNoGroupSiteCollection>(siteCollection.ToString());
                            existingValue.Add(site);
                            break;
                        }
                }
            }

            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, SiteCollectionCollection value, JsonSerializer serializer)
        {
            if (value.Count > 0)
            {
                writer.WriteStartArray();
                foreach (var site in value)
                {
                    switch (site)
                    {
                        case CommunicationSiteCollection cs:
                            serializer.Serialize(writer, cs);
                            break;
                        case TeamSiteCollection ts:


                            serializer.Serialize(writer, ts);
                            break;
                        case TeamNoGroupSiteCollection tngs:
                            serializer.Serialize(writer, tngs);
                            break;
                    }
                }
                writer.WriteEndArray();
            }
        }
    }
}
