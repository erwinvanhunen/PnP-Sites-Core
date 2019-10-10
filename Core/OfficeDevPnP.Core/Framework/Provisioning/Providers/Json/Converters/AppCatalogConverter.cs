using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters
{
    internal class AppCatalogConverter : JsonConverter<AppCatalog>
    {
        public override AppCatalog ReadJson(JsonReader reader, Type objectType, AppCatalog existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (existingValue == null)
            {
                existingValue = new AppCatalog();
            }
            var packages = serializer.Deserialize<JArray>(reader);
            foreach (var package in packages)
            {
                existingValue.Packages.Add(new Package()
                {
                    Src = package.Value<string>("src"),
                    Action = (PackageAction)Enum.Parse(typeof(PackageAction), package.Value<string>("action")),
                    Overwrite = package.Value<bool>("overwrite"),
                    PackageId = package.Value<string>("packageid"),
                    SkipFeatureDeployment = package.Value<bool>("skipFeatureDeployment")
                });
            }
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, AppCatalog value, JsonSerializer serializer)
        {
            List<Package> packages = new List<Package>();
            foreach (var package in value.Packages)
            {
                packages.Add(package);
            }
            serializer.Serialize(writer, packages);
        }
    }
}
