using System;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;

namespace ClearAction.Modules.SolveSpaces
{
    public partial class View : ClearAction_SolveSpacesModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if ((Settings["DisplayMode"] != null) && ((Settings["DisplayMode"].ToString() == "MyExchange")))
                {
                    pnlSolveSpaceDashboard.Visible = false;
                    ltTopSolveSpaces.Visible = true;
                }
                else
                {
                    pnlSolveSpaceDashboard.Visible = true;
                    ltTopSolveSpaces.Visible = false;
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
                actions[0].Title = "Manage Solve-Spaces";
                return actions;
            }
        }
    }
}