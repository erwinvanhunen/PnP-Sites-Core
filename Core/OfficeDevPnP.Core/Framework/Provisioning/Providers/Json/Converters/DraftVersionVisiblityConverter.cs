using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class DraftVersionVisibilityConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "Approver":
                    return 2;
                case "Author":
                    return 1;
                case "Reader":
                    return 0;
            }
            throw new Exception("Cannot unmarshal type DraftVersionVisibility");
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case 2:
                    writer.WriteStringValue("Approver");
                    return;
                case 1:
                    writer.WriteStringValue("Author");
                    return;
                case 0:
                   writer.WriteStringValue("Reader");
                    return;
            }
        }
    }
}
