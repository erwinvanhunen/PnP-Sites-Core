﻿using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class UserCollectionConverter : JsonConverter<UserCollection>
    {
        public override UserCollection ReadJson(JsonReader reader, Type objectType, UserCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new UserCollection(null);
            }

            if (reader.TokenType == JsonToken.Null) return null;
            var values = serializer.Deserialize<string[]>(reader);
            if (values != null && values.Length > 0)
            {
                foreach (var value in values)
                {
                    existingValue.Add(new User() { Name = value });
                }
                return existingValue;
            }
            throw new Exception("Cannot unmarshal type User");
        }

        public override void WriteJson(JsonWriter writer, UserCollection value, JsonSerializer serializer)
        {
            List<string> values = new List<string>();
            foreach (var user in value)
            {
                values.Add(user.Name);
            }
            serializer.Serialize(writer, values);
        }
    }
}
