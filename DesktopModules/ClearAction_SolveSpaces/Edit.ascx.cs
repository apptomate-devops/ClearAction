using System;
//using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Exceptions;
namespace ClearAction.Modules.SolveSpaces
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditClearAction_SolveSpaces class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from ClearAction_SolveSpacesModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : ClearAction_SolveSpacesModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}