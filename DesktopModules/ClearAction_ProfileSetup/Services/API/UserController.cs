using System.Linq;
using System.Net.Http;
using System.Collections.Generic;
using ClearAction.Modules.ProfileClearAction_ProfileSetup.Services.ViewModels;
using DotNetNuke.Web.Api;
using DotNetNuke.Security;
using DotNetNuke.Entities.Users;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Services
{
    [SupportedModules("ClearAction_ProfileSetup")]
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
    public class UserController : DnnApiController
    {
        public UserController() { }

        public HttpResponseMessage GetList()
        {

            var userlist = DotNetNuke.Entities.Users.UserController.GetUsers(this.PortalSettings.PortalId);
            var users = userlist.Cast<UserInfo>().ToList()
                   .Select(user => new UserViewModel(user))
                   .ToList();

            return Request.CreateResponse(users);
        }

        public HttpResponseMessage Get(int iUserid)
        {

            var userlist = DotNetNuke.Entities.Users.UserController.GetUserById(this.PortalSettings.PortalId, iUserid);
            return Request.CreateResponse(new UserViewModel(userlist));
        }
    }
}
