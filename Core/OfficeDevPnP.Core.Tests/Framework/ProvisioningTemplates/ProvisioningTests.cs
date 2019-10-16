#if !ONPREMISES
using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using OfficeDevPnP.Core.Framework.Provisioning.Model.Drive;
using OfficeDevPnP.Core.Framework.Provisioning.Model.Office365Groups;
using OfficeDevPnP.Core.Framework.Provisioning.Model.SPUPS;
using OfficeDevPnP.Core.Framework.Provisioning.ObjectHandlers;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Resolvers;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OfficeDevPnP.Core.Tests.Framework.ProvisioningTemplates
{
    [TestClass]
    public class ProvisioningTests
    {

        [TestMethod]
        public void GetGroupInfoTest()
        {
            using (var context = TestCommon.CreateClientContext())
            {
                OfficeDevPnP.Core.Sites.SiteCollection.GetGroupInfo(context, "demo1").GetAwaiter().GetResult();
            }
        }

        [TestMethod]
        public void CanDeserializeProvisioningTemplateJson()
        {
            using (var context = TestCommon.CreateClientContext())
            {
                var resourceFolder = string.Format(@"{0}\..\..\Resources\Templates", AppDomain.CurrentDomain.BaseDirectory);
                XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(resourceFolder, "");

                var existingTemplate = provider.GetTemplate("ProvisioningSchema-2019-09-FullSample-01.xml");

                var serializerOutOptions = new JsonSerializerOptions();
                serializerOutOptions.IgnoreNullValues = true;
                serializerOutOptions.IgnoreReadOnlyProperties = true;
                serializerOutOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                serializerOutOptions.Converters.Add(new AlternateUICultureCollectionConverter());
                serializerOutOptions.Converters.Add(new SupportedUILanguageCollectionConverter());
                serializerOutOptions.Converters.Add(new SiteDesignRightConverter());
                serializerOutOptions.Converters.Add(new JsonStringEnumConverter());
                serializerOutOptions.Converters.Add(new UserCollectionConverter());
                serializerOutOptions.Converters.Add(new FieldCollectionConverter());
                serializerOutOptions.Converters.Add(new XElementConverter());
                serializerOutOptions.Converters.Add(new BasePermissionsConverter());
                serializerOutOptions.Converters.Add(new ProvisioningTemplateObjectCollectionConverterFactory());
                serializerOutOptions.Converters.Add(new ProvisioningHierarchyObjectCollectionConverterFactory());
                var jsonTemplateText = JsonSerializer.Serialize(existingTemplate, serializerOutOptions);

                var serializerInOptions = new JsonSerializerOptions();
                serializerInOptions.IgnoreNullValues = true;
                serializerInOptions.IgnoreReadOnlyProperties = true;
                serializerInOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                serializerInOptions.Converters.Add(new AlternateUICultureCollectionConverter());
                serializerInOptions.Converters.Add(new SupportedUILanguageCollectionConverter());
                serializerInOptions.Converters.Add(new SiteDesignRightConverter());
                serializerInOptions.Converters.Add(new JsonStringEnumConverter());
                serializerInOptions.Converters.Add(new UserCollectionConverter());
                serializerInOptions.Converters.Add(new FieldCollectionConverter());
                serializerInOptions.Converters.Add(new XElementConverter());
                serializerInOptions.Converters.Add(new BasePermissionsConverter());
                serializerInOptions.Converters.Add(new ProvisioningTemplateObjectCollectionConverterFactory());
                serializerInOptions.Converters.Add(new ProvisioningHierarchyObjectCollectionConverterFactory());
                var template = JsonSerializer.Deserialize<ProvisioningTemplate>(jsonTemplateText, serializerInOptions);
            }
        }

        [TestMethod]
        public void CanSerializeJson()
        {
            using (var context = TestCommon.CreateClientContext())
            {
                var resourceFolder = string.Format(@"{0}\..\..\Resources\Templates", AppDomain.CurrentDomain.BaseDirectory);
                XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(resourceFolder, "");

                var existingTemplate = provider.GetHierarchy("ProvisioningSchema-2019-09-FullSample-01.xml");
                existingTemplate.Schema = "file:///c:/repos/pnp-sites-core/core/officedevpnp.core/framework/provisioning/providers/json/schemas/201909/tenant.schema.json";
                //existingTemplate.Schema = TenantSchema;
                var serializerOptions = new JsonSerializerOptions();
                serializerOptions.IgnoreNullValues = true;
                serializerOptions.IgnoreReadOnlyProperties = true;
                serializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                serializerOptions.Converters.Add(new AlternateUICultureCollectionConverter());
                serializerOptions.Converters.Add(new SupportedUILanguageCollectionConverter());
                serializerOptions.Converters.Add(new SiteDesignRightConverter());
                serializerOptions.Converters.Add(new JsonStringEnumConverter());
                serializerOptions.Converters.Add(new UserCollectionConverter());
                serializerOptions.Converters.Add(new FieldCollectionConverter());
                serializerOptions.Converters.Add(new XElementConverter());
                serializerOptions.Converters.Add(new BasePermissionsConverter());
                var jsonString = JsonSerializer.Serialize(existingTemplate, serializerOptions);
            }
        }

        [TestMethod]
        public void ProvisionTenantTemplate()
        {
            var resourceFolder = string.Format(@"{0}\..\..\Resources\Templates", AppDomain.CurrentDomain.BaseDirectory);
            XMLTemplateProvider provider = new XMLFileSystemTemplateProvider(resourceFolder, "");

            var existingTemplate = provider.GetTemplate("ProvisioningSchema-2018-07-FullSample-01.xml");

            Guid siteGuid = Guid.NewGuid();
            int siteId = siteGuid.GetHashCode();
            var template = new ProvisioningTemplate();
            template.Id = "TestTemplate";
            template.Lists.Add(new ListInstance()
            {
                Title = "Testlist",
                TemplateType = 100,
                Url = "lists/testlist"
            });

            template.TermGroups.AddRange(existingTemplate.TermGroups);

            ProvisioningHierarchy hierarchy = new ProvisioningHierarchy();

            hierarchy.Templates.Add(template);

            hierarchy.Parameters.Add("CompanyName", "Contoso");

            var sequence = new ProvisioningSequence();

            sequence.TermStore = new ProvisioningTermStore();
            var termGroup = new TermGroup() { Name = "Contoso TermGroup" };
            var termSet = new TermSet() { Name = "Projects", Id = Guid.NewGuid(), IsAvailableForTagging = true, Language = 1033 };
            var term = new Term() { Name = "Contoso Term" };

            termSet.Terms.Add(term);
            // termGroup.TermSets.Add(termSet);

            var existingTermSet = existingTemplate.TermGroups[0].TermSets[0];
            termGroup.TermSets.Add(existingTermSet);

            // sequence.TermStore.TermGroups.Add(termGroup);

            var teamSite1 = new TeamSiteCollection()
            {
                //  Alias = $"prov-1-{siteId}",
                Alias = "prov-1",
                Description = "prov-1",
                DisplayName = "prov-1",
                IsHubSite = false,
                IsPublic = false,
                Title = "prov-1",
            };
            teamSite1.Templates.Add("TestTemplate");

            var subsite = new TeamNoGroupSubSite()
            {
                Description = "Test Sub",
                Url = "testsub1",
                Language = 1033,
                TimeZoneId = 4,
                Title = "Test Sub",
                UseSamePermissionsAsParentSite = true
            };
            subsite.Templates.Add("TestTemplate");
            teamSite1.Sites.Add(subsite);

            sequence.SiteCollections.Add(teamSite1);

            var teamSite2 = new TeamSiteCollection()
            {
                Alias = $"prov-2-{siteId}",
                Description = "prov-2",
                DisplayName = "prov-2",
                IsHubSite = false,
                IsPublic = false,
                Title = "prov-2"
            };
            teamSite2.Templates.Add("TestTemplate");

            sequence.SiteCollections.Add(teamSite2);

            hierarchy.Sequences.Add(sequence);


            using (var tenantContext = TestCommon.CreateTenantClientContext())
            {
                var applyingInformation = new ProvisioningTemplateApplyingInformation();
                applyingInformation.ProgressDelegate = (message, step, total) =>
                {
                    if (message != null)
                    {


                    }
                };

                var tenant = new Tenant(tenantContext);

                tenant.ApplyProvisionHierarchy(hierarchy, sequence.ID, applyingInformation);
            }
        }
    }
}
#endif