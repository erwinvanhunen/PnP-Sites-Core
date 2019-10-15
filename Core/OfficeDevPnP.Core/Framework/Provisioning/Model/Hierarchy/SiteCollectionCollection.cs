using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    /// <summary>
    /// Collection of SiteCollection items
    /// </summary>
    [JsonConverter(typeof(SiteCollectionCollectionConverter))]
    public partial class SiteCollectionCollection : BaseProvisioningHierarchyObjectCollection<SiteCollection>
    {
        /// <summary>
        /// Constructor for SiteCollectionCollection class
        /// </summary>
        /// <param name="parentHierarchy">Parent Provisioning object</param>
        public SiteCollectionCollection(ProvisioningHierarchy parentHierarchy) :
            base(parentHierarchy)
        {
        }
    }
}
