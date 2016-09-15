using System;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;
using ProvisioningTemplate = OfficeDevPnP.Core.Framework.Provisioning.Model.ProvisioningTemplate;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 180)]
    internal class SearchSettingsParser : IBaseElementParser
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
            if (source.SearchSettings != null && source.SearchSettings.SiteSearchSettings != null)
            {
                result.SiteSearchSettings = source.SearchSettings.SiteSearchSettings.OuterXml;
            }

            if (source.SearchSettings != null && source.SearchSettings.WebSearchSettings != null)
            {
                result.WebSearchSettings = source.SearchSettings.WebSearchSettings.OuterXml;
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
            if (!String.IsNullOrEmpty(template.SiteSearchSettings))
            {
                if (result.SearchSettings == null)
                {
                    result.SearchSettings = new ProvisioningTemplateSearchSettings();
                }
                result.SearchSettings.SiteSearchSettings = template.SiteSearchSettings.ToXmlElement();
            }

            if (!String.IsNullOrEmpty(template.WebSearchSettings))
            {
                if (result.SearchSettings == null)
                {
                    result.SearchSettings = new ProvisioningTemplateSearchSettings();
                }
                result.SearchSettings.WebSearchSettings = template.WebSearchSettings.ToXmlElement();
            }
            return result;
        }
    }
}
