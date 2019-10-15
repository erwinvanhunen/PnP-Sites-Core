using OfficeDevPnP.Core.Framework.Provisioning.Providers.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OfficeDevPnP.Core.Framework.Provisioning.Model
{
    /// <summary>
    /// The Audit Settings for the Provisioning Template
    /// </summary>
    public partial class AuditSettings : BaseModel, IEquatable<AuditSettings>
    {
        #region Public Members

        /// <summary>
        /// Audit Flags configured for the Site
        /// </summary>
        [JsonConverter(typeof(AuditMaskTypeConverter))] // override of JsonStringEnumConverter
        public Microsoft.SharePoint.Client.AuditMaskType AuditFlags { get; set; }

        /// <summary>
        /// The Audit Log Trimming Retention for Audits
        /// </summary>
        [DefaultValue(90)]
        public Int32 AuditLogTrimmingRetention { get; set; }

        /// <summary>
        /// A flag to enable Audit Log Trimming
        /// </summary>
        [DefaultValue(true)]
        public Boolean TrimAuditLog { get; set; }

        #endregion

        #region Comparison code

        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns>Returns HashCode</returns>
        public override int GetHashCode()
        {
            return (String.Format("{0}|{1}|{2}|",
                this.AuditFlags.GetHashCode(),
                this.AuditLogTrimmingRetention.GetHashCode(),
                this.TrimAuditLog.GetHashCode()
            ).GetHashCode());
        }

        /// <summary>
        /// Compares object with AuditSettings
        /// </summary>
        /// <param name="obj">Object that represents AuditSettings</param>
        /// <returns>true if the current object is equal to the AuditSettings</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is AuditSettings))
            {
                return (false);
            }
            return (Equals((AuditSettings)obj));
        }

        /// <summary>
        /// Compares AuditSetting object based on AuditFlags, AuditLogTrimmingRetention and TrimAuditLog properties.
        /// </summary>
        /// <param name="other">AuditSettings object</param>
        /// <returns>true if the AuditSettings object is equal to the current object; otherwise, false.</returns>
        public bool Equals(AuditSettings other)
        {
            if (other == null)
            {
                return (false);
            }

            return (this.AuditFlags == other.AuditFlags  &&
                this.AuditLogTrimmingRetention == other.AuditLogTrimmingRetention &&
                this.TrimAuditLog == other.TrimAuditLog
                );
        }

        #endregion
    }
}
