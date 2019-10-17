using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Resolvers;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json
{
    public class JsonPnPFormatter : ITemplateFormatter
    {
        private TemplateProviderBase _provider;

        private string TenantSchema => "https://developer.microsoft.com/en-us/json-schemas/pnp/provisioning/201909/tenant.schema.json";
        private string SiteSchema => "https://developer.microsoft.com/en-us/json-schemas/pnp/provisioning/201909/site.schema.json";

        public void Initialize(TemplateProviderBase provider)
        {
            this._provider = provider;
        }

        public bool IsValid(System.IO.Stream templateStream)
        {
            return true;
            //using (var localStream = new MemoryStream())
            //{
            //    templateStream.CopyTo(localStream);
            //    localStream.Position = 0;
            //    templateStream.Position = 0;

            //    var jsonString = string.Empty;

            //    using (StreamReader templateReader = new StreamReader(localStream, Encoding.UTF8))
            //    {
            //        jsonString = templateReader.ReadToEnd();
            //    }

            //    var template = System.Text.Json.JsonDocument.Parse(jsonString);

            //    var schemaIdentified = template.Value<string>("$schema");

            //    var resourceName = string.Empty;
            //    if (schemaIdentified.ToLower().EndsWith("site.schema.json"))
            //    {
            //        resourceName = "OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Schemas._201909.site.schema.json";
            //    }
            //    else
            //    {
            //        resourceName = "OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Schemas._201909.tenant.schema.json";
            //    }

            //    var assembly = Assembly.GetExecutingAssembly();

            //    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            //    {
            //        using (StreamReader reader = new StreamReader(stream))
            //        {
            //            string result = reader.ReadToEnd();
            //            var schema = NJsonSchema.JsonSchema.FromJsonAsync(result).GetAwaiter().GetResult();

            //            var messages = schema.Validate(template);
            //            return messages.Count == 0;
            //        }
            //    }
            //}
        }

        public System.IO.Stream ToFormattedTemplate(ProvisioningTemplate template)
        {
            template.Schema = SiteSchema;
            var serializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new AlternateUICultureCollectionConverter(),
                    new SupportedUILanguageCollectionConverter(),
                    new SiteDesignRightConverter(),
                    new JsonStringEnumConverter(),
                    new UserCollectionConverter(),
                    new FieldCollectionConverter(),
                    new XElementConverter(),
                    new BasePermissionsConverter(),
                    new ProvisioningTemplateObjectCollectionConverterFactory(),
                    new ProvisioningHierarchyObjectCollectionConverterFactory()
                }
            };
            var jsonString = JsonSerializer.Serialize(template, serializerOptions);

            var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            MemoryStream jsonStream = new MemoryStream(jsonBytes);
            jsonStream.Position = 0;

            return (jsonStream);
        }

        public System.IO.Stream ToFormattedHierarchy(ProvisioningHierarchy hierarchy)
        {
            hierarchy.Schema = TenantSchema;
            var serializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new AlternateUICultureCollectionConverter(),
                    new SupportedUILanguageCollectionConverter(),
                    new SiteDesignRightConverter(),
                    new JsonStringEnumConverter(),
                    new UserCollectionConverter(),
                    new FieldCollectionConverter(),
                    new XElementConverter(),
                    new BasePermissionsConverter(),
                    new ProvisioningTemplateObjectCollectionConverterFactory(),
                    new ProvisioningHierarchyObjectCollectionConverterFactory()
                }
            };
            var jsonString = JsonSerializer.Serialize(hierarchy, serializerOptions);

            var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            MemoryStream jsonStream = new MemoryStream(jsonBytes);
            jsonStream.Position = 0;

            return (jsonStream);
        }

        public ProvisioningHierarchy ToProvisioningHierarchy(Stream hierarchy)
        {
            if (IsValid(hierarchy))
            {
                hierarchy.Position = 0; // reset to beginning
                StreamReader sr = new StreamReader(hierarchy, Encoding.UTF8);
                var jsonString = sr.ReadToEnd();
                var serializerOptions = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters =
                    {
                        new AlternateUICultureCollectionConverter(),
                        new SupportedUILanguageCollectionConverter(),
                        new SiteDesignRightConverter(),
                        new JsonStringEnumConverter(),
                        new UserCollectionConverter(),
                        new FieldCollectionConverter(),
                        new XElementConverter(),
                        new BasePermissionsConverter(),
                        new ProvisioningTemplateObjectCollectionConverterFactory(),
                        new ProvisioningHierarchyObjectCollectionConverterFactory()
                    }
                };
                var result = JsonSerializer.Deserialize<ProvisioningHierarchy>(jsonString, serializerOptions);
                return result;
            }
            else
            {
                throw new Exception("JSON is not valid");
            }

        }

        public ProvisioningTemplate ToProvisioningTemplate(System.IO.Stream template)
        {
            return (this.ToProvisioningTemplate(template, null));
        }

        public ProvisioningTemplate ToProvisioningTemplate(System.IO.Stream template, string identifier)
        {
            if (IsValid(template))
            {
                template.Position = 0;
                StreamReader sr = new StreamReader(template, Encoding.UTF8);
                string jsonString = sr.ReadToEnd();

                var serializerOptions = new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters =
                    {
                        new AlternateUICultureCollectionConverter(),
                        new SupportedUILanguageCollectionConverter(),
                        new SiteDesignRightConverter(),
                        new JsonStringEnumConverter(),
                        new UserCollectionConverter(),
                        new FieldCollectionConverter(),
                        new XElementConverter(),
                        new BasePermissionsConverter(),
                        new ProvisioningTemplateObjectCollectionConverterFactory(),
                        new ProvisioningHierarchyObjectCollectionConverterFactory()
                    }
                };
                var result = JsonSerializer.Deserialize<ProvisioningTemplate>(jsonString, serializerOptions);

                return (result);
            }
            else
            {
                throw new Exception("JSON is not valid");
            }
        }
    }
}
