using System;
using System.Linq;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;
using ProvisioningTemplate = OfficeDevPnP.Core.Framework.Provisioning.Model.ProvisioningTemplate;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 70)]
    internal class NavigationParser : IBaseElementParser
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
            if (source.Navigation != null)
            {
                result.Navigation = new Model.Navigation(
                    source.Navigation.GlobalNavigation != null ?
                        new GlobalNavigation(
                            (GlobalNavigationType)Enum.Parse(typeof(GlobalNavigationType), source.Navigation.GlobalNavigation.NavigationType.ToString()),
                            source.Navigation.GlobalNavigation.StructuralNavigation != null ?
                                new Model.StructuralNavigation
                                {
                                    RemoveExistingNodes = source.Navigation.GlobalNavigation.StructuralNavigation.RemoveExistingNodes,
                                } : null,
                            source.Navigation.GlobalNavigation.ManagedNavigation != null ?
                                new Model.ManagedNavigation
                                {
                                    TermSetId = source.Navigation.GlobalNavigation.ManagedNavigation.TermSetId,
                                    TermStoreId = source.Navigation.GlobalNavigation.ManagedNavigation.TermStoreId,
                                } : null
                        )
                        : null,
                    source.Navigation.CurrentNavigation != null ?
                        new CurrentNavigation(
                            (CurrentNavigationType)Enum.Parse(typeof(CurrentNavigationType), source.Navigation.CurrentNavigation.NavigationType.ToString()),
                            source.Navigation.CurrentNavigation.StructuralNavigation != null ?
                                new Model.StructuralNavigation
                                {
                                    RemoveExistingNodes = source.Navigation.CurrentNavigation.StructuralNavigation.RemoveExistingNodes,
                                } : null,
                            source.Navigation.CurrentNavigation.ManagedNavigation != null ?
                                new Model.ManagedNavigation
                                {
                                    TermSetId = source.Navigation.CurrentNavigation.ManagedNavigation.TermSetId,
                                    TermStoreId = source.Navigation.CurrentNavigation.ManagedNavigation.TermStoreId,
                                } : null
                        )
                        : null
                    );

                // If I need to update the Global Structural Navigation nodes
                if (result.Navigation.GlobalNavigation != null &&
                    result.Navigation.GlobalNavigation.StructuralNavigation != null &&
                    source.Navigation.GlobalNavigation != null &&
                    source.Navigation.GlobalNavigation.StructuralNavigation != null)
                {
                    result.Navigation.GlobalNavigation.StructuralNavigation.NavigationNodes.AddRange(
                        from n in source.Navigation.GlobalNavigation.StructuralNavigation.NavigationNode
                        select n.FromSchemaNavigationNodeToModelNavigationNodeV201605()
                        );
                }

                // If I need to update the Current Structural Navigation nodes
                if (result.Navigation.CurrentNavigation != null &&
                    result.Navigation.CurrentNavigation.StructuralNavigation != null &&
                    source.Navigation.CurrentNavigation != null &&
                    source.Navigation.CurrentNavigation.StructuralNavigation != null)
                {
                    result.Navigation.CurrentNavigation.StructuralNavigation.NavigationNodes.AddRange(
                        from n in source.Navigation.CurrentNavigation.StructuralNavigation.NavigationNode
                        select n.FromSchemaNavigationNodeToModelNavigationNodeV201605()
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
            if (template.Navigation != null)
            {
                result.Navigation = new V201605.Navigation
                {
                    GlobalNavigation =
                        template.Navigation.GlobalNavigation != null ?
                            new NavigationGlobalNavigation
                            {
                                NavigationType = (NavigationGlobalNavigationNavigationType)Enum.Parse(typeof(NavigationGlobalNavigationNavigationType), template.Navigation.GlobalNavigation.NavigationType.ToString()),
                                StructuralNavigation =
                                    template.Navigation.GlobalNavigation.StructuralNavigation != null ?
                                        new V201605.StructuralNavigation
                                        {
                                            RemoveExistingNodes = template.Navigation.GlobalNavigation.StructuralNavigation.RemoveExistingNodes,
                                            NavigationNode = (from n in template.Navigation.GlobalNavigation.StructuralNavigation.NavigationNodes
                                                              select n.FromModelNavigationNodeToSchemaNavigationNodeV201605()).ToArray()
                                        } : null,
                                ManagedNavigation =
                                    template.Navigation.GlobalNavigation.ManagedNavigation != null ?
                                        new V201605.ManagedNavigation
                                        {
                                            TermSetId = template.Navigation.GlobalNavigation.ManagedNavigation.TermSetId,
                                            TermStoreId = template.Navigation.GlobalNavigation.ManagedNavigation.TermStoreId,
                                        } : null
                            }
                                : null,
                    CurrentNavigation =
                        template.Navigation.CurrentNavigation != null ?
                            new NavigationCurrentNavigation
                            {
                                NavigationType = (NavigationCurrentNavigationNavigationType)Enum.Parse(typeof(NavigationCurrentNavigationNavigationType), template.Navigation.CurrentNavigation.NavigationType.ToString()),
                                StructuralNavigation =
                                    template.Navigation.CurrentNavigation.StructuralNavigation != null ?
                                        new V201605.StructuralNavigation
                                        {
                                            RemoveExistingNodes = template.Navigation.CurrentNavigation.StructuralNavigation.RemoveExistingNodes,
                                            NavigationNode = (from n in template.Navigation.CurrentNavigation.StructuralNavigation.NavigationNodes
                                                              select n.FromModelNavigationNodeToSchemaNavigationNodeV201605()).ToArray()
                                        } : null,
                                ManagedNavigation =
                                    template.Navigation.CurrentNavigation.ManagedNavigation != null ?
                                        new V201605.ManagedNavigation
                                        {
                                            TermSetId = template.Navigation.CurrentNavigation.ManagedNavigation.TermSetId,
                                            TermStoreId = template.Navigation.CurrentNavigation.ManagedNavigation.TermStoreId,
                                        } : null
                            }
                            : null
                };
            }
            return result;
        }
    }
}
