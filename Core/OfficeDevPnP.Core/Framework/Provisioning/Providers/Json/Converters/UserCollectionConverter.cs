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
    internal class UserCollectionConverter : JsonConverter<UserCollection>
    {
        public override UserCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var userCollection = new UserCollection(null);

            var values = JsonSerializer.Deserialize<string[]>(ref reader, options);
            foreach(var value in values)
            {
                userCollection.Add(new User()
                {
                    Name = value
                });
            }
            return userCollection;
        }

        public override void Write(Utf8JsonWriter writer, UserCollection value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach(var user in value)
            {
                writer.WriteStringValue(user.Name);
            }
            writer.WriteEndArray();
        }
    }
}
