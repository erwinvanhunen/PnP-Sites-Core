using System;
using System.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 190)]
    internal class PublishingParser : IBaseElementParser
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
            if (source.Publishing != null)
            {
                result.Publishing = new Model.Publishing(
                    (Model.AutoCheckRequirementsOptions)Enum.Parse(typeof(Model.AutoCheckRequirementsOptions), source.Publishing.AutoCheckRequirements.ToString()),
                    source.Publishing.DesignPackage != null ?
                    new Model.DesignPackage
                    {
                        DesignPackagePath = source.Publishing.DesignPackage.DesignPackagePath,
                        MajorVersion = source.Publishing.DesignPackage.MajorVersionSpecified ? source.Publishing.DesignPackage.MajorVersion : 0,
                        MinorVersion = source.Publishing.DesignPackage.MinorVersionSpecified ? source.Publishing.DesignPackage.MinorVersion : 0,
                        PackageGuid = Guid.Parse(source.Publishing.DesignPackage.PackageGuid),
                        PackageName = source.Publishing.DesignPackage.PackageName,
                    } : null,
                    source.Publishing.AvailableWebTemplates != null && source.Publishing.AvailableWebTemplates.Length > 0 ?
                         (from awt in source.Publishing.AvailableWebTemplates
                          select new Model.AvailableWebTemplate
                          {
                              LanguageCode = awt.LanguageCodeSpecified ? awt.LanguageCode : 1033,
                              TemplateName = awt.TemplateName,
                          }) : null,
                    source.Publishing.PageLayouts != null && source.Publishing.PageLayouts.PageLayout != null && source.Publishing.PageLayouts.PageLayout.Length > 0 ?
                        (from pl in source.Publishing.PageLayouts.PageLayout
                         select new Model.PageLayout
                         {
                             IsDefault = pl.Path == source.Publishing.PageLayouts.Default,
                             Path = pl.Path,
                         }) : null
                    );
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
            if (template.Publishing != null)
            {
                result.Publishing = new V201605.Publishing
                {
                    AutoCheckRequirements = (V201605.PublishingAutoCheckRequirements)Enum.Parse(typeof(V201605.PublishingAutoCheckRequirements), template.Publishing.AutoCheckRequirements.ToString()),
                    AvailableWebTemplates = template.Publishing.AvailableWebTemplates.Count > 0 ?
                        (from awt in template.Publishing.AvailableWebTemplates
                         select new V201605.PublishingWebTemplate
                         {
                             LanguageCode = awt.LanguageCode,
                             LanguageCodeSpecified = true,
                             TemplateName = awt.TemplateName,
                         }).ToArray() : null,
                    DesignPackage = template.Publishing.DesignPackage != null ? new V201605.PublishingDesignPackage
                    {
                        DesignPackagePath = template.Publishing.DesignPackage.DesignPackagePath,
                        MajorVersion = template.Publishing.DesignPackage.MajorVersion,
                        MajorVersionSpecified = true,
                        MinorVersion = template.Publishing.DesignPackage.MinorVersion,
                        MinorVersionSpecified = true,
                        PackageGuid = template.Publishing.DesignPackage.PackageGuid.ToString(),
                        PackageName = template.Publishing.DesignPackage.PackageName,
                    } : null,
                    PageLayouts = template.Publishing.PageLayouts != null ?
                        new V201605.PublishingPageLayouts
                        {
                            PageLayout = template.Publishing.PageLayouts.Count > 0 ?
                        (from pl in template.Publishing.PageLayouts
                         select new V201605.PublishingPageLayoutsPageLayout
                         {
                             Path = pl.Path,
                         }).ToArray() : null,
                            Default = template.Publishing.PageLayouts.Any(p => p.IsDefault) ?
                                template.Publishing.PageLayouts.Last(p => p.IsDefault).Path : null,
                        } : null,
                };
            }
            else
            {
                result.Publishing = null;
            }
            return result;
        }

    }
}
