using OfficeDevPnP.Core.Framework.Provisioning.Model.Configuration.Lists.Lists;
using System.Collections.Generic;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model.Configuration.Lists
{
    public class ExtractConfiguration
    {
        public bool IncludeHiddenLists { get; set; }

        public List<Lists.ExtractConfiguration> Lists { get; set; }

        public bool HasLists
        {
            get
            {
                return this.Lists != null && this.Lists.Count > 0;
            }
        }
    }
}
