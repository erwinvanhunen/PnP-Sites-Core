using System;
using System.Linq;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;
using ContentType = OfficeDevPnP.Core.Framework.Provisioning.Model.ContentType;
using ProvisioningTemplate = OfficeDevPnP.Core.Framework.Provisioning.Model.ProvisioningTemplate;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 90)]
    internal class ContentTypesParser : IBaseElementParser
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
            if (source.ContentTypes != null)
            {
                result.ContentTypes.AddRange(
                    from contentType in source.ContentTypes
                    select new ContentType(
                        contentType.ID,
                        contentType.Name,
                        contentType.Description,
                        contentType.Group,
                        contentType.Sealed,
                        contentType.Hidden,
                        contentType.ReadOnly,
                        (contentType.DocumentTemplate != null ?
                            contentType.DocumentTemplate.TargetName : null),
                        contentType.Overwrite,
                        (contentType.FieldRefs != null ?
                            (from fieldRef in contentType.FieldRefs
                             select new Model.FieldRef(fieldRef.Name)
                             {
                                 Id = Guid.Parse(fieldRef.ID),
                                 Hidden = fieldRef.Hidden,
                                 Required = fieldRef.Required
                             }) : null)
                        )
                    {
                        DocumentSetTemplate = contentType.DocumentSetTemplate != null ?
                            new Model.DocumentSetTemplate(
                                contentType.DocumentSetTemplate.WelcomePage,
                                contentType.DocumentSetTemplate.AllowedContentTypes != null ?
                                    (from act in contentType.DocumentSetTemplate.AllowedContentTypes
                                     select act.ContentTypeID) : null,
                                contentType.DocumentSetTemplate.DefaultDocuments != null ?
                                    (from dd in contentType.DocumentSetTemplate.DefaultDocuments
                                     select new Model.DefaultDocument
                                     {
                                         ContentTypeId = dd.ContentTypeID,
                                         FileSourcePath = dd.FileSourcePath,
                                         Name = dd.Name,
                                     }) : null,
                                contentType.DocumentSetTemplate.SharedFields != null ?
                                    (from sf in contentType.DocumentSetTemplate.SharedFields
                                     select Guid.Parse(sf.ID)) : null,
                                contentType.DocumentSetTemplate.WelcomePageFields != null ?
                                    (from wpf in contentType.DocumentSetTemplate.WelcomePageFields
                                     select Guid.Parse(wpf.ID)) : null
                                ) : null,
                        DisplayFormUrl = contentType.DisplayFormUrl,
                        EditFormUrl = contentType.EditFormUrl,
                        NewFormUrl = contentType.NewFormUrl,
                    }
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
            // Translate ContentTypes, if any
            if (template.ContentTypes != null && template.ContentTypes.Count > 0)
            {
                result.ContentTypes =
                    (from ct in template.ContentTypes
                     select new V201605.ContentType
                     {
                         ID = ct.Id,
                         Description = ct.Description,
                         Group = ct.Group,
                         Name = ct.Name,
                         FieldRefs = ct.FieldRefs.Count > 0 ?
                         (from fieldRef in ct.FieldRefs
                          select new V201605.ContentTypeFieldRef
                          {
                              Name = fieldRef.Name,
                              ID = fieldRef.Id.ToString(),
                              Hidden = fieldRef.Hidden,
                              Required = fieldRef.Required
                          }).ToArray() : null,
                         DocumentTemplate = !String.IsNullOrEmpty(ct.DocumentTemplate) ? new ContentTypeDocumentTemplate { TargetName = ct.DocumentTemplate } : null,
                         DocumentSetTemplate = ct.DocumentSetTemplate != null ?
                             new V201605.DocumentSetTemplate
                             {
                                 AllowedContentTypes = ct.DocumentSetTemplate.AllowedContentTypes.Count > 0 ?
                                     (from act in ct.DocumentSetTemplate.AllowedContentTypes
                                      select new DocumentSetTemplateAllowedContentType
                                      {
                                          ContentTypeID = act
                                      }).ToArray() : null,
                                 DefaultDocuments = ct.DocumentSetTemplate.DefaultDocuments.Count > 0 ?
                                     (from dd in ct.DocumentSetTemplate.DefaultDocuments
                                      select new DocumentSetTemplateDefaultDocument
                                      {
                                          ContentTypeID = dd.ContentTypeId,
                                          FileSourcePath = dd.FileSourcePath,
                                          Name = dd.Name,
                                      }).ToArray() : null,
                                 SharedFields = ct.DocumentSetTemplate.SharedFields.Count > 0 ?
                                     (from sf in ct.DocumentSetTemplate.SharedFields
                                      select new DocumentSetFieldRef
                                      {
                                          ID = sf.ToString(),
                                      }).ToArray() : null,
                                 WelcomePage = ct.DocumentSetTemplate.WelcomePage,
                                 WelcomePageFields = ct.DocumentSetTemplate.WelcomePageFields.Count > 0 ?
                                     (from wpf in ct.DocumentSetTemplate.WelcomePageFields
                                      select new DocumentSetFieldRef
                                      {
                                          ID = wpf.ToString(),
                                      }).ToArray() : null,
                             } : null,
                         DisplayFormUrl = ct.DisplayFormUrl,
                         EditFormUrl = ct.EditFormUrl,
                         NewFormUrl = ct.NewFormUrl,
                     }).ToArray();
            }
            else
            {
                result.ContentTypes = null;
            }
            return result;
        }
    }
}
