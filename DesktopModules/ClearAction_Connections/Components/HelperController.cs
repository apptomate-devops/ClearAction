using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ClearAction.Modules.Connections
{
    public static class HelperController
    {

        public static int MaxFav
        {

            get
            {
                int iMax = 5;
                try
                {

                    string strConfigVaulue = System.Configuration.ConfigurationManager.AppSettings["MaxFav"];
                    if (!string.IsNullOrEmpty(strConfigVaulue))
                        int.TryParse(strConfigVaulue, out iMax);

                }
                catch (Exception ex)
                {


                }
                return iMax;
            }
        }

        //Execulde roles
        public static string ExeculdeRoles
        {
            get
            {
                string strConfigVaulue = System.Configuration.ConfigurationManager.AppSettings["Exculde_Role"];
                if (string.IsNullOrEmpty(strConfigVaulue))
                    strConfigVaulue = "Administrators, Registered Users, Subscribers, Translator (en-US),Unverified Users";
                return strConfigVaulue;
            }
        }
        public static string GetcurrentUserRoleName(int iPortalid, int iUserID)
        {
            string[] strRole = UserController.GetUserById(iPortalid, iUserID).Roles;
            string strRoleToExecludes = ExeculdeRoles;
            string struserrole = string.Empty;
            foreach (string strrolename in strRole)
            {
                if (!strRoleToExecludes.Contains(strrolename))
                {
                    struserrole = strrolename;
                }


            }
            return struserrole;
        }


        public static string GetProfileResponse(int QuestionID, int iUserID)
        {
            string strResponse = string.Empty;

            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<string>();

                strResponse = rep.Find("SELECT ResponseText FROM ClearChoice_ProfileResponse Where UserID=@0 AND QuestionId=@1", iUserID, QuestionID).SingleOrDefault();

            }
            return strResponse;
        }

        public static string GetProfileResponse(int QuestionID, int iUserID, bool IsCompany)
        {
            var oResponseText = GetProfileResponse(QuestionID, iUserID);

            try
            {
                if (!string.IsNullOrEmpty(oResponseText))
                    return GetCompanyName(Convert.ToInt32(oResponseText));
            }
            catch (Exception ex)
            {


            }
            return "";
        }

        public static string GetCompanyName(int CompanyId)
        {
            string strResponse = string.Empty;
            IQueryable<string> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<string>();

                t = rep.Find("SELECT CompanyName FROM ClearAction_GlobalCompany Where  CompanyId=@0", CompanyId).AsQueryable();

            }
            if (t != null)
                return t.SingleOrDefault();
            return strResponse;
        }

        public static string ToRelativeDateString(DateTime value, bool approximate)
        {
            StringBuilder sb = new StringBuilder();

            string suffix = (value > DateTime.Now) ? " from now" : " ago";

            TimeSpan timeSpan = new TimeSpan(Math.Abs(DateTime.Now.Subtract(value).Ticks));

            if (timeSpan.Days > 0)
            {
                sb.AppendFormat("{0} {1}", timeSpan.Days,
                  (timeSpan.Days > 1) ? "days" : "day");
                if (approximate) return sb.ToString() + suffix;
            }
            if (timeSpan.Hours > 0)
            {
                sb.AppendFormat("{0}{1} {2}", (sb.Length > 0) ? ", " : string.Empty,
                  timeSpan.Hours, (timeSpan.Hours > 1) ? "hours" : "hour");
                if (approximate) return sb.ToString() + suffix;
            }
            if (timeSpan.Minutes > 0)
            {
                sb.AppendFormat("{0}{1} {2}", (sb.Length > 0) ? ", " : string.Empty,
                  timeSpan.Minutes, (timeSpan.Minutes > 1) ? "minutes" : "minute");
                if (approximate) return sb.ToString() + suffix;
            }
            if (timeSpan.Seconds > 0)
            {
                sb.AppendFormat("{0}{1} {2}", (sb.Length > 0) ? ", " : string.Empty,
                  timeSpan.Seconds, (timeSpan.Seconds > 1) ? "seconds" : "second");
                if (approximate) return sb.ToString() + suffix;
            }
            if (sb.Length == 0) return "right now";

            sb.Append(suffix);
            return sb.ToString();
        }


    }
}