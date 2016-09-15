using System;
using System.Linq;
using System.Xml.Linq;
using OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.V201605;
using ProvisioningTemplate = OfficeDevPnP.Core.Framework.Provisioning.Model.ProvisioningTemplate;

namespace OfficeDevPnP.Core.Framework.Provisioning.Providers.Xml.Parsers
{
    [BaseElementParser(
        SupportedSchemas = XMLPnPSchemaVersion.V201605,
        Sequence = 60)]
    internal class SecurityParser : IBaseElementParser
    {
        public ProvisioningTemplate ParseElement(XMLPnPSchemaVersion schema, ProvisioningTemplate outgoingTemplate, IProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var source = incomingTemplate as V201605.ProvisioningTemplate;
                        outgoingTemplate = Parse201605Element(outgoingTemplate, source);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }
            return outgoingTemplate;
        }

        private static ProvisioningTemplate Parse201605Element(ProvisioningTemplate result, V201605.ProvisioningTemplate source)
        {
            if (source.Security != null)
            {
                result.Security.BreakRoleInheritance = source.Security.BreakRoleInheritance;
                result.Security.CopyRoleAssignments = source.Security.CopyRoleAssignments;
                result.Security.ClearSubscopes = source.Security.ClearSubscopes;

                if (source.Security.AdditionalAdministrators != null)
                {
                    result.Security.AdditionalAdministrators.AddRange(
                        from user in source.Security.AdditionalAdministrators
                        select new Model.User
                        {
                            Name = user.Name,
                        });
                }
                if (source.Security.AdditionalOwners != null)
                {
                    result.Security.AdditionalOwners.AddRange(
                        from user in source.Security.AdditionalOwners
                        select new Model.User
                        {
                            Name = user.Name,
                        });
                }
                if (source.Security.AdditionalMembers != null)
                {
                    result.Security.AdditionalMembers.AddRange(
                        from user in source.Security.AdditionalMembers
                        select new Model.User
                        {
                            Name = user.Name,
                        });
                }
                if (source.Security.AdditionalVisitors != null)
                {
                    result.Security.AdditionalVisitors.AddRange(
                        from user in source.Security.AdditionalVisitors
                        select new Model.User
                        {
                            Name = user.Name,
                        });
                }
                if (source.Security.SiteGroups != null)
                {
                    result.Security.SiteGroups.AddRange(
                        from g in source.Security.SiteGroups
                        select new Model.SiteGroup(g.Members != null ? from m in g.Members select new Model.User { Name = m.Name } : null)
                        {
                            AllowMembersEditMembership = g.AllowMembersEditMembershipSpecified && g.AllowMembersEditMembership,
                            AllowRequestToJoinLeave = g.AllowRequestToJoinLeaveSpecified && g.AllowRequestToJoinLeave,
                            AutoAcceptRequestToJoinLeave = g.AutoAcceptRequestToJoinLeaveSpecified && g.AutoAcceptRequestToJoinLeave,
                            Description = g.Description,
                            OnlyAllowMembersViewMembership = g.OnlyAllowMembersViewMembershipSpecified && g.OnlyAllowMembersViewMembership,
                            Owner = g.Owner,
                            RequestToJoinLeaveEmailSetting = g.RequestToJoinLeaveEmailSetting,
                            Title = g.Title,
                        });
                }
                if (source.Security.Permissions != null)
                {
                    if (source.Security.Permissions.RoleAssignments != null && source.Security.Permissions.RoleAssignments.Length > 0)
                    {
                        result.Security.SiteSecurityPermissions.RoleAssignments.AddRange
                            (from ra in source.Security.Permissions.RoleAssignments
                             select new Model.RoleAssignment
                             {
                                 Principal = ra.Principal,
                                 RoleDefinition = ra.RoleDefinition,
                             });
                    }
                    if (source.Security.Permissions.RoleDefinitions != null && source.Security.Permissions.RoleDefinitions.Length > 0)
                    {
                        result.Security.SiteSecurityPermissions.RoleDefinitions.AddRange
                            (from rd in source.Security.Permissions.RoleDefinitions
                             select new Model.RoleDefinition(
                                 from p in rd.Permissions
                                 select (Microsoft.SharePoint.Client.PermissionKind)Enum.Parse(typeof(Microsoft.SharePoint.Client.PermissionKind), p.ToString()))
                             {
                                 Description = rd.Description,
                                 Name = rd.Name,
                             });
                    }
                }
            }
            return result;
        }

        public IProvisioningTemplate ParseTemplate(XMLPnPSchemaVersion schema, IProvisioningTemplate outgoingTemplate, ProvisioningTemplate incomingTemplate)
        {
            switch (schema)
            {
                case XMLPnPSchemaVersion.V201605:
                    {
                        var result = outgoingTemplate as V201605.ProvisioningTemplate;
                        outgoingTemplate = Parse201605Object(result, incomingTemplate);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }
            return outgoingTemplate;
        }

        private static IProvisioningTemplate Parse201605Object(V201605.ProvisioningTemplate result, ProvisioningTemplate template)
        {
            if (template.Security != null)
            {
                result.Security = new V201605.Security();

                result.Security.BreakRoleInheritance = template.Security.BreakRoleInheritance;
                result.Security.CopyRoleAssignments = template.Security.CopyRoleAssignments;
                result.Security.ClearSubscopes = template.Security.ClearSubscopes;

                if (template.Security.AdditionalAdministrators != null && template.Security.AdditionalAdministrators.Count > 0)
                {
                    result.Security.AdditionalAdministrators =
                        (from user in template.Security.AdditionalAdministrators
                         select new V201605.User
                         {
                             Name = user.Name,
                         }).ToArray();
                }
                else
                {
                    result.Security.AdditionalAdministrators = null;
                }

                if (template.Security.AdditionalOwners != null && template.Security.AdditionalOwners.Count > 0)
                {
                    result.Security.AdditionalOwners =
                        (from user in template.Security.AdditionalOwners
                         select new V201605.User
                         {
                             Name = user.Name,
                         }).ToArray();
                }
                else
                {
                    result.Security.AdditionalOwners = null;
                }

                if (template.Security.AdditionalMembers != null && template.Security.AdditionalMembers.Count > 0)
                {
                    result.Security.AdditionalMembers =
                        (from user in template.Security.AdditionalMembers
                         select new V201605.User
                         {
                             Name = user.Name,
                         }).ToArray();
                }
                else
                {
                    result.Security.AdditionalMembers = null;
                }

                if (template.Security.AdditionalVisitors != null && template.Security.AdditionalVisitors.Count > 0)
                {
                    result.Security.AdditionalVisitors =
                        (from user in template.Security.AdditionalVisitors
                         select new V201605.User
                         {
                             Name = user.Name,
                         }).ToArray();
                }
                else
                {
                    result.Security.AdditionalVisitors = null;
                }

                if (template.Security.SiteGroups != null && template.Security.SiteGroups.Count > 0)
                {
                    result.Security.SiteGroups =
                        (from g in template.Security.SiteGroups
                         select new V201605.SiteGroup
                         {
                             AllowMembersEditMembership = g.AllowMembersEditMembership,
                             AllowMembersEditMembershipSpecified = true,
                             AllowRequestToJoinLeave = g.AllowRequestToJoinLeave,
                             AllowRequestToJoinLeaveSpecified = true,
                             AutoAcceptRequestToJoinLeave = g.AutoAcceptRequestToJoinLeave,
                             AutoAcceptRequestToJoinLeaveSpecified = true,
                             Description = g.Description,
                             Members = g.Members.Any() ? (from m in g.Members
                                                          select new V201605.User
                                                          {
                                                              Name = m.Name,
                                                          }).ToArray() : null,
                             OnlyAllowMembersViewMembership = g.OnlyAllowMembersViewMembership,
                             OnlyAllowMembersViewMembershipSpecified = true,
                             Owner = g.Owner,
                             RequestToJoinLeaveEmailSetting = g.RequestToJoinLeaveEmailSetting,
                             Title = g.Title,
                         }).ToArray();
                }
                else
                {
                    result.Security.SiteGroups = null;
                }

                result.Security.Permissions = new SecurityPermissions();
                if (template.Security.SiteSecurityPermissions != null)
                {
                    if (template.Security.SiteSecurityPermissions.RoleAssignments != null && template.Security.SiteSecurityPermissions.RoleAssignments.Count > 0)
                    {
                        result.Security.Permissions.RoleAssignments =
                            (from ra in template.Security.SiteSecurityPermissions.RoleAssignments
                             select new V201605.RoleAssignment
                             {
                                 Principal = ra.Principal,
                                 RoleDefinition = ra.RoleDefinition,
                             }).ToArray();
                    }
                    else
                    {
                        result.Security.Permissions.RoleAssignments = null;
                    }
                    if (template.Security.SiteSecurityPermissions.RoleDefinitions != null && template.Security.SiteSecurityPermissions.RoleDefinitions.Count > 0)
                    {
                        result.Security.Permissions.RoleDefinitions =
                            (from rd in template.Security.SiteSecurityPermissions.RoleDefinitions
                             select new V201605.RoleDefinition
                             {
                                 Description = rd.Description,
                                 Name = rd.Name,
                                 Permissions =
                                    (from p in rd.Permissions
                                     select (RoleDefinitionPermission)Enum.Parse(typeof(RoleDefinitionPermission), p.ToString())).ToArray(),
                             }).ToArray();
                    }
                    else
                    {
                        result.Security.Permissions.RoleDefinitions = null;
                    }
                }
                if (
                    result.Security.AdditionalAdministrators == null &&
                    result.Security.AdditionalMembers == null &&
                    result.Security.AdditionalOwners == null &&
                    result.Security.AdditionalVisitors == null &&
                    result.Security.Permissions.RoleAssignments == null &&
                    result.Security.Permissions.RoleDefinitions == null &&
                    result.Security.SiteGroups == null)
                {
                    result.Security = null;
                }
            }
            return result;
        }

    }
}
