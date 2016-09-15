using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;
using ProvisioningTemplate = OfficeDevPnP.Core.Framework.Provisioning.Model.ProvisioningTemplate;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 130)]
    internal class FilesParser : IBaseElementParser
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
            if (source.Files != null)
            {
                if (source.Files.File != null && source.Files.File.Length > 0)
                {
                    // Handle Files
                    result.Files.AddRange(
                        from file in source.Files.File
                        select new Model.File(file.Src,
                            file.Folder,
                            file.Overwrite,
                            file.WebParts != null ?
                                (from wp in file.WebParts
                                 select new Model.WebPart
                                 {
                                     Order = (uint)wp.Order,
                                     Zone = wp.Zone,
                                     Title = wp.Title,
                                     Contents = wp.Contents.InnerXml
                                 }) : null,
                            file.Properties != null ? file.Properties.ToDictionary(k => k.Key, v => v.Value) : null,
                            file.Security.FromSchemaToTemplateObjectSecurityV201605(),
                            file.LevelSpecified ?
                                (Model.FileLevel)Enum.Parse(typeof(Model.FileLevel), file.Level.ToString()) :
                                Model.FileLevel.Draft
                            )
                        );
                }

                if (source.Files.Directory != null && source.Files.Directory.Length > 0)
                {
                    // Handle Directories of files
                    result.Directories.AddRange(
                        from dir in source.Files.Directory
                        select new Model.Directory(dir.Src,
                            dir.Folder,
                            dir.Overwrite,
                            dir.LevelSpecified ?
                                (Model.FileLevel)Enum.Parse(typeof(Model.FileLevel), dir.Level.ToString()) :
                                Model.FileLevel.Draft,
                            dir.Recursive,
                            dir.IncludedExtensions,
                            dir.ExcludedExtensions,
                            dir.MetadataMappingFile,
                            dir.Security.FromSchemaToTemplateObjectSecurityV201605()
                            )
                        );
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
            // Translate Files, if any
            if (template.Files != null && template.Files.Count > 0)
            {
                result.Files = new ProvisioningTemplateFiles();

                result.Files.File =
                    (from file in template.Files
                     select new V201605.File
                     {
                         Overwrite = file.Overwrite,
                         Src = file.Src,
                         Folder = file.Folder,
                         WebParts = file.WebParts.Count > 0 ?
                            (from wp in file.WebParts
                             select new V201605.WebPartPageWebPart
                             {
                                 Zone = wp.Zone,
                                 Order = (int)wp.Order,
                                 Contents = XElement.Parse(wp.Contents).ToXmlElement(),
                                 Title = wp.Title,
                             }).ToArray() : null,
                         Properties = file.Properties != null && file.Properties.Count > 0 ?
                            (from p in file.Properties
                             select new V201605.StringDictionaryItem
                             {
                                 Key = p.Key,
                                 Value = p.Value
                             }).ToArray() : null,
                         Security = file.Security.FromTemplateToSchemaObjectSecurityV201605()
                     }).ToArray();
            }
            else
            {
                result.Files = null;
            }
            return result;
        }
    }
}
