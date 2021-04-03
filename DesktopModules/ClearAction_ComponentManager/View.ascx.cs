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
using System.Web.Configuration;

namespace ClearAction.Modules.ComponentManager
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_ComponentManagerModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ComponentManagerModuleBase, IActionable
    {

        public int PageSize
        {
            get
            {
                try
                {
                    string strPageSize = WebConfigurationManager.AppSettings["SolveSpace_PageSize"];
                    if (strPageSize.Trim() == "") return 5;
                    else
                        return int.Parse(strPageSize);
                }
                catch (Exception ex)
                {
                    return 5;
                }
            }
        }
        /// <summary>
        /// Forum Page Size
        /// </summary>
        public int FPageSize
        {
            get
            {
                try
                {
                    string strPageSize = WebConfigurationManager.AppSettings["Forum_PageSize"];
                    if (strPageSize.Trim() == "") return 5;
                    else
                        return int.Parse(strPageSize);
                }
                catch (Exception ex)
                {
                    return 5;
                }
            }
        }
        /// <summary>
        /// Blog Page Size
        /// </summary>
        public int BPageSize
        {
            get
            {
                try
                {
                    string strPageSize = WebConfigurationManager.AppSettings["Blog_PageSize"];
                    if (strPageSize.Trim() == "") return 5;
                    else
                        return int.Parse(strPageSize);
                }
                catch (Exception ex)
                {
                    return 5;
                }
            }
        }


        /// <summary>
        /// Term Page Size
        /// </summary>
        public int TPageSize
        {
            get
            {
                try
                {
                    string strPageSize = WebConfigurationManager.AppSettings["Term_PageSize"];
                    if (strPageSize.Trim() == "") return 10;
                    else
                        return int.Parse(strPageSize);
                }
                catch (Exception ex)
                {
                    return 10;
                }
            }
        }


        /// <summary>
        /// Global Category Page Size
        /// </summary>
        public int CatPageSize
        {
            get
            {
                try
                {
                    string strPageSize = WebConfigurationManager.AppSettings["Category_PageSize"];
                    if (strPageSize.Trim() == "") return 10;
                    else
                        return int.Parse(strPageSize);
                }
                catch (Exception ex)
                {
                    return 10;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                

               // lnkGlobCat2.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "CategoryAdmin");
                
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