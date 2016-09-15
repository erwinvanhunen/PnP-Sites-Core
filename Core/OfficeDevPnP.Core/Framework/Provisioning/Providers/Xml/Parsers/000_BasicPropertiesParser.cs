using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Extensions;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 210)]
    internal class BasicPropertiesParser : IBaseElementParser
    {
        public ProvisioningTemplate ParseElement(XMLPnPSchemaVersion schema, ProvisioningTemplate outgoingTemplate, IProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var source = incomingTemplate as V201605.ProvisioningTemplate;
                        outgoingTemplate = Parse201605Element(outgoingTemplate, source);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }
            return outgoingTemplate;
        }

        private static ProvisioningTemplate Parse201605Element(ProvisioningTemplate result, V201605.ProvisioningTemplate source)
        {
            result.Id = source.ID;
            result.Version = (Double)source.Version;
            result.SitePolicy = source.SitePolicy;
            result.ImagePreviewUrl = source.ImagePreviewUrl;
            result.DisplayName = source.DisplayName;
            result.Description = source.Description;
            result.BaseSiteTemplate = source.BaseSiteTemplate;

            if (source.Properties != null && source.Properties.Length > 0)
            {
                result.Properties.AddRange(
                    (from p in source.Properties
                     select p).ToDictionary(i => i.Key, i => i.Value));
            }

            return result;
        }

        public IProvisioningTemplate ParseTemplate(XMLPnPSchemaVersion schema, IProvisioningTemplate outgoingTemplate, ProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var result = outgoingTemplate as V201605.ProvisioningTemplate;
                        outgoingTemplate = Parse201605Object(result, incomingTemplate);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }
            return outgoingTemplate;
        }

        private static IProvisioningTemplate Parse201605Object(V201605.ProvisioningTemplate result, ProvisioningTemplate template)
        {
            result.ID = template.Id;
            result.Version = (Decimal)template.Version;
            result.VersionSpecified = true;
            result.SitePolicy = template.SitePolicy;
            result.ImagePreviewUrl = template.ImagePreviewUrl;
            result.DisplayName = template.DisplayName;
            result.Description = template.Description;
            result.BaseSiteTemplate = template.BaseSiteTemplate;

            if (template.Properties != null && template.Properties.Any())
            {
                result.Properties =
                    (from p in template.Properties
                     select new V201605.StringDictionaryItem
                     {
                         Key = p.Key,
                         Value = p.Value,
                     }).ToArray();
            }
            else
            {
                result.Properties = null;
            }

            return result;
        }
    }
}
