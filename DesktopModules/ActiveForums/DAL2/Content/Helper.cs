using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{
    public static class Helper
    {
        public static int ComponentId = 1;
        public static bool GlobalQuestion = false;

        public static string RecommandImagename(bool isRecommanded)
        {
            if (!isRecommanded)
                return "forum-recommend.png";
            return "forum-recommended.png";
        }
        public static string UnRecommandImagename(bool isRecommanded)
        {
            if (!isRecommanded)
                return "forum-unrecommend.png";
            return "forum-unrecommended.png";
        }

        public static DotNetNuke.Entities.Portals.PortalSettings CurrentPortalSettings
        {
            get
            {
                return DotNetNuke.Entities.Portals.PortalSettings.Current;

            }
        }

        public static int GetPortalid
        {
            get
            {
                var oPortalsettings = CurrentPortalSettings;
                if (oPortalsettings != null)
                    return oPortalsettings.PortalId;
                return 0;

            }
        }

        public static int GetCurrentTabid
        {
            get
            {
                var oCurenttabid = DotNetNuke.Entities.Tabs.TabController.CurrentPage;
                if (oCurenttabid != null)
                    return oCurenttabid.TabID;
                return 0;

            }
        }

        public static int GetCurrentModuleid
        {
            get
            {
                var Moduledefing = new DotNetNuke.Entities.Modules.ModuleController().GetModuleByDefinition(GetPortalid == -1 ? 0 : GetPortalid, "Active Forums");
                if (Moduledefing != null)
                    return Moduledefing.ModuleID;
                return 419;

            }
        }
        public static string TagUrl(string TagName)
        {
            if (string.IsNullOrEmpty(TagName)) return "";

            string strurl = DotNetNuke.Common.Globals.NavigateURL(GetCurrentTabid, "Search", "mid=" + GetCurrentModuleid.ToString(), "q=" + TagName);
            return string.Format("<a href='{0}' class='tags'>{1}</a>", strurl, TagName);
        }
    }
}
