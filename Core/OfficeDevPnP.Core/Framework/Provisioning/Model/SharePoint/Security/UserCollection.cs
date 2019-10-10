using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    /// <summary>
    /// Collection of User objects
    /// </summary>
    [JsonConverter(typeof(UserCollectionConverter))]
    public partial class UserCollection : BaseProvisioningTemplateObjectCollection<User>
    {
        /// <summary>
        /// Constructor for UserCollection class
        /// </summary>
        /// <param name="parentTemplate">Parent provisioning template</param>
        public UserCollection(ProvisioningTemplate parentTemplate) : base(parentTemplate)
        {

        }
    }
}
