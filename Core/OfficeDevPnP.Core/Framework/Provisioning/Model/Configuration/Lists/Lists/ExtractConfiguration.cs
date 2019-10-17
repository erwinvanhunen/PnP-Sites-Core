
namespace OfficeDevPnP.Core.Framework.Provisioning.Model.Configuration.Lists.Lists
{
    public class ExtractConfiguration
    {
        public string Title { get; set; }

        public bool IncludeItems { get; set; }

        public bool SkipEmptyFields { get; set; }

        public ExtractQueryConfiguration Query { get; set; }

        public bool RemoveExistingContentTypes { get; set; }

    }
}
