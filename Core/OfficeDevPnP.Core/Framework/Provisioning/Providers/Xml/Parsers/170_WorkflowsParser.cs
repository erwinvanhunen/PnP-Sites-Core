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
        Sequence = 170)]
    internal class WorkflowsParser : IBaseElementParser
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
            if (source.Workflows != null)
            {
                result.Workflows = new Model.Workflows(
                    (source.Workflows.WorkflowDefinitions != null &&
                    source.Workflows.WorkflowDefinitions.Length > 0) ?
                        (from wd in source.Workflows.WorkflowDefinitions
                         select new Model.WorkflowDefinition(
                             (wd.Properties != null && wd.Properties.Length > 0) ?
                             (from p in wd.Properties
                              select p).ToDictionary(i => i.Key, i => i.Value) : null)
                         {
                             AssociationUrl = wd.AssociationUrl,
                             Description = wd.Description,
                             DisplayName = wd.DisplayName,
                             DraftVersion = wd.DraftVersion,
                             FormField = wd.FormField != null ? wd.FormField.OuterXml : null,
                             Id = Guid.Parse(wd.Id),
                             InitiationUrl = wd.InitiationUrl,
                             Published = wd.PublishedSpecified ? wd.Published : false,
                             RequiresAssociationForm = wd.RequiresAssociationFormSpecified ? wd.RequiresAssociationForm : false,
                             RequiresInitiationForm = wd.RequiresInitiationFormSpecified ? wd.RequiresInitiationForm : false,
                             RestrictToScope = wd.RestrictToScope,
                             RestrictToType = wd.RestrictToType.ToString(),
                             XamlPath = wd.XamlPath,
                         }) : null,
                    (source.Workflows.WorkflowSubscriptions != null &&
                    source.Workflows.WorkflowSubscriptions.Length > 0) ?
                        (from ws in source.Workflows.WorkflowSubscriptions
                         select new Model.WorkflowSubscription(
                             (ws.PropertyDefinitions != null && ws.PropertyDefinitions.Length > 0) ?
                             (from pd in ws.PropertyDefinitions
                              select pd).ToDictionary(i => i.Key, i => i.Value) : null)
                         {
                             DefinitionId = Guid.Parse(ws.DefinitionId),
                             Enabled = ws.Enabled,
                             EventSourceId = ws.EventSourceId,
                             EventTypes = (new String[] {
                                ws.ItemAddedEvent? "ItemAdded" : null,
                                ws.ItemUpdatedEvent? "ItemUpdated" : null,
                                ws.WorkflowStartEvent? "WorkflowStart" : null }).Where(e => e != null).ToList(),
                             ListId = ws.ListId,
                             ManualStartBypassesActivationLimit = ws.ManualStartBypassesActivationLimitSpecified ? ws.ManualStartBypassesActivationLimit : false,
                             Name = ws.Name,
                             ParentContentTypeId = ws.ParentContentTypeId,
                             StatusFieldName = ws.StatusFieldName,
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
            if (template.Workflows != null &&
               (template.Workflows.WorkflowDefinitions.Any() || template.Workflows.WorkflowSubscriptions.Any()))
            {
                result.Workflows = new V201605.Workflows
                {
                    WorkflowDefinitions = template.Workflows.WorkflowDefinitions.Count > 0 ?
                        (from wd in template.Workflows.WorkflowDefinitions
                         select new WorkflowsWorkflowDefinition
                         {
                             AssociationUrl = wd.AssociationUrl,
                             Description = wd.Description,
                             DisplayName = wd.DisplayName,
                             DraftVersion = wd.DraftVersion,
                             FormField = (wd.FormField != null) ? wd.FormField.ToXmlElement() : null,
                             Id = wd.Id.ToString(),
                             InitiationUrl = wd.InitiationUrl,
                             Properties = (wd.Properties != null && wd.Properties.Count > 0) ?
                                (from p in wd.Properties
                                 select new V201605.StringDictionaryItem
                                 {
                                     Key = p.Key,
                                     Value = p.Value,
                                 }).ToArray() : null,
                             Published = wd.Published,
                             PublishedSpecified = true,
                             RequiresAssociationForm = wd.RequiresAssociationForm,
                             RequiresAssociationFormSpecified = true,
                             RequiresInitiationForm = wd.RequiresInitiationForm,
                             RequiresInitiationFormSpecified = true,
                             RestrictToScope = wd.RestrictToScope,
                             RestrictToType = (V201605.WorkflowsWorkflowDefinitionRestrictToType)Enum.Parse(typeof(V201605.WorkflowsWorkflowDefinitionRestrictToType), wd.RestrictToType),
                             RestrictToTypeSpecified = true,
                             XamlPath = wd.XamlPath,
                         }).ToArray() : null,
                    WorkflowSubscriptions = template.Workflows.WorkflowSubscriptions.Count > 0 ?
                        (from ws in template.Workflows.WorkflowSubscriptions
                         select new WorkflowsWorkflowSubscription
                         {
                             DefinitionId = ws.DefinitionId.ToString(),
                             Enabled = ws.Enabled,
                             EventSourceId = ws.EventSourceId,
                             ItemAddedEvent = ws.EventTypes.Contains("ItemAdded"),
                             ItemUpdatedEvent = ws.EventTypes.Contains("ItemUpdated"),
                             WorkflowStartEvent = ws.EventTypes.Contains("WorkflowStart"),
                             ListId = ws.ListId,
                             ManualStartBypassesActivationLimit = ws.ManualStartBypassesActivationLimit,
                             ManualStartBypassesActivationLimitSpecified = true,
                             Name = ws.Name,
                             ParentContentTypeId = ws.ParentContentTypeId,
                             PropertyDefinitions = (ws.PropertyDefinitions != null && ws.PropertyDefinitions.Count > 0) ?
                                (from pd in ws.PropertyDefinitions
                                 select new V201605.StringDictionaryItem
                                 {
                                     Key = pd.Key,
                                     Value = pd.Value,
                                 }).ToArray() : null,
                             StatusFieldName = ws.StatusFieldName,
                         }).ToArray() : null,
                };
            }
            else
            {
                result.Workflows = null;
            }
            return result;
        }
    }
}
