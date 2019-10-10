using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class SubSiteConverter : JsonConverter<SubSite>
    {
        public override SubSite ReadJson(JsonReader reader, Type objectType, SubSite existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var siteObject = serializer.Deserialize<JObject>(reader);
            return ParseSite(siteObject);
        }

        private TeamNoGroupSubSite ParseSite(JToken siteObject)
        {
            var site = new TeamNoGroupSubSite();

            site.Description = siteObject.Value<string>("description");
            site.Language = siteObject.Value<int>("language");
            site.ProvisioningId = siteObject.Value<string>("provisioningId");
            site.Theme = siteObject.Value<string>("theme");
            site.TimeZoneId = siteObject.Value<int>("timeZoneId");
            site.Title = siteObject.Value<string>("title");
            site.Url = siteObject.Value<string>("url");
            site.UseSamePermissionsAsParentSite = siteObject.Value<bool>("useSamePermissionsAsParentSite");
            site.QuickLaunchEnabled = siteObject.Value<bool>("quickLaunchEnabled");
            var templates = siteObject.Value<JArray>("templates");
            if (templates != null)
            {
                foreach (var template in templates)
                {
                    site.Templates.Add(template.Value<string>());
                }
            }
            var subsites = siteObject.Value<JArray>("sites");
            if (subsites != null && subsites.Count > 0)
            {
                foreach (var subsite in subsites)
                {
                    site.Sites.Add(ParseSite(subsite));
                }
            }

            return site;
        }

        public override void WriteJson(JsonWriter writer, SubSite value, JsonSerializer serializer)
        {
            var site = (TeamNoGroupSubSite)value;
            SerializeSubSite(writer, site);
        }

        private void SerializeSubSite(JsonWriter writer, TeamNoGroupSubSite site)
        {
            writer.WriteStartObject();
            if (!string.IsNullOrEmpty(site.Description))
            {
                writer.WritePropertyName("description");
                writer.WriteValue(site.Description);
            }
            if (site.Language > 0)
            {
                writer.WritePropertyName("language");
                writer.WriteValue(site.Language);
            }
            if (!string.IsNullOrEmpty(site.ProvisioningId))
            {
                writer.WritePropertyName("provisioningId");
                writer.WriteValue(site.ProvisioningId);
            }
            if (!string.IsNullOrEmpty(site.Theme))
            {
                writer.WritePropertyName("theme");
                writer.WriteValue(site.Theme);
            }
            if (site.TimeZoneId > 0)
            {
                writer.WritePropertyName("timeZoneId");
                writer.WriteValue(site.TimeZoneId);
            }
            if (!string.IsNullOrEmpty(site.Title))
            {
                writer.WritePropertyName("title");
                writer.WriteValue(site.Title);
            }
            if (!string.IsNullOrEmpty(site.Url))
            {
                writer.WritePropertyName("url");
                writer.WriteValue(site.Url);
            }
            writer.WritePropertyName("useSamePermissionsAsParentSite");
            writer.WriteValue(site.UseSamePermissionsAsParentSite);
            writer.WritePropertyName("quickLaunchEnabled");
            writer.WriteValue(site.QuickLaunchEnabled);
            if (site.Templates != null && site.Templates.Count > 0)
            {
                writer.WritePropertyName("templates");
                writer.WriteStartArray();
                foreach (var template in site.Templates)
                {
                    writer.WriteValue(template);
                }
                writer.WriteEndArray();
            }
            if (site.Sites != null && site.Sites.Count > 0)
            {
                writer.WritePropertyName("sites");
                writer.WriteStartArray();
                foreach (var subsite in site.Sites)
                {
                    var subTeamSite = (TeamNoGroupSubSite)subsite;
                    SerializeSubSite(writer, subTeamSite);
                }
                writer.WriteEnd();
            }
            writer.WriteEndObject();
        }
    }
}
