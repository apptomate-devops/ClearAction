using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;

using DotNetNuke.Security;
using DotNetNuke.Services.Localization;

using DotNetNuke.Services.FileSystem;
using DotNetNuke.Entities.Profile;
using DotNetNuke.Entities.Users;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using ClearAction.Modules.Dashboard.Components;
using DotNetNuke.UI.Utilities;
using DotNetNuke.Entities.Portals;

namespace ClearAction.Modules.Dashboard
{
    public partial class ViewConnections : Dashboard.Components.ClearAction_DashboardModuleBase
    {
        private int PageSize
        {
            get
            {
                int TopN = 5;
                try
                {


                    TopN = int.Parse(GetSettingByKey("PageSize", TopN.ToString()));
                }
                catch (Exception ex)
                {
                }
                return TopN;
            }
        }
        private int ViewType
        {
            get
            {
                try
                {

                    return Convert.ToInt32(GetSettingByKey("ConfigureFor", "0"));

                }
                catch (Exception ex)
                {


                }
                return 0;
            }
        }

        private void RenderUI(int iViewType)
        {
            var oController = new Dashboard.Components.DashboardController();
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            switch (iViewType)
            {
                case 0:
                    //Connections
                    var oListofUser = oController.GetConnectionList(this.PortalId, UserId, PageSize);


                    string ItemTemplate = ReadTemplate("ConnectionsItem");
                    bool IsEmpty = false;
                    if (oListofUser == null || oListofUser.Count == 0)
                    {
                        IsEmpty = true;
                    }



                    if (IsEmpty)
                    {
                        strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No connections found.</label>");

                    }
                    else
                        foreach (UserInfo oUserinfo in oListofUser)
                        {
                            strBuilder.Append(Utilies.ReplaceUserInfoToken(ItemTemplate, oUserinfo));
                        }

                    pHolder.Text = ReadTemplate("ConnectionsHeader").Replace("[Items]", strBuilder.ToString());

                    break;
                case 1:

                    ///Similar to roles
                    ///

                    string strRole = oController.GetcurrentUserRoleName(this.PortalId, this.UserId);
                    IsEmpty = false;
                    if (string.IsNullOrEmpty(strRole))
                    {
                        IsEmpty = true;

                        //To Do: action if no roles has been added to user. Display  appropriate information
                    }
                    var oListOfUserInSimilarRoles = oController.GetUsersByCurrentRoleName(this.PortalId, strRole).Where(x => x.UserID != UserId).Skip(0).Take(PageSize).ToList();

                    ItemTemplate = ReadTemplate("SimilarRolesItem");

                    if (oListOfUserInSimilarRoles == null || oListOfUserInSimilarRoles.Count == 0)
                    {
                        IsEmpty = true;
                    }



                    if (IsEmpty)
                    {
                        strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No  User found</label>");

                    }
                    else
                        foreach (UserInfo oUserinfo in oListOfUserInSimilarRoles)
                        {
                            strBuilder.Append(Utilies.ReplaceUserInfoToken(ItemTemplate, oUserinfo));
                        }

                    pHolder.Text = ReadTemplate("SimilarRolesHeader").Replace("[Items]", strBuilder.ToString());

                    break;
                case 2:

                    //In my company

                    if (BoolsIsOtherCompany)
                    {
                        strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No User found in your company.</label>");
                        return;
                    }
                    //Connections
                    oListofUser = oController.GetSimilarToCompany(this.PortalId, UserId, PageSize);


                    ItemTemplate = ReadTemplate("CompanyItem");
                    IsEmpty = false;
                    if (oListofUser == null || oListofUser.Count == 0)
                    {
                        IsEmpty = true;
                    }



                    if (IsEmpty)
                    {
                        strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No User found in your company.</label>");

                    }
                    else
                        foreach (UserInfo oUserinfo in oListofUser)
                        {
                            strBuilder.Append(Utilies.ReplaceUserInfoToken(ItemTemplate, oUserinfo));
                        }

                    pHolder.Text = ReadTemplate("CompanyHeader").Replace("[Items]", strBuilder.ToString());



                    break;
                case 3: break;
            }
        }
        private string CompanyName
        {
            get
            {
                var rName = new DashboardController().GetResponseText(PortalId, UserId, "CompanyName", true);
                return string.IsNullOrEmpty(rName) == true ? "" : rName;
            }
        }

        public bool BoolsIsOtherCompany
        {
            get
            {
                var rName = CompanyName;
                if (rName.ToLower().Contains("other"))
                    return true;
                return false;
            }
        }

        #region "Roles Similar to mine"

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (GetSettingByKey("JSReference", "false").ToLower() == "true")
            {
                SetControl("~/DesktopModules/ClearAction_Dashboard/UcControl.ascx");

            }
        }

        private void SetControl(string ControlPath)
        {
            PortalModuleBase b = (PortalModuleBase)LoadControl(ControlPath);
            b.ModuleConfiguration = this.ModuleConfiguration;
            b.ID = System.IO.Path.GetFileNameWithoutExtension(ControlPath);
            pControler.Controls.Add(b);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            ClientAPI.RegisterClientReference(this.Page, ClientAPI.ClientNamespaceReferences.dnn);
            if (!IsPostBack)
                RenderUI(ViewType);
        }
    }
}