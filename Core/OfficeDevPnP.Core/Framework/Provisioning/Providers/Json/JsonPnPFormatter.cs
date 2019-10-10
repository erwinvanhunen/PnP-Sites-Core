using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Resolvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json
{
    public class JsonPnPFormatter : ITemplateFormatter
    {
        private TemplateProviderBase _provider;

        public static string TenantSchema => "https://www.sharepointpnp.com/schema/provisioning_tenant01.schema.json";
        public static string SiteSchema => "https://www.sharepointpnp.com/schema/provisioning_site01.schema.json";

        public void Initialize(TemplateProviderBase provider)
        {
            this._provider = provider;
        }

        public bool IsValid(System.IO.Stream template)
        {
            // We do not provide JSON validation capabilities
            return (true);
        }

        public System.IO.Stream ToFormattedTemplate(Model.ProvisioningTemplate template)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new NullToDefaultConverter<Guid>());
            serializerSettings.ContractResolver = new IgnoreEmptyEnumerableResolver();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(template, serializerSettings);

            var jsonBytes = Encoding.Unicode.GetBytes(jsonString);
            MemoryStream jsonStream = new MemoryStream(jsonBytes);
            jsonStream.Position = 0;

            return (jsonStream);
        }

        public System.IO.Stream ToFormattedHierarchy(Model.ProvisioningHierarchy hierarchy)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new NullToDefaultConverter<Guid>());
            serializerSettings.ContractResolver = new IgnoreEmptyEnumerableResolver();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            var jsonString = JsonConvert.SerializeObject(hierarchy, serializerSettings);

            var jsonBytes = Encoding.Unicode.GetBytes(jsonString);
            MemoryStream jsonStream = new MemoryStream(jsonBytes);
            jsonStream.Position = 0;

            return (jsonStream);
        }

        public Model.ProvisioningTemplate ToProvisioningTemplate(System.IO.Stream template)
        {
            return (this.ToProvisioningTemplate(template, null));
        }

        public Model.ProvisioningTemplate ToProvisioningTemplate(System.IO.Stream template, string identifier)
        {
            StreamReader sr = new StreamReader(template, Encoding.Unicode);
            String jsonString = sr.ReadToEnd();

            var result = JsonConvert.DeserializeObject<Model.ProvisioningTemplate>(jsonString, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            //Model.ProvisioningTemplate result = JsonConvert.DeserializeObject<Model.ProvisioningTemplate>(jsonString);
            return (result);
        }
    }
}
