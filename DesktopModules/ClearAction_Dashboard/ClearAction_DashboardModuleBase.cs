/*
' Copyright (c) 2017  ClearAction
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

namespace ClearAction.Modules.Dashboard.Components
{
    public class ClearAction_DashboardModuleBase : PortalModuleBase
    {
        public string ReadTemplate(string FileName)
        {
            string strFilePath = Server.MapPath("DesktopModules") + string.Format("/ClearAction_Dashboard/Templates/{0}.html", FileName);

            try
            {
                if (System.IO.File.Exists(strFilePath))
                    return System.IO.File.ReadAllText(strFilePath);


            }
            catch (Exception exc)
            {


            }

            return "";
        }
        public int ItemId
        {
            get
            {
                var qs = Request.QueryString["tid"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return -1;
            }

        }


        public string GetSettingByKey(string key, string defaultvalue)
        {
            if (!Settings.Contains(key))
                return defaultvalue;
            return Convert.ToString(Settings[key]);

        }



       



    }
}