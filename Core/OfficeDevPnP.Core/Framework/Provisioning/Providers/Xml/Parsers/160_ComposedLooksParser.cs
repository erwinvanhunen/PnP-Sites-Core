using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 160)]
    internal class ComposedLooksParser : IBaseElementParser
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
            if (source.ComposedLook != null)
            {
                result.ComposedLook.BackgroundFile = source.ComposedLook.BackgroundFile;
                result.ComposedLook.ColorFile = source.ComposedLook.ColorFile;
                result.ComposedLook.FontFile = source.ComposedLook.FontFile;
                result.ComposedLook.Name = source.ComposedLook.Name;
                result.ComposedLook.Version = source.ComposedLook.Version;
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
            // Translate ComposedLook, if any
            if (template.ComposedLook != null && !template.ComposedLook.Equals(Model.ComposedLook.Empty))
            {
                result.ComposedLook = new V201605.ComposedLook
                {
                    BackgroundFile = template.ComposedLook.BackgroundFile,
                    ColorFile = template.ComposedLook.ColorFile,
                    FontFile = template.ComposedLook.FontFile,
                    Name = template.ComposedLook.Name,
                    Version = template.ComposedLook.Version,
                    VersionSpecified = true,
                };

                if (
                    template.ComposedLook.BackgroundFile == null &&
                    template.ComposedLook.ColorFile == null &&
                    template.ComposedLook.FontFile == null &&
                    template.ComposedLook.Name == null &&
                    template.ComposedLook.Version == 0)
                {
                    result.ComposedLook = null;
                }
            }
            return result;
        }

    }
}
