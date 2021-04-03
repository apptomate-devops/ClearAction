

using ClearAction.Modules.Dashboard.Components;
using System.Configuration;

namespace ClearAction.Modules.Dashboard
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_DashboardModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class UcControl : ClearAction_DashboardModuleBase//, IActionable
    {

        public string GetAPIKey
        {
            get
            {
                return ConfigurationManager.AppSettings["FileStackApiKey"];
            }
        }


    }
}