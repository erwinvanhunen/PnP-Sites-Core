using System;
using System.Linq;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 50)]
    internal class AuditSettingsParser : IBaseElementParser
    {
        public ProvisioningTemplate ParseElement(XMLPnPSchemaVersion schema, ProvisioningTemplate outgoingTemplate, IProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var source = incomingTemplate as V201605.ProvisioningTemplate;
                        outgoingTemplate.AuditSettings = new Model.AuditSettings
                        {
                            AuditLogTrimmingRetention = source.AuditSettings.AuditLogTrimmingRetentionSpecified ? source.AuditSettings.AuditLogTrimmingRetention : 0,
                            TrimAuditLog = source.AuditSettings.TrimAuditLogSpecified && source.AuditSettings.TrimAuditLog,
                            AuditFlags = source.AuditSettings.Audit.Aggregate(Microsoft.SharePoint.Client.AuditMaskType.None, (acc, next) => acc |= (Microsoft.SharePoint.Client.AuditMaskType)Enum.Parse(typeof(Microsoft.SharePoint.Client.AuditMaskType), next.AuditFlag.ToString())),
                        };
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
            if (template.AuditSettings != null)
            {
                result.AuditSettings = new V201605.AuditSettings
                {
                    AuditLogTrimmingRetention = template.AuditSettings.AuditLogTrimmingRetention,
                    AuditLogTrimmingRetentionSpecified = true,
                    TrimAuditLog = template.AuditSettings.TrimAuditLog,
                    TrimAuditLogSpecified = true,
                    Audit = template.AuditSettings.AuditFlags.FromTemplateToSchemaAuditsV201605(),
                };
            }
            else
            {
                result.AuditSettings = null;
            }
            return result;
        }
    }
}
