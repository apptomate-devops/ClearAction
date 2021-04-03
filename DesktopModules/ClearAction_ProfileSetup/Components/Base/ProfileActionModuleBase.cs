using System;
using DotNetNuke.Entities.Modules;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Portals;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup
{
    public class ProfileActionModuleBase : PortalModuleBase
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
        public int ModuleSettingKey(string Key)
        {
            int iout = -1;
            string strValue = string.Empty;
            strValue = PortalController.GetPortalSetting(Key, PortalId, strValue);

            if (string.IsNullOrEmpty(strValue)) return iout;
            try
            {

                int.TryParse(strValue, out iout);
            }
            catch (Exception ex)
            {


            }
            return iout;
        }

        public string ModuleSettingKeyString(string Key)
        {

            string strValue = string.Empty;
            strValue = PortalController.GetPortalSetting(Key, PortalId, strValue);
            if (string.IsNullOrEmpty(strValue)) return "";

            return Convert.ToString(strValue);

        }

        public Control ReadControlGeneric(string ParentControlID)
        {
            HtmlGenericControl divControl = (HtmlGenericControl)this.FindControl(ParentControlID);
            return divControl;

        }

        public string ReadTemplate(string FileName)
        {
            string strFilePath = DotNetNuke.Common.Globals.ApplicationMapPath + string.Format("/DesktopModules/ClearAction_ProfileSetup/Templates/{0}.html", FileName);
            if (System.IO.File.Exists(strFilePath))
                return System.IO.File.ReadAllText(strFilePath);
            return "";
        }
    }
}