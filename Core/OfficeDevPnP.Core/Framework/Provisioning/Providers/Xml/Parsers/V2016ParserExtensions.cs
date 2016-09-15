using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    internal static class V2016ParserExtensions
    {
        public static V201605.CalendarType FromTemplateToSchemaCalendarTypeV201605(this Microsoft.SharePoint.Client.CalendarType calendarType)
        {
            switch (calendarType)
            {
                case Microsoft.SharePoint.Client.CalendarType.ChineseLunar:
                    return V201605.CalendarType.ChineseLunar;
                case Microsoft.SharePoint.Client.CalendarType.Gregorian:
                    return V201605.CalendarType.Gregorian;
                case Microsoft.SharePoint.Client.CalendarType.GregorianArabic:
                    return V201605.CalendarType.GregorianArabicCalendar;
                case Microsoft.SharePoint.Client.CalendarType.GregorianMEFrench:
                    return V201605.CalendarType.GregorianMiddleEastFrenchCalendar;
                case Microsoft.SharePoint.Client.CalendarType.GregorianXLITEnglish:
                    return V201605.CalendarType.GregorianTransliteratedEnglishCalendar;
                case Microsoft.SharePoint.Client.CalendarType.GregorianXLITFrench:
                    return V201605.CalendarType.GregorianTransliteratedFrenchCalendar;
                case Microsoft.SharePoint.Client.CalendarType.Hebrew:
                    return V201605.CalendarType.Hebrew;
                case Microsoft.SharePoint.Client.CalendarType.Hijri:
                    return V201605.CalendarType.Hijri;
                case Microsoft.SharePoint.Client.CalendarType.Japan:
                    return V201605.CalendarType.Japan;
                case Microsoft.SharePoint.Client.CalendarType.Korea:
                    return V201605.CalendarType.Korea;
                case Microsoft.SharePoint.Client.CalendarType.KoreaJapanLunar:
                    return V201605.CalendarType.KoreaandJapaneseLunar;
                case Microsoft.SharePoint.Client.CalendarType.SakaEra:
                    return V201605.CalendarType.SakaEra;
                case Microsoft.SharePoint.Client.CalendarType.Taiwan:
                    return V201605.CalendarType.Taiwan;
                case Microsoft.SharePoint.Client.CalendarType.Thai:
                    return V201605.CalendarType.Thai;
                case Microsoft.SharePoint.Client.CalendarType.UmAlQura:
                    return V201605.CalendarType.UmmalQura;
                case Microsoft.SharePoint.Client.CalendarType.None:
                default:
                    return V201605.CalendarType.None;
            }
        }

        public static Microsoft.SharePoint.Client.CalendarType FromSchemaToTemplateCalendarTypeV201605(this V201605.CalendarType calendarType)
        {
            switch (calendarType)
            {
                case V201605.CalendarType.ChineseLunar:
                    return Microsoft.SharePoint.Client.CalendarType.ChineseLunar;
                case V201605.CalendarType.Gregorian:
                    return Microsoft.SharePoint.Client.CalendarType.Gregorian;
                case V201605.CalendarType.GregorianArabicCalendar:
                    return Microsoft.SharePoint.Client.CalendarType.GregorianArabic;
                case V201605.CalendarType.GregorianMiddleEastFrenchCalendar:
                    return Microsoft.SharePoint.Client.CalendarType.GregorianMEFrench;
                case V201605.CalendarType.GregorianTransliteratedEnglishCalendar:
                    return Microsoft.SharePoint.Client.CalendarType.GregorianXLITEnglish;
                case V201605.CalendarType.GregorianTransliteratedFrenchCalendar:
                    return Microsoft.SharePoint.Client.CalendarType.GregorianXLITFrench;
                case V201605.CalendarType.Hebrew:
                    return Microsoft.SharePoint.Client.CalendarType.Hebrew;
                case V201605.CalendarType.Hijri:
                    return Microsoft.SharePoint.Client.CalendarType.Hijri;
                case V201605.CalendarType.Japan:
                    return Microsoft.SharePoint.Client.CalendarType.Japan;
                case V201605.CalendarType.Korea:
                    return Microsoft.SharePoint.Client.CalendarType.Korea;
                case V201605.CalendarType.KoreaandJapaneseLunar:
                    return Microsoft.SharePoint.Client.CalendarType.KoreaJapanLunar;
                case V201605.CalendarType.SakaEra:
                    return Microsoft.SharePoint.Client.CalendarType.SakaEra;
                case V201605.CalendarType.Taiwan:
                    return Microsoft.SharePoint.Client.CalendarType.Taiwan;
                case V201605.CalendarType.Thai:
                    return Microsoft.SharePoint.Client.CalendarType.Thai;
                case V201605.CalendarType.UmmalQura:
                    return Microsoft.SharePoint.Client.CalendarType.UmAlQura;
                case V201605.CalendarType.None:
                default:
                    return Microsoft.SharePoint.Client.CalendarType.None;
            }
        }

        public static V201605.WorkHour FromTemplateToSchemaWorkHourV201605(this Model.WorkHour workHour)
        {
            switch (workHour)
            {
                case Model.WorkHour.AM0100:
                    return V201605.WorkHour.Item100AM;
                case Model.WorkHour.AM0200:
                    return V201605.WorkHour.Item200AM;
                case Model.WorkHour.AM0300:
                    return V201605.WorkHour.Item300AM;
                case Model.WorkHour.AM0400:
                    return V201605.WorkHour.Item400AM;
                case Model.WorkHour.AM0500:
                    return V201605.WorkHour.Item500AM;
                case Model.WorkHour.AM0600:
                    return V201605.WorkHour.Item600AM;
                case Model.WorkHour.AM0700:
                    return V201605.WorkHour.Item700AM;
                case Model.WorkHour.AM0800:
                    return V201605.WorkHour.Item800AM;
                case Model.WorkHour.AM0900:
                    return V201605.WorkHour.Item900AM;
                case Model.WorkHour.AM1000:
                    return V201605.WorkHour.Item1000AM;
                case Model.WorkHour.AM1100:
                    return V201605.WorkHour.Item1100AM;
                case Model.WorkHour.AM1200:
                    return V201605.WorkHour.Item1200AM;
                case Model.WorkHour.PM0100:
                    return V201605.WorkHour.Item100PM;
                case Model.WorkHour.PM0200:
                    return V201605.WorkHour.Item200PM;
                case Model.WorkHour.PM0300:
                    return V201605.WorkHour.Item300PM;
                case Model.WorkHour.PM0400:
                    return V201605.WorkHour.Item400PM;
                case Model.WorkHour.PM0500:
                    return V201605.WorkHour.Item500PM;
                case Model.WorkHour.PM0600:
                    return V201605.WorkHour.Item600PM;
                case Model.WorkHour.PM0700:
                    return V201605.WorkHour.Item700PM;
                case Model.WorkHour.PM0800:
                    return V201605.WorkHour.Item800PM;
                case Model.WorkHour.PM0900:
                    return V201605.WorkHour.Item900PM;
                case Model.WorkHour.PM1000:
                    return V201605.WorkHour.Item1000PM;
                case Model.WorkHour.PM1100:
                    return V201605.WorkHour.Item1100PM;
                case Model.WorkHour.PM1200:
                    return V201605.WorkHour.Item1200PM;
                default:
                    return V201605.WorkHour.Item100AM;
            }
        }

        public static Model.WorkHour FromSchemaToTemplateWorkHourV201605(this V201605.WorkHour workHour)
        {
            switch (workHour)
            {
                case V201605.WorkHour.Item100AM:
                    return Model.WorkHour.AM0100;
                case V201605.WorkHour.Item200AM:
                    return Model.WorkHour.AM0200;
                case V201605.WorkHour.Item300AM:
                    return Model.WorkHour.AM0300;
                case V201605.WorkHour.Item400AM:
                    return Model.WorkHour.AM0400;
                case V201605.WorkHour.Item500AM:
                    return Model.WorkHour.AM0500;
                case V201605.WorkHour.Item600AM:
                    return Model.WorkHour.AM0600;
                case V201605.WorkHour.Item700AM:
                    return Model.WorkHour.AM0700;
                case V201605.WorkHour.Item800AM:
                    return Model.WorkHour.AM0800;
                case V201605.WorkHour.Item900AM:
                    return Model.WorkHour.AM0900;
                case V201605.WorkHour.Item1000AM:
                    return Model.WorkHour.AM1000;
                case V201605.WorkHour.Item1100AM:
                    return Model.WorkHour.AM1100;
                case V201605.WorkHour.Item1200AM:
                    return Model.WorkHour.AM1200;
                case V201605.WorkHour.Item100PM:
                    return Model.WorkHour.PM0100;
                case V201605.WorkHour.Item200PM:
                    return Model.WorkHour.PM0200;
                case V201605.WorkHour.Item300PM:
                    return Model.WorkHour.PM0300;
                case V201605.WorkHour.Item400PM:
                    return Model.WorkHour.PM0400;
                case V201605.WorkHour.Item500PM:
                    return Model.WorkHour.PM0500;
                case V201605.WorkHour.Item600PM:
                    return Model.WorkHour.PM0600;
                case V201605.WorkHour.Item700PM:
                    return Model.WorkHour.PM0700;
                case V201605.WorkHour.Item800PM:
                    return Model.WorkHour.PM0800;
                case V201605.WorkHour.Item900PM:
                    return Model.WorkHour.PM0900;
                case V201605.WorkHour.Item1000PM:
                    return Model.WorkHour.PM1000;
                case V201605.WorkHour.Item1100PM:
                    return Model.WorkHour.PM1100;
                case V201605.WorkHour.Item1200PM:
                    return Model.WorkHour.PM1200;
                default:
                    return Model.WorkHour.AM0100;
            }
        }

        public static V201605.AuditSettingsAudit[] FromTemplateToSchemaAuditsV201605(this Microsoft.SharePoint.Client.AuditMaskType audits)
        {
            List<V201605.AuditSettingsAudit> result = new List<AuditSettingsAudit>();
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.All))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.All });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.CheckIn))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.CheckIn });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.CheckOut))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.CheckOut });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.ChildDelete))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.ChildDelete });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.Copy))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.Copy });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.Move))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.Move });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.None))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.None });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.ObjectDelete))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.ObjectDelete });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.ProfileChange))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.ProfileChange });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.SchemaChange))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.SchemaChange });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.Search))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.Search });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.SecurityChange))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.SecurityChange });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.Undelete))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.Undelete });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.Update))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.Update });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.View))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.View });
            }
            if (audits.HasFlag(Microsoft.SharePoint.Client.AuditMaskType.Workflow))
            {
                result.Add(new AuditSettingsAudit { AuditFlag = AuditSettingsAuditAuditFlag.Workflow });
            }

            return result.ToArray();
        }

        public static Model.ObjectSecurity FromSchemaToTemplateObjectSecurityV201605(this V201605.ObjectSecurity objectSecurity)
        {
            return ((objectSecurity != null && objectSecurity.BreakRoleInheritance != null) ?
                new Model.ObjectSecurity(
                    objectSecurity.BreakRoleInheritance.RoleAssignment != null ?
                        (from ra in objectSecurity.BreakRoleInheritance.RoleAssignment
                         select new Model.RoleAssignment
                         {
                             Principal = ra.Principal,
                             RoleDefinition = ra.RoleDefinition,
                         }) : null
                    )
                {
                    ClearSubscopes = objectSecurity.BreakRoleInheritance.ClearSubscopes,
                    CopyRoleAssignments = objectSecurity.BreakRoleInheritance.CopyRoleAssignments,
                } : null);
        }

        public static V201605.ObjectSecurity FromTemplateToSchemaObjectSecurityV201605(this Model.ObjectSecurity objectSecurity)
        {
            return ((objectSecurity != null && (objectSecurity.ClearSubscopes == true || objectSecurity.CopyRoleAssignments == true || objectSecurity.RoleAssignments.Count > 0)) ?
                new V201605.ObjectSecurity
                {
                    BreakRoleInheritance = new V201605.ObjectSecurityBreakRoleInheritance
                    {
                        ClearSubscopes = objectSecurity.ClearSubscopes,
                        CopyRoleAssignments = objectSecurity.CopyRoleAssignments,
                        RoleAssignment = (objectSecurity.RoleAssignments != null && objectSecurity.RoleAssignments.Count > 0) ?
                            (from ra in objectSecurity.RoleAssignments
                             select new V201605.RoleAssignment
                             {
                                 Principal = ra.Principal,
                                 RoleDefinition = ra.RoleDefinition,
                             }).ToArray() : null,
                    }
                } : null);
        }

        public static Model.Folder FromSchemaToTemplateFolderV201605(this V201605.Folder folder)
        {
            Model.Folder result = new Model.Folder(folder.Name, null, folder.Security.FromSchemaToTemplateObjectSecurityV201605());
            if (folder.Folder1 != null && folder.Folder1.Length > 0)
            {
                result.Folders.AddRange(from child in folder.Folder1 select child.FromSchemaToTemplateFolderV201605());
            }
            return (result);
        }

        public static V201605.Folder FromTemplateToSchemaFolderV201605(this Model.Folder folder)
        {
            V201605.Folder result = new V201605.Folder();
            result.Name = folder.Name;
            result.Security = folder.Security.FromTemplateToSchemaObjectSecurityV201605();
            result.Folder1 = folder.Folders != null ? (from child in folder.Folders select child.FromTemplateToSchemaFolderV201605()).ToArray() : null;
            return (result);
        }

        public static string FromBasePermissionsToStringV201605(this BasePermissions basePermissions)
        {
            List<string> permissions = new List<string>();
            foreach (var pk in (PermissionKind[])Enum.GetValues(typeof(PermissionKind)))
            {
                if (basePermissions.Has(pk) && pk != PermissionKind.EmptyMask)
                {
                    permissions.Add(pk.ToString());
                }
            }
            return string.Join(",", permissions.ToArray());
        }

        public static BasePermissions ToBasePermissionsV201605(this string basePermissionString)
        {
            BasePermissions bp = new BasePermissions();

            // Is it an int value (for backwards compability)?
            int permissionInt;
            if (int.TryParse(basePermissionString, out permissionInt))
            {
                bp.Set((PermissionKind)permissionInt);
            }
            else if (!string.IsNullOrEmpty(basePermissionString))
            {
                foreach (var pk in basePermissionString.Split(','))
                {
                    PermissionKind permissionKind;
                    if (Enum.TryParse<PermissionKind>(pk, out permissionKind))
                    {
                        bp.Set(permissionKind);
                    }
                }
            }
            return bp;
        }

        public static Model.NavigationNode FromSchemaNavigationNodeToModelNavigationNodeV201605(
            this V201605.NavigationNode node)
        {
            var result = new Model.NavigationNode
            {
                IsExternal = node.IsExternal,
                Title = node.Title,
                Url = node.Url,
            };

            if (node.ChildNodes != null && node.ChildNodes.Length > 0)
            {
                result.NavigationNodes.AddRange(
                    (from n in node.ChildNodes
                     select n.FromSchemaNavigationNodeToModelNavigationNodeV201605()));
            }

            return (result);
        }

        public static V201605.NavigationNode FromModelNavigationNodeToSchemaNavigationNodeV201605(
            this Model.NavigationNode node)
        {
            var result = new V201605.NavigationNode
            {
                IsExternal = node.IsExternal,
                Title = node.Title,
                Url = node.Url,
                ChildNodes = (from n in node.NavigationNodes
                              select n.FromModelNavigationNodeToSchemaNavigationNodeV201605()).ToArray()
            };

            return (result);
        }
    }
}
