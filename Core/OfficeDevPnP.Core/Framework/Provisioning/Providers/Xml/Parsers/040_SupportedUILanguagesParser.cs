using System;
using System.Linq;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 40)]
    internal class SupportedUILanguagesParser : IBaseElementParser
    {
        public ProvisioningTemplate ParseElement(XMLPnPSchemaVersion schema, ProvisioningTemplate outgoingTemplate, IProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var source = incomingTemplate as V201605.ProvisioningTemplate;

                        if (source.SupportedUILanguages != null && source.SupportedUILanguages.Length > 0)
                        {
                            outgoingTemplate.SupportedUILanguages.AddRange(
                                from l in source.SupportedUILanguages
                                select new SupportedUILanguage
                                {
                                    LCID = l.LCID,
                                });
                        }
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }
            return outgoingTemplate;
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
            if (template.SupportedUILanguages != null && template.SupportedUILanguages.Count > 0)
            {
                result.SupportedUILanguages =
                    (from l in template.SupportedUILanguages
                     select new V201605.SupportedUILanguagesSupportedUILanguage
                     {
                         LCID = l.LCID,
                     }).ToArray();
            }
            else
            {
                result.SupportedUILanguages = null;
            }
            return result;
        }
    }
}
