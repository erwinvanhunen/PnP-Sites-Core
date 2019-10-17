using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class SiteDesignWebTemplateConverter : JsonConverter<SiteDesignWebTemplate>
    {
        public override SiteDesignWebTemplate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return (SiteDesignWebTemplate)Enum.Parse(typeof(SiteDesignWebTemplate), value);
        }

        public override void Write(Utf8JsonWriter writer, SiteDesignWebTemplate value, JsonSerializerOptions options)
        {
            if ((int)value == 1)
            {
                writer.WriteStringValue("CommunicationSite");
            }
            else
            {
                writer.WriteStringValue("TeamSite");
            }
        }
    }
}
