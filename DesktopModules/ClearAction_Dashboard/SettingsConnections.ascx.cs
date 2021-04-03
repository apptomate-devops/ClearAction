
using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
namespace ClearAction.Modules.Dashboard.Components
{
    public partial class SettingsConnections : ClearAction_DashboardModuleSettingsBase
    {

        public string ReadTemplate(string FileName)
        {
            string strFilePath = DotNetNuke.Common.Globals.ApplicationMapPath + string.Format("/DesktopModules/ClearAction_Connections/Templates/{0}.html", FileName);

            try
            {
                if (System.IO.File.Exists(strFilePath))
                    return Server.HtmlEncode(System.IO.File.ReadAllText(strFilePath));


            }
            catch (Exception exc)
            {


            }

            return "";
        }
        public void UpdateTemplate(string data)
        {
            string strFilePath = DotNetNuke.Common.Globals.ApplicationMapPath + string.Format("/DesktopModules/ClearAction_Connections/Templates/{0}.html", "EmailTemplate");
            try
            {
                if (System.IO.File.Exists(strFilePath))
                {
                    System.IO.File.WriteAllText(strFilePath, Server.HtmlDecode(data));
                }


            }
            catch (Exception exc)
            {


            }


        }
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
                    //Check for existing settings and use those on this page
                    //Settings["SettingName"]

                    //    uncomment to load saved settings in the text boxes
                    if (Settings.Contains("ConfigureFor"))
                        rdobtn.SelectedValue = Convert.ToString(Settings["ConfigureFor"]) == "0" ? "0" : Convert.ToString(Settings["ConfigureFor"]);

                    if (Settings.Contains("PageSize"))
                        txtNumberofRecords.Text = Convert.ToString(Settings["PageSize"]);


                    if (Settings.Contains("JSReference"))
                        chkJSRefernce.Checked = Convert.ToBoolean(Settings["JSReference"]);
                 //   txteditor.Text = ReadTemplate("EmailTemplate");

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
                modules.UpdateModuleSetting(ModuleId, "ConfigureFor", rdobtn.SelectedValue == null ? "-1" : rdobtn.SelectedValue.ToString());
                modules.UpdateModuleSetting(ModuleId, "PageSize", string.IsNullOrEmpty(txtNumberofRecords.Text) == true ? "5" : txtNumberofRecords.Text);
                modules.UpdateModuleSetting(ModuleId, "JSReference", chkJSRefernce.Checked.ToString());
             //   UpdateTemplate(txteditor.Text);
                //tab module settings
                //  modules.UpdateTabModuleSetting(TabModuleId, "SettingFor", rdobtn.SelectedValue == null ? "0" : rdobtn.SelectedValue.ToString());
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