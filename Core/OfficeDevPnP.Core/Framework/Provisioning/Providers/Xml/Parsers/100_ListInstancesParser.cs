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
        Sequence = 100)]
    internal class ListInstancesParser : IBaseElementParser
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
            if (source.Lists != null)
            {
                result.Lists.AddRange(
                    from list in source.Lists
                    select new Model.ListInstance(
                        (list.ContentTypeBindings != null ?
                                (from contentTypeBinding in list.ContentTypeBindings
                                 select new Model.ContentTypeBinding
                                 {
                                     ContentTypeId = contentTypeBinding.ContentTypeID,
                                     Default = contentTypeBinding.Default,
                                     Remove = contentTypeBinding.Remove,
                                 }) : null),
                        (list.Views != null ?
                                (from view in list.Views.Any
                                 select new Model.View
                                 {
                                     SchemaXml = view.OuterXml,
                                 }) : null),
                        (list.Fields != null ?
                                (from field in list.Fields.Any
                                 select new Model.Field
                                 {
                                     SchemaXml = field.OuterXml,
                                 }) : null),
                        (list.FieldRefs != null ?
                                    (from fieldRef in list.FieldRefs
                                     select new Model.FieldRef(fieldRef.Name)
                                     {
                                         DisplayName = fieldRef.DisplayName,
                                         Hidden = fieldRef.Hidden,
                                         Required = fieldRef.Required,
                                         Id = Guid.Parse(fieldRef.ID)
                                     }) : null),
                        (list.DataRows != null ?
                                    (from dataRow in list.DataRows
                                     select new Model.DataRow(
                                 (from dataValue in dataRow.DataValue
                                  select dataValue).ToDictionary(k => k.FieldName, v => v.Value),
                                 dataRow.Security.FromSchemaToTemplateObjectSecurityV201605()
                             )).ToList() : null),
                        (list.FieldDefaults != null ?
                            (from fd in list.FieldDefaults
                             select fd).ToDictionary(k => k.FieldName, v => v.Value) : null),
                        list.Security.FromSchemaToTemplateObjectSecurityV201605(),
                        (list.Folders != null ?
                            (new List<Model.Folder>(from folder in list.Folders
                                                    select folder.FromSchemaToTemplateFolderV201605())) : null),
                        (list.UserCustomActions != null ?
                            (new List<Model.CustomAction>(
                                from customAction in list.UserCustomActions
                                select new Model.CustomAction
                                {
                                    CommandUIExtension = (customAction.CommandUIExtension != null && customAction.CommandUIExtension.Any != null) ?
                                        (new XElement("CommandUIExtension", from x in customAction.CommandUIExtension.Any select x.ToXElement())) : null,
                                    Description = customAction.Description,
                                    Enabled = customAction.Enabled,
                                    Group = customAction.Group,
                                    ImageUrl = customAction.ImageUrl,
                                    Location = customAction.Location,
                                    Name = customAction.Name,
                                    Rights = customAction.Rights.ToBasePermissionsV201605(),
                                    ScriptBlock = customAction.ScriptBlock,
                                    ScriptSrc = customAction.ScriptSrc,
                                    RegistrationId = customAction.RegistrationId,
                                    RegistrationType = (UserCustomActionRegistrationType)Enum.Parse(typeof(UserCustomActionRegistrationType), customAction.RegistrationType.ToString(), true),
                                    Remove = customAction.Remove,
                                    Sequence = customAction.SequenceSpecified ? customAction.Sequence : 100,
                                    Title = customAction.Title,
                                    Url = customAction.Url,
                                })) : null)
                        )
                    {
                        ContentTypesEnabled = list.ContentTypesEnabled,
                        Description = list.Description,
                        DocumentTemplate = list.DocumentTemplate,
                        EnableVersioning = list.EnableVersioning,
                        EnableMinorVersions = list.EnableMinorVersions,
                        DraftVersionVisibility = list.DraftVersionVisibility,
                        EnableModeration = list.EnableModeration,
                        Hidden = list.Hidden,
                        MinorVersionLimit = list.MinorVersionLimitSpecified ? list.MinorVersionLimit : 0,
                        MaxVersionLimit = list.MaxVersionLimitSpecified ? list.MaxVersionLimit : 0,
                        OnQuickLaunch = list.OnQuickLaunch,
                        EnableAttachments = list.EnableAttachments,
                        EnableFolderCreation = list.EnableFolderCreation,
                        ForceCheckout = list.ForceCheckout,
                        RemoveExistingContentTypes = list.RemoveExistingContentTypes,
                        TemplateFeatureID = !String.IsNullOrEmpty(list.TemplateFeatureID) ? Guid.Parse(list.TemplateFeatureID) : Guid.Empty,
                        RemoveExistingViews = list.Views != null ? list.Views.RemoveExistingViews : false,
                        TemplateType = list.TemplateType,
                        Title = list.Title,
                        Url = list.Url,
                    });
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
            // Translate Lists Instances, if any
            if (template.Lists != null && template.Lists.Count > 0)
            {
                result.Lists =
                    (from list in template.Lists
                     select new V201605.ListInstance
                     {
                         ContentTypesEnabled = list.ContentTypesEnabled,
                         Description = list.Description,
                         DocumentTemplate = list.DocumentTemplate,
                         EnableVersioning = list.EnableVersioning,
                         EnableMinorVersions = list.EnableMinorVersions,
                         EnableModeration = list.EnableModeration,
                         DraftVersionVisibility = list.DraftVersionVisibility,
                         DraftVersionVisibilitySpecified = true,
                         Hidden = list.Hidden,
                         MinorVersionLimit = list.MinorVersionLimit,
                         MinorVersionLimitSpecified = true,
                         MaxVersionLimit = list.MaxVersionLimit,
                         MaxVersionLimitSpecified = true,
                         OnQuickLaunch = list.OnQuickLaunch,
                         EnableAttachments = list.EnableAttachments,
                         EnableFolderCreation = list.EnableFolderCreation,
                         ForceCheckout = list.ForceCheckout,
                         RemoveExistingContentTypes = list.RemoveExistingContentTypes,
                         TemplateFeatureID = list.TemplateFeatureID != Guid.Empty ? list.TemplateFeatureID.ToString() : null,
                         TemplateType = list.TemplateType,
                         Title = list.Title,
                         Url = list.Url,
                         ContentTypeBindings = list.ContentTypeBindings.Count > 0 ?
                            (from contentTypeBinding in list.ContentTypeBindings
                             select new V201605.ContentTypeBinding
                             {
                                 ContentTypeID = contentTypeBinding.ContentTypeId,
                                 Default = contentTypeBinding.Default,
                                 Remove = contentTypeBinding.Remove,
                             }).ToArray() : null,
                         Views = list.Views.Count > 0 ?
                         new V201605.ListInstanceViews
                         {
                             Any =
                                (from view in list.Views
                                 select view.SchemaXml.ToXmlElement()).ToArray(),
                             RemoveExistingViews = list.RemoveExistingViews,
                         } : null,
                         Fields = list.Fields.Count > 0 ?
                         new V201605.ListInstanceFields
                         {
                             Any =
                             (from field in list.Fields
                              select field.SchemaXml.ToXmlElement()).ToArray(),
                         } : null,
                         FieldDefaults = list.FieldDefaults.Count > 0 ?
                            (from value in list.FieldDefaults
                             select new FieldDefault { FieldName = value.Key, Value = value.Value }).ToArray() : null,
                         FieldRefs = list.FieldRefs.Count > 0 ?
                         (from fieldRef in list.FieldRefs
                          select new V201605.ListInstanceFieldRef
                          {
                              Name = fieldRef.Name,
                              DisplayName = fieldRef.DisplayName,
                              Hidden = fieldRef.Hidden,
                              Required = fieldRef.Required,
                              ID = fieldRef.Id.ToString(),
                          }).ToArray() : null,
                         DataRows = list.DataRows.Count > 0 ?
                            (from dr in list.DataRows
                             select new ListInstanceDataRow
                             {
                                 DataValue = dr.Values.Count > 0 ?
                                    (from value in dr.Values
                                     select new DataValue { FieldName = value.Key, Value = value.Value }).ToArray() : null,
                                 Security = dr.Security.FromTemplateToSchemaObjectSecurityV201605()
                             }).ToArray() : null,
                         Security = list.Security.FromTemplateToSchemaObjectSecurityV201605(),
                         Folders = list.Folders.Count > 0 ?
                         (from folder in list.Folders
                          select folder.FromTemplateToSchemaFolderV201605()).ToArray() : null,
                         UserCustomActions = list.UserCustomActions.Count > 0 ?
                         (from customAction in list.UserCustomActions
                          select new V201605.CustomAction
                          {
                              CommandUIExtension = new CustomActionCommandUIExtension
                              {
                                  Any = customAction.CommandUIExtension != null ?
                                     (from x in customAction.CommandUIExtension.Elements() select x.ToXmlElement()).ToArray() : null,
                              },
                              Description = customAction.Description,
                              Enabled = customAction.Enabled,
                              Group = customAction.Group,
                              ImageUrl = customAction.ImageUrl,
                              Location = customAction.Location,
                              Name = customAction.Name,
                              Rights = customAction.Rights.FromBasePermissionsToStringV201605(),
                              RegistrationId = customAction.RegistrationId,
                              RegistrationType = (RegistrationType)Enum.Parse(typeof(RegistrationType), customAction.RegistrationType.ToString(), true),
                              RegistrationTypeSpecified = true,
                              Remove = customAction.Remove,
                              ScriptBlock = customAction.ScriptBlock,
                              ScriptSrc = customAction.ScriptSrc,
                              Sequence = customAction.Sequence,
                              SequenceSpecified = true,
                              Title = customAction.Title,
                              Url = customAction.Url,
                          }).ToArray() : null,
                     }).ToArray();
            }
            else
            {
                result.Lists = null;
            }
            return result;
        }
    }
}
