using System.Collections.Generic;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model.Configuration.Lists.Lists
{

    public class ExtractQueryConfiguration
    {
        public string CamlQuery { get; set; }

        public int RowLimit { get; set; }

        public List<string> ViewFields { get; set; }

        public bool IncludeAttachments { get; set; }

        public int PageSize { get; set; }
    }
}
