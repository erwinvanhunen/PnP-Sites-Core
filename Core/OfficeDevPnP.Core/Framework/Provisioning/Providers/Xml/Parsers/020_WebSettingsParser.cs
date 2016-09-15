using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 20)]
    internal class WebSettingsParser : IBaseElementParser
    {
        public ProvisioningTemplate ParseElement(XMLPnPSchemaVersion schema, ProvisioningTemplate outgoingTemplate, IProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var source = incomingTemplate as V201605.ProvisioningTemplate;
                        if (source.WebSettings != null)
                        {
                            outgoingTemplate.WebSettings = new Model.WebSettings
                            {
                                NoCrawl = source.WebSettings.NoCrawlSpecified && source.WebSettings.NoCrawl,
                                RequestAccessEmail = source.WebSettings.RequestAccessEmail,
                                WelcomePage = source.WebSettings.WelcomePage,
                                Title = source.WebSettings.Title,
                                Description = source.WebSettings.Description,
                                SiteLogo = source.WebSettings.SiteLogo,
                                AlternateCSS = source.WebSettings.AlternateCSS,
                                MasterPageUrl = source.WebSettings.MasterPageUrl,
                                CustomMasterPageUrl = source.WebSettings.CustomMasterPageUrl
                            };
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
            if (template.WebSettings != null)
            {
                result.WebSettings = new V201605.WebSettings
                {
                    NoCrawl = template.WebSettings.NoCrawl,
                    NoCrawlSpecified = true,
                    RequestAccessEmail = template.WebSettings.RequestAccessEmail,
                    Title = template.WebSettings.Title,
                    Description = template.WebSettings.Description,
                    SiteLogo = template.WebSettings.SiteLogo,
                    AlternateCSS = template.WebSettings.AlternateCSS,
                    MasterPageUrl = template.WebSettings.MasterPageUrl,
                    CustomMasterPageUrl = template.WebSettings.CustomMasterPageUrl,
                    WelcomePage = template.WebSettings.WelcomePage
                };
            }
            return result;
        }
    }
}
