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
    internal class SiteDesignRightConverter : JsonConverter<SiteDesignRight>
    {
        public override SiteDesignRight Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return (SiteDesignRight)Enum.Parse(typeof(SiteDesignRight), value);
        }

        public override void Write(Utf8JsonWriter writer, SiteDesignRight value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
