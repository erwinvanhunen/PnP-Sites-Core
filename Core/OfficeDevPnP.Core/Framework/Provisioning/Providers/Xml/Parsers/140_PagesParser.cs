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
        Sequence = 140)]
    internal class PagesParser : IBaseElementParser
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
            if (source.Pages != null)
            {
                foreach (var page in source.Pages)
                {

                    var pageLayout = WikiPageLayout.OneColumn;
                    switch (page.Layout)
                    {
                        case V201605.WikiPageLayout.OneColumn:
                            pageLayout = WikiPageLayout.OneColumn;
                            break;
                        case V201605.WikiPageLayout.OneColumnSidebar:
                            pageLayout = WikiPageLayout.OneColumnSideBar;
                            break;
                        case V201605.WikiPageLayout.TwoColumns:
                            pageLayout = WikiPageLayout.TwoColumns;
                            break;
                        case V201605.WikiPageLayout.TwoColumnsHeader:
                            pageLayout = WikiPageLayout.TwoColumnsHeader;
                            break;
                        case V201605.WikiPageLayout.TwoColumnsHeaderFooter:
                            pageLayout = WikiPageLayout.TwoColumnsHeaderFooter;
                            break;
                        case V201605.WikiPageLayout.ThreeColumns:
                            pageLayout = WikiPageLayout.ThreeColumns;
                            break;
                        case V201605.WikiPageLayout.ThreeColumnsHeader:
                            pageLayout = WikiPageLayout.ThreeColumnsHeader;
                            break;
                        case V201605.WikiPageLayout.ThreeColumnsHeaderFooter:
                            pageLayout = WikiPageLayout.ThreeColumnsHeaderFooter;
                            break;
                        case V201605.WikiPageLayout.Custom:
                            pageLayout = WikiPageLayout.Custom;
                            break;
                    }

                    result.Pages.Add(new Model.Page(page.Url, page.Overwrite, pageLayout,
                        (page.WebParts != null ?
                            (from wp in page.WebParts
                             select new Model.WebPart
                             {
                                 Title = wp.Title,
                                 Column = (uint)wp.Column,
                                 Row = (uint)wp.Row,
                                 Contents = wp.Contents.InnerXml
                             }).ToList() : null),
                        page.Security.FromSchemaToTemplateObjectSecurityV201605(),
                        (page.Fields != null && page.Fields.Length > 0) ?
                             (from f in page.Fields
                              select f).ToDictionary(i => i.FieldName, i => i.Value) : null
                        ));
                }
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
            if (template.Pages != null && template.Pages.Count > 0)
            {
                var pages = new List<V201605.Page>();

                foreach (var page in template.Pages)
                {
                    var schemaPage = new V201605.Page();

                    var pageLayout = V201605.WikiPageLayout.OneColumn;
                    switch (page.Layout)
                    {
                        case WikiPageLayout.OneColumn:
                            pageLayout = V201605.WikiPageLayout.OneColumn;
                            break;
                        case WikiPageLayout.OneColumnSideBar:
                            pageLayout = V201605.WikiPageLayout.OneColumnSidebar;
                            break;
                        case WikiPageLayout.TwoColumns:
                            pageLayout = V201605.WikiPageLayout.TwoColumns;
                            break;
                        case WikiPageLayout.TwoColumnsHeader:
                            pageLayout = V201605.WikiPageLayout.TwoColumnsHeader;
                            break;
                        case WikiPageLayout.TwoColumnsHeaderFooter:
                            pageLayout = V201605.WikiPageLayout.TwoColumnsHeaderFooter;
                            break;
                        case WikiPageLayout.ThreeColumns:
                            pageLayout = V201605.WikiPageLayout.ThreeColumns;
                            break;
                        case WikiPageLayout.ThreeColumnsHeader:
                            pageLayout = V201605.WikiPageLayout.ThreeColumnsHeader;
                            break;
                        case WikiPageLayout.ThreeColumnsHeaderFooter:
                            pageLayout = V201605.WikiPageLayout.ThreeColumnsHeaderFooter;
                            break;
                        case WikiPageLayout.Custom:
                            pageLayout = V201605.WikiPageLayout.Custom;
                            break;
                    }
                    schemaPage.Layout = pageLayout;
                    schemaPage.Overwrite = page.Overwrite;
                    schemaPage.Security = (page.Security != null) ? page.Security.FromTemplateToSchemaObjectSecurityV201605() : null;

                    schemaPage.WebParts = page.WebParts.Count > 0 ?
                        (from wp in page.WebParts
                         select new V201605.WikiPageWebPart
                         {
                             Column = (int)wp.Column,
                             Row = (int)wp.Row,
                             Contents = XElement.Parse(wp.Contents).ToXmlElement(),
                             Title = wp.Title,
                         }).ToArray() : null;

                    schemaPage.Url = page.Url;

                    schemaPage.Fields = (page.Fields != null && page.Fields.Count > 0) ?
                                (from f in page.Fields
                                 select new V201605.BaseFieldValue
                                 {
                                     FieldName = f.Key,
                                     Value = f.Value,
                                 }).ToArray() : null;

                    pages.Add(schemaPage);
                }

                result.Pages = pages.ToArray();
            }
            return result;
        }
    }
}
