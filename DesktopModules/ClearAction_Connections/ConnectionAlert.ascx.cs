/*
' Copyright (c) 2017  Ajit jha
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;

using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;

using System.Reflection;
using System.Web.Routing;
using System.Web.UI;

using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.UI.Modules;
using DotNetNuke.Web.Client.ClientResourceManagement;
using System.IO;
using System.Web;
using System.Configuration;

namespace ClearAction.Modules.Connections
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_ConnectionsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class ConnectionAlert : ConnectionsModuleBase, IActionable
    {
        private static string templatePath = "~/DesktopModules/ClearAction_Connections/Templates/";
        protected override void OnInit(EventArgs e)
        {
            ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
            JavaScript.RequestRegistration(CommonJs.DnnPlugins);
            JavaScript.RequestRegistration(CommonJs.jQueryFileUpload);
            JavaScript.RequestRegistration(CommonJs.Knockout);

            ClientResourceManager.RegisterScript(Page, "~/DesktopModules/ClearAction_Connections/Scripts/Module.js");
            //   AddIe7StyleSheet();

            //searchBar.Visible = DisplaySearch != "None";
            //advancedSearchBar.Visible = DisplaySearch == "Both";
            //popUpPanel.Visible = EnablePopUp;
            //loadMore.Visible = !DisablePaging;

            base.OnInit(e);
            
        }


        public static string DefaultItemTemplate
        {
            get
            {
                string template;
                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(templatePath + "tmpGlobFriend.html")))
                {
                    template = sr.ReadToEnd();
                }
                return template;
            }
        }

        public string ConnectionTab
        {
            get
            {
                if (PortalSettings.ActiveTab.TabName.Contains("Connections"))
                    return "none";
                return "block";
            }
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }
    }
}