using System.Collections.Generic;

using DotNetNuke.Entities.Profile;
using DotNetNuke.Entities.Users.Social;
using DotNetNuke.Security.Roles;
using DotNetNuke.Web.Mvp;
namespace ClearAction.Modules.Connections.ViewModels
{
    public class MemberDirectorySettingsModel : SettingsModel
    {
        public IList<ProfilePropertyDefinition> ProfileProperties;
        public IList<Relationship> Relationships;
        public IList<RoleInfo> Groups;
    }
}