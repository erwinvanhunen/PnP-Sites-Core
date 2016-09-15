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
        Sequence = 150)]
    internal class TaxonomyParser : IBaseElementParser
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
            if (source.TermGroups != null)
            {
                result.TermGroups.AddRange(
                    from termGroup in source.TermGroups
                    select new Model.TermGroup(
                        !string.IsNullOrEmpty(termGroup.ID) ? Guid.Parse(termGroup.ID) : Guid.Empty,
                        termGroup.Name,
                        new List<Model.TermSet>(
                            from termSet in termGroup.TermSets
                            select new Model.TermSet(
                                !string.IsNullOrEmpty(termSet.ID) ? Guid.Parse(termSet.ID) : Guid.Empty,
                                termSet.Name,
                                termSet.LanguageSpecified ? (int?)termSet.Language : null,
                                termSet.IsAvailableForTagging,
                                termSet.IsOpenForTermCreation,
                                termSet.Terms != null ? FromSchemaTermsToModelTermsV201605(termSet.Terms) : null,
                                termSet.CustomProperties?.ToDictionary(k => k.Key, v => v.Value))
                            {
                                Description = termSet.Description,
                            }),
                        termGroup.SiteCollectionTermGroup,
                        termGroup.Contributors != null ? (from c in termGroup.Contributors
                                                          select new Model.User { Name = c.Name }).ToArray() : null,
                        termGroup.Managers != null ? (from m in termGroup.Managers
                                                      select new Model.User { Name = m.Name }).ToArray() : null
                        )
                    {
                        Description = termGroup.Description,
                    });
            }

            return result;
        }

        private static V201605.Term[] FromModelTermsToSchemaTermsV201605(TermCollection terms)
        {
            V201605.Term[] result = terms.Count > 0 ? (
                from term in terms
                select new V201605.Term
                {
                    ID = term.Id != Guid.Empty ? term.Id.ToString() : null,
                    Name = term.Name,
                    Description = term.Description,
                    Owner = term.Owner,
                    LanguageSpecified = term.Language.HasValue,
                    Language = term.Language.HasValue ? term.Language.Value : 1033,
                    IsAvailableForTagging = term.IsAvailableForTagging,
                    IsDeprecated = term.IsDeprecated,
                    IsReused = term.IsReused,
                    IsSourceTerm = term.IsSourceTerm,
                    SourceTermId = term.SourceTermId != Guid.Empty ? term.SourceTermId.ToString() : null,
                    CustomSortOrder = term.CustomSortOrder,
                    Terms = term.Terms.Count > 0 ? new V201605.TermTerms { Items = FromModelTermsToSchemaTermsV201605(term.Terms) } : null,
                    CustomProperties = term.Properties.Count > 0 ?
                        (from p in term.Properties
                         select new V201605.StringDictionaryItem
                         {
                             Key = p.Key,
                             Value = p.Value
                         }).ToArray() : null,
                    LocalCustomProperties = term.LocalProperties.Count > 0 ?
                        (from p in term.LocalProperties
                         select new V201605.StringDictionaryItem
                         {
                             Key = p.Key,
                             Value = p.Value
                         }).ToArray() : null,
                    Labels = term.Labels.Count > 0 ?
                        (from l in term.Labels
                         select new V201605.TermLabelsLabel
                         {
                             Language = l.Language,
                             IsDefaultForLanguage = l.IsDefaultForLanguage,
                             Value = l.Value,
                         }).ToArray() : null,
                }).ToArray() : null;

            return (result);
        }

        private static List<Model.Term> FromSchemaTermsToModelTermsV201605(V201605.Term[] terms)
        {
            List<Model.Term> result = new List<Model.Term>(
                from term in terms
                select new Model.Term(
                    !string.IsNullOrEmpty(term.ID) ? Guid.Parse(term.ID) : Guid.Empty,
                    term.Name,
                    term.LanguageSpecified ? term.Language : (int?)null,
                    (term.Terms != null && term.Terms.Items != null) ? FromSchemaTermsToModelTermsV201605(term.Terms.Items) : null,
                    term.Labels != null ?
                    (new List<Model.TermLabel>(
                        from label in term.Labels
                        select new Model.TermLabel
                        {
                            Language = label.Language,
                            Value = label.Value,
                            IsDefaultForLanguage = label.IsDefaultForLanguage
                        }
                    )) : null,
                    term.CustomProperties != null ? term.CustomProperties.ToDictionary(k => k.Key, v => v.Value) : null,
                    term.LocalCustomProperties != null ? term.LocalCustomProperties.ToDictionary(k => k.Key, v => v.Value) : null
                    )
                {
                    CustomSortOrder = term.CustomSortOrder,
                    IsAvailableForTagging = term.IsAvailableForTagging,
                    IsReused = term.IsReused,
                    IsSourceTerm = term.IsSourceTerm,
                    SourceTermId = !String.IsNullOrEmpty(term.SourceTermId) ? new Guid(term.SourceTermId) : Guid.Empty,
                    IsDeprecated = term.IsDeprecated,
                    Owner = term.Owner,
                }
                );

            return (result);
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

            // Translate Taxonomy elements, if any
            if (template.TermGroups != null && template.TermGroups.Count > 0)
            {
                result.TermGroups =
                    (from grp in template.TermGroups
                     select new V201605.TermGroup
                     {
                         Name = grp.Name,
                         ID = grp.Id != Guid.Empty ? grp.Id.ToString() : null,
                         Description = grp.Description,
                         SiteCollectionTermGroup = grp.SiteCollectionTermGroup,
                         SiteCollectionTermGroupSpecified = grp.SiteCollectionTermGroup,
                         Contributors = (from c in grp.Contributors
                                         select new V201605.User { Name = c.Name }).ToArray(),
                         Managers = (from m in grp.Managers
                                     select new V201605.User { Name = m.Name }).ToArray(),
                         TermSets = (
                            from termSet in grp.TermSets
                            select new V201605.TermSet
                            {
                                ID = termSet.Id != Guid.Empty ? termSet.Id.ToString() : null,
                                Name = termSet.Name,
                                IsAvailableForTagging = termSet.IsAvailableForTagging,
                                IsOpenForTermCreation = termSet.IsOpenForTermCreation,
                                Description = termSet.Description,
                                Language = termSet.Language.HasValue ? termSet.Language.Value : 0,
                                LanguageSpecified = termSet.Language.HasValue,
                                Terms = FromModelTermsToSchemaTermsV201605(termSet.Terms),
                                CustomProperties = termSet.Properties.Count > 0 ?
                                     (from p in termSet.Properties
                                      select new V201605.StringDictionaryItem
                                      {
                                          Key = p.Key,
                                          Value = p.Value
                                      }).ToArray() : null,
                            }).ToArray(),
                     }).ToArray();
            }
            return result;
        }
    }
}
