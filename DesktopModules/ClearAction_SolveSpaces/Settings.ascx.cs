using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Tabs;
using System.Collections.Generic;
namespace ClearAction.Modules.SolveSpaces
{
    public partial class Settings : ClearAction_SolveSpacesModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    List<TabInfo> lstTabs = TabController.Instance.GetTabsByPortal(this.PortalId).AsList();
                    foreach (TabInfo iTab in lstTabs)
                    {
                        ddlTabs.Items.Add(new System.Web.UI.WebControls.ListItem(iTab.TabName, iTab.TabID.ToString()));
                    }
                    
                    //Check for existing settings and use those on this page
                    //Settings["SettingName"]

                    /* uncomment to load saved settings in the text boxes
                    if(Settings.Contains("Setting1"))
                        txtSetting1.Text = Settings["Setting1"].ToString();
			
                    if (Settings.Contains("Setting2"))
                        txtSetting2.Text = Settings["Setting2"].ToString();

                    */
                    try
                    {
                        if (Settings["DisplayMode"] != null)
                        {
                            ddlDisplayMode.ClearSelection();
                            ddlDisplayMode.Items.FindByValue(Settings["DisplayMode"].ToString()).Selected = true;
                        }
                        if (Settings["MyVaultTab"] != null)
                        {
                            ddlTabs.ClearSelection();
                            ddlTabs.Items.FindByValue(Settings["MyVaultTab"].ToString()).Selected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Do Nothing on error
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                //the following are two sample Module Settings, using the text boxes that are commented out in the ASCX file.
                //module settings
                modules.UpdateModuleSetting(ModuleId, "MyVaultTab", ddlTabs.SelectedValue);
                modules.UpdateModuleSetting(ModuleId, "DisplayMode", ddlDisplayMode.SelectedValue);
                //modules.UpdateModuleSetting(ModuleId, "Setting2", txtSetting2.Text);

                //tab module settings
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting1",  txtSetting1.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting2",  txtSetting2.Text);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}