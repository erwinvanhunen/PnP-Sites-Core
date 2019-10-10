using Newtonsoft.Json;
using OfficeDevPnP.Core.Framework.Provisioning.Connectors;
using System.Collections.Generic;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    /// <summary>
    /// Defines the root node of the Provisioning Domain Model
    /// </summary>
    public partial class ProvisioningHierarchy
    {
        #region Constructors

        public ProvisioningHierarchy()
        {
            this.Templates = new ProvisioningTemplateCollection(this);
            this.Sequences = new ProvisioningSequenceCollection(this);
            this.Localizations = new LocalizationCollection(null);
            this.Tenant = new ProvisioningTenant();
            this.Teams = new Teams.ProvisioningTeams();
            this.AzureActiveDirectory = new AzureActiveDirectory.ProvisioningAzureActiveDirectory();
            this.Drive = new Drive.Drive();
            this.ProvisioningWebhooks = new ProvisioningWebhookCollection(null);
        }

        #endregion

        #region Public Members

        [JsonProperty("$schema")]
        internal string Schema { get; set; }
        /// <summary>
        /// Any parameters that can be used throughout the template
        /// </summary>
        public Dictionary<string, string> Parameters { get; internal set; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the Localizations
        /// </summary>
        [JsonProperty("localizations", NullValueHandling= NullValueHandling.Ignore)]
        public LocalizationCollection Localizations { get; internal set; }

        /// <summary>
        /// The Tenant-wide settings for the template
        /// </summary>
        [JsonProperty("tenant", NullValueHandling = NullValueHandling.Ignore)]
        public ProvisioningTenant Tenant { get; set; }

        /// <summary>
        /// Gets or sets the Provisioning File Version number
        /// </summary>
        public double Version { get; set; }

        /// <summary>
        /// Gets or sets the Provisioning File Author name
        /// </summary>
        [JsonProperty("author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the Name of the tool generating this Provisioning File
        /// </summary>
        [JsonProperty("generator", NullValueHandling = NullValueHandling.Ignore)]
        public string Generator { get; set; }

        /// <summary>
        /// The Description of the Provisioning File
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// The Display Name of the Provisioning File
        /// </summary>
        [JsonProperty("displayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The Image Preview Url of the Provisioning File
        /// </summary>
        [JsonProperty("imagePreviewUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string ImagePreviewUrl { get; set; }

        /// <summary>
        /// The Connector which can be used to retrieve or save related artifacts
        /// </summary>
        [JsonIgnore]
        public FileConnectorBase Connector { get; set; }

        /// <summary>
        /// A collection of Provisioning Template objects, if any
        /// </summary>
        [JsonProperty("templates", NullValueHandling = NullValueHandling.Ignore)]
        public ProvisioningTemplateCollection Templates { get; private set; }

        /// <summary>
        /// A collection of Provisioning Sequence objects, if any
        /// </summary>
        [JsonProperty("sequences", NullValueHandling = NullValueHandling.Ignore)]
        public ProvisioningSequenceCollection Sequences { get; private set; }

        /// <summary>
        /// Settings for provisioning Teams objects, if any
        /// </summary>
        [JsonProperty("teams", NullValueHandling = NullValueHandling.Ignore)]
        public Teams.ProvisioningTeams Teams { get; private set; }

        /// <summary>
        /// Settings for provisioning Azure Active Directory objects, if any
        /// </summary>
        public AzureActiveDirectory.ProvisioningAzureActiveDirectory AzureActiveDirectory { get; private set; }

        /// <summary>
        /// Settings for provisioning Drive objects, if any
        /// </summary>
        public Drive.Drive Drive { get; private set; }

        /// <summary>
        /// A collection of Provisioning Webhooks
        /// </summary>
        public ProvisioningWebhookCollection ProvisioningWebhooks { get; private set; }


        #endregion
    }
}
