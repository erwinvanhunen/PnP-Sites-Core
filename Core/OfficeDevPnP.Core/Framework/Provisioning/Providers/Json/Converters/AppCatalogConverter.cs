using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class AppCatalogConverter : JsonConverter<AppCatalog>
    {
        public override AppCatalog Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            var appCatalog = new AppCatalog();
            while(reader.Read())
            {
                var package = new Package();
                switch (reader.TokenType)
                {
                    case JsonTokenType.StartObject:
                        {
                            package = new Package();
                            break;
                        }
                    case JsonTokenType.EndObject:
                        {
                            appCatalog.Packages.Add(package);
                            break;
                        }
                    case JsonTokenType.PropertyName:
                        {
                            var propertyName = reader.GetString();
                            switch (propertyName)
                            {
                                case "src":
                                    {
                                        package.Src = reader.GetString();
                                        break;
                                    }
                                case "action":
                                    {
                                        package.Action = (PackageAction)Enum.Parse(typeof(PackageAction), reader.GetString());
                                        break;
                                    }
                                case "overwrite":
                                    {
                                        package.Overwrite = reader.GetBoolean();
                                        break;
                                    }
                                case "skipFeatureDeployment":
                                    {
                                        package.SkipFeatureDeployment = reader.GetBoolean();
                                        break;
                                    }
                                case "packageId":
                                    {
                                        package.PackageId = reader.GetString();
                                        break;
                                    }

                            }
                            break;
                        }
                }
            }
            return appCatalog;
        }

        public override void Write(Utf8JsonWriter writer, AppCatalog value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach(var package in value.Packages)
            {
                writer.WriteStartObject();
                if (!string.IsNullOrEmpty(package.Src))
                {
                    writer.WriteString("src", package.Src);
                }
                writer.WriteString("action", package.Action.ToString());
                writer.WriteBoolean("overwrite", package.Overwrite);
                if (!string.IsNullOrEmpty(package.PackageId))
                {
                    writer.WriteString("packageId", package.PackageId);
                }
                writer.WriteBoolean("skipFeatureDeployment", package.SkipFeatureDeployment);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
