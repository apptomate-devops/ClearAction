/*
' Copyright (c) 2018  ClearAction
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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Portals;

namespace ClearAction.Modules.WebCast
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from ClearAction_WebCastSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : SettingsBase
    {
        #region Base Method Implementations



        public void BindDropDown()
        {
            var oComponentlist = new Components.DBController().GetComponentMaster();
            ddCommunityCalls.DataSource = oComponentlist;
            ddCommunityCalls.DataValueField = "ComponentID";
            ddCommunityCalls.DataTextField = "ComponentName";


            ddWebCastConversion.DataValueField = "ComponentID";
            ddWebCastConversion.DataTextField = "ComponentName";
            ddWebCastConversion.DataSource = oComponentlist;

            ddCommunityCalls.DataBind();
            ddWebCastConversion.DataBind();

        }
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
                    BindDropDown();
                    ddCommunityCalls.Items.FindByValue(PortalController.GetPortalSetting(Components.Util.CommunityCalls, PortalId, ddCommunityCalls.Items[0].Value)).Selected = true;
                    ddWebCastConversion.Items.FindByValue(PortalController.GetPortalSetting(Components.Util.WebCastConversion, PortalId, ddWebCastConversion.Items[0].Value)).Selected = true;
                    if (Settings.Contains(Components.Util.ModuleInstance))
                        rdoListingApplicableFor.Items.FindByValue(Convert.ToString(Settings[Components.Util.ModuleInstance])).Selected = true;

                    if (Settings.Contains("SENDEMAILNOTIFICATION"))
                        chkSendCongrtsEmail.Checked = Settings.Contains("SENDEMAILNOTIFICATION").ToString().ToLower() == "true" ? true : false;// Convert.ToBoolean(Settings.Contains("SENDEMAILNOTIFICATION"));
                    if (Settings.Contains(Components.Util.MAX_SELFASSIGNED_EVENT))
                        txtMaxCount.Text = Settings[Components.Util.MAX_SELFASSIGNED_EVENT].ToString();// PortalController.GetPortalSetting(Components.Util.MAX_SELFASSIGNED_EVENT, PortalId, "2");

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
                modules.UpdateModuleSetting(ModuleId, Components.Util.ModuleInstance, rdoListingApplicableFor.SelectedValue);

                modules.UpdateModuleSetting(ModuleId, "SENDEMAILNOTIFICATION", chkSendCongrtsEmail.Checked.ToString());
                modules.UpdateModuleSetting(ModuleId, Components.Util.MAX_SELFASSIGNED_EVENT, txtMaxCount.Text);
                //modules.UpdateModuleSetting(ModuleId, "Setting2", txtSetting2.Text);

                //tab module settings
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting1",  txtSetting1.Text);
                //modules.UpdateTabModuleSetting(TabModuleId, "Setting2",  txtSetting2.Text);

                PortalController.UpdatePortalSetting(this.PortalId, Components.Util.CommunityCalls, ddCommunityCalls.SelectedItem == null ? ddCommunityCalls.Items[0].Value : ddCommunityCalls.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Components.Util.WebCastConversion, ddWebCastConversion.SelectedItem == null ? ddWebCastConversion.Items[0].Value : ddWebCastConversion.SelectedValue);

                //  PortalController.UpdatePortalSetting(this.PortalId, Components.Util.MAX_SELFASSIGNED_EVENT, txtMaxCount.Text);

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}