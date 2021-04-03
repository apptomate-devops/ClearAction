/*
' Copyright (c) 2017  Christoc.com
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
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;

namespace ClearAction.Modules.UserAnalytics
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_UserAnalyticsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : UserAnalyticsModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Settings["Analytics_Mode"] != null)
                {
                    string _Mode = Settings["Analytics_Mode"].ToString();
                    if (_Mode != "-1")
                    {
                        pnlGrayBar.Visible = false;
                        if (_Mode == "1")//Solve Space
                        {
                            pnlSSContainer.Visible = true;
                            pnlForumContainer.Visible = false;
                            pnlInsightContainer.Visible = false;
                            lblSSGrap.Text = "Solve-Spaces<br/>Completed";
                            pnlSSContainer.CssClass = "CA_SingleGraph";
                            pnlWCContainer.Visible = false;
                            pnlCCContainer.Visible = false;
                        }
                        if (_Mode == "2") // Forum
                        {
                            pnlSSContainer.Visible = false;
                            pnlForumContainer.Visible = true;
                            pnlInsightContainer.Visible = false;
                            pnlForumContainer.CssClass = "CA_SingleGraph";
                            pnlWCContainer.Visible = false;
                            pnlCCContainer.Visible = false;
                        }
                        if (_Mode == "3")// Blog
                        {
                            pnlSSContainer.Visible = false;
                            pnlForumContainer.Visible = false;
                            pnlInsightContainer.Visible = true;
                            pnlInsightContainer.CssClass = "CA_SingleGraph";
                            pnlWCContainer.Visible = false;
                            pnlCCContainer.Visible = false;
                        }
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
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