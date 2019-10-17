using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model.Configuration
{

    public partial class ExtractConfiguration
    {
        [JsonPropertyName("$schema")]
        public string Schema { get; set; }

        public bool PersistAssetFiles { get; set; }

        public List<ConfigurationHandler> Handlers { get; set; }

        public Lists.ExtractConfiguration Lists { get; set; }

        public Pages.ExtractConfiguration Pages { get; set; }

        public SiteSecurity.ExtractConfiguration SiteSecurity { get; set; }

        public Taxonomy.ExtractConfiguration Taxonomy { get; set; }

        public Navigation.ExtractConfiguration Navigation { get; set; }

        public SiteFooter.ExtractConfiguration SiteFooter { get; set; }

        public ContentTypes.ExtractConfiguration ContentTypes { get; set; }

        public SearchSettings.ExtractConfiguration SearchSettings { get; set; }
        public ProvisioningTemplateCreationInformation ToCreationInformation(Web web)
        {

            var ci = new ProvisioningTemplateCreationInformation(web);

            ci.ExtractConfiguration = this;

            ci.PersistBrandingFiles = PersistAssetFiles;

            if (Handlers != null && Handlers.Any())
            {
                ci.HandlersToProcess = Model.Handlers.None;
                foreach (var handler in Handlers)
                {
                    Model.Handlers handlerEnumValue = Model.Handlers.None;
                    switch (handler)
                    {
                        case ConfigurationHandler.Pages:
                            handlerEnumValue = Model.Handlers.PageContents;
                            break;
                        case ConfigurationHandler.Taxonomy:
                            handlerEnumValue = Model.Handlers.TermGroups;
                            break;
                        default:
                            handlerEnumValue = (Model.Handlers)Enum.Parse(typeof(Model.Handlers), handler.ToString());
                            break;
                    }
                    ci.HandlersToProcess |= handlerEnumValue;
                }
            }
            else
            {
                ci.HandlersToProcess = Model.Handlers.All;
            }

            if (this.Pages != null)
            {
                ci.IncludeAllClientSidePages = this.Pages.IncludeAllClientSidePages;
            }
            if (this.Lists != null)
            {
                ci.IncludeHiddenLists = this.Lists.IncludeHiddenLists;
            }
            if (this.SiteSecurity != null)
            {
                ci.IncludeSiteGroups = this.SiteSecurity.IncludeSiteGroups;
            }
            if (this.ContentTypes != null)
            {
                ci.ContentTypeGroupsToInclude = this.ContentTypes.Groups;
                ci.IncludeContentTypesFromSyndication = !this.ContentTypes.ExcludeFromSyndication;
            }
            if (this.Taxonomy != null)
            {
                ci.IncludeTermGroupsSecurity = this.Taxonomy.IncludeSecurity;
                ci.IncludeSiteCollectionTermGroup = this.Taxonomy.IncludeSiteCollectionTermGroup;
            }
            if (this.SearchSettings != null)
            {
                ci.IncludeSearchConfiguration = this.SearchSettings.Include;
            }
            return ci;
        }

        public static ExtractConfiguration FromString(string input)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "OfficeDevPnP.Core.Framework.Provisioning.Model.Configuration.Schemas._201909.extract-configuration.schema.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();

                var schema = NJsonSchema.JsonSchema.FromJsonAsync(result).GetAwaiter().GetResult();
                schema.AllowAdditionalProperties = false;
                var validationErrors = schema.Validate(input);
                if (validationErrors.Count > 0)
                {
                    var validationException = new JsonValidationException("Configuration is not valid according to schema.");
                    foreach (var validationError in validationErrors)
                    {
                        validationException.ValidationErrors.Add(new JsonValidationError()
                        {
                            HasLineInfo = validationError.HasLineInfo,
                            Kind = (JsonValidationErrorKind)Enum.Parse(typeof(JsonValidationErrorKind), validationError.Kind.ToString()),
                            LineNumber = validationError.LineNumber,
                            LinePosition = validationError.LinePosition,
                            Path = validationError.Path,
                            Property = validationError.Property
                        });
                    }
                    throw validationException;
                }
                return JsonSerializer.Deserialize<ExtractConfiguration>(input, new JsonSerializerOptions()
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
        }
    }
}
