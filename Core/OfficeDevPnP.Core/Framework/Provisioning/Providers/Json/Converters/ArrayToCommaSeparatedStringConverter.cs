﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class ArrayToCommaSeparatedStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var values = new List<string>();
            while(reader.Read())
            {
                values.Add(reader.GetString());
            }
            return string.Join(",", values);
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach(var item in value.Split(new char[] { ',' }))
            {
                writer.WriteStringValue(item);
            }
            writer.WriteEndArray();
        }
    }
}
