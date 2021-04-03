using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Security.Roles;
using DotNetNuke.Entities.Users;
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup.Components
{
    public partial class DbController
    {
        public string ExeculdeRoles
        {
            get
            {
                string strConfigVaulue = System.Configuration.ConfigurationManager.AppSettings["Exculde_Role"];
                if (string.IsNullOrEmpty(strConfigVaulue))
                    strConfigVaulue = "Administrators, Registered Users, Subscribers, Translator (en-US),Unverified Users";
                return strConfigVaulue;
            }
        }

        public bool UserInRole(int Portalid, int iRoleid, UserInfo oCurrentUser)
        {
            var oRoleinfo = new RoleController().GetRoleById(Portalid, iRoleid);
            if (oRoleinfo != null)
            {
                if (oCurrentUser.IsInRole(oRoleinfo.RoleName))
                    return true;
            }

            return false;
        }

        public List<RoleInfo> GetPortalRoles(int Portalid)
        {
            var oList = new RoleController().GetRoles(Portalid);
            string RoleToExeculde = ExeculdeRoles;
            List<RoleInfo> lstRoleToShow = new List<RoleInfo>();

            foreach (RoleInfo oroleinfo in oList)
            {
                if (!RoleToExeculde.Contains(oroleinfo.RoleName))
                {
                    lstRoleToShow.Add(oroleinfo);
                }
            }

            return lstRoleToShow;
        }

        public string GetcurrentUserRoleName(int iPortalid, int iUserID)
        {
            string[] strRole = UserController.GetUserById(iPortalid, iUserID).Roles;
            string strRoleToExecludes = ExeculdeRoles;
            string struserrole = string.Empty;
            foreach (string strrolename in strRole)
            {
                if (!strRoleToExecludes.Contains(strrolename))
                {
                    struserrole = strrolename;
                }


            }
            return struserrole;
        }

        public void UpdateUserRoleInformation(DotNetNuke.Entities.Portals.PortalSettings p, int Portalid, int iCurrentRoleId, UserInfo oCurrentUser)
        {
            var oRoleinfo = new RoleController().GetRoleById(Portalid, iCurrentRoleId);
            if (oRoleinfo != null)
            {
                if (oCurrentUser.IsInRole(oRoleinfo.RoleName))
                    return;


                //Remove user from other roles they already assigned.
                var olist = GetPortalRoles(Portalid);
                foreach (RoleInfo r in olist)
                {
                    if (oCurrentUser.IsInRole(oRoleinfo.RoleName))
                        RoleController.DeleteUserRole(oCurrentUser, r, p, true);

                }
                //Add new role if not yet added

                RoleController.AddUserRole(oCurrentUser, oRoleinfo, p, RoleStatus.Approved, System.DateTime.Now, System.DateTime.Now.AddYears(10), true, false);


                UserController.UpdateUser(Portalid, oCurrentUser);








            }


        }
        public void UpdateUserRoleInformation(DotNetNuke.Entities.Portals.PortalSettings p, int Portalid, int iCurrentRoleId, UserInfo oCurrentUser, bool Deleteold)
        {

            if (oCurrentUser.IsSuperUser)
                return;
            var oRole = new RoleController().GetRoleById(Portalid, iCurrentRoleId);

            var oRoleinfo = GetcurrentUserRoleName(Portalid, oCurrentUser.UserID);
            if (oRole.RoleName == oRoleinfo)
                return;
            else
            {
                //Remove user from other roles they already assigned.
                var olist = GetPortalRoles(Portalid);
                foreach (RoleInfo r in olist)
                {
                    if (oCurrentUser.IsInRole(r.RoleName))
                        RoleController.DeleteUserRole(oCurrentUser, r, p, false);

                }
                //Add new role if not yet added

                if (oRole != null)
                    RoleController.AddUserRole(oCurrentUser, oRole, p, RoleStatus.Approved, System.DateTime.Now, System.DateTime.Now.AddYears(10), false, false);


                UserController.UpdateUser(Portalid, oCurrentUser);

            }


        }

    }
}