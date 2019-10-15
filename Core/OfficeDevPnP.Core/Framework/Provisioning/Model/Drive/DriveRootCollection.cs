using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model.Drive
{
    /// <summary>
    /// Collection of DriveRoot items
    /// </summary>
    public partial class DriveRootCollection : BaseProvisioningHierarchyObjectCollection<DriveRoot>
    {
        /// <summary>
        /// Constructor for DriveRootCollection class
        /// </summary>
        /// <param name="parentTemplate">Parent provisioning template</param>
        public DriveRootCollection(ProvisioningHierarchy parentTemplate) :
            base(parentTemplate)
        {
        }

        public DriveRootCollection() { }
    }
}
