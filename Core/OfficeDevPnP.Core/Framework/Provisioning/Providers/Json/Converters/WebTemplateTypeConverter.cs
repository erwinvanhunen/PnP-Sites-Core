using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class SiteDesignWebTemplateConverter : JsonConverter<SiteDesignWebTemplate>
    {
        public override SiteDesignWebTemplate ReadJson(JsonReader reader, Type objectType, SiteDesignWebTemplate existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            return (SiteDesignWebTemplate)Enum.Parse(typeof(SiteDesignWebTemplate), value);
        }

        public override void WriteJson(JsonWriter writer, SiteDesignWebTemplate value, JsonSerializer serializer)
        {
            if((int)value == 1)
            {
                serializer.Serialize(writer,"CommunicationSite");
            } else
            {
                serializer.Serialize(writer,"TeamSite");
            }
        }
    }
}
