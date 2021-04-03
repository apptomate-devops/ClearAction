using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace ClearAction.Modules.Dashboard.Components
{
    public static class Utilies
    {

        public static int CurrentPortalid
        {

            get
            {
                var o = DotNetNuke.Entities.Portals.PortalController.Instance.GetCurrentPortalSettings();
                return o == null ? 0 : o.PortalId;
            }

        }
        public static string GetCurrencySymbol(decimal currency)
        {
            return String.Format("{0:C}", Convert.ToString(currency));
        }

        public static string DisplayCulutureNumeric(decimal oType)
        {
            return oType != -1 ? string.Format(new CultureInfo("en-US"), "{0:c}", oType) : "";

        }

        public static string DisplayCulutureDateTime(DateTime dt)
        {
           
            return String.Format("{0:MM/dd/yyyy}",   dt);
        }

        public static string DisplayCulutureDateTime(DateTime dt,bool IsCST)
        {
            TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime newDT = TimeZoneInfo.ConvertTimeFromUtc(dt, targetZone);

            return String.Format("{0:MM/dd/yyyy}", newDT);
        }
        public static string DisplayCulutureNumeric(int oType)
        {
            return oType != -1 ? string.Format(new CultureInfo("en-US"), "{0:c}", oType) : "";

        }

        public static string DisplayCulutureTime(DateTime dt)
        {
            return dt.ToString("h:mm tt");
        }


        public static string RemoveFragmentsBetween(this string rawString, char enter, char exit)
        {
            if (rawString.Contains(enter) && rawString.Contains(exit))
            {
                int substringStartIndex = rawString.IndexOf(enter) + 1;
                int substringLength = rawString.LastIndexOf(exit) - substringStartIndex;

                if (substringLength > 0 && substringLength > substringStartIndex)
                {
                    string substring = rawString.Substring(substringStartIndex, substringLength).RemoveFragmentsBetween(enter, exit);
                    if (substring.Length != substringLength) // This would mean that letters have been removed
                    {
                        rawString = rawString.Remove(substringStartIndex, substringLength).Insert(substringStartIndex, substring).Trim();
                    }
                }

                Regex regex = new Regex(String.Format("\\{0}.*?\\{1}", enter, exit));
                return new Regex(" +").Replace(regex.Replace(rawString, string.Empty), " ").Trim(); // Removing duplicate and tailing/leading spaces
            }
            else
            {
                return rawString;
            }
        }

        public static string ReplaceUserInfoToken(string data, UserInfo oFeed)
        {
            if (oFeed == null)
                return "";

            if (data.Contains("{ROLES}"))
                data = data.Replace("{ROLES}", new DashboardController().GetcurrentUserRoleName(oFeed.PortalID, oFeed.UserID));
            if (data.Contains("{PHONE}"))
                data = data.Replace("{PHONE}", new DashboardController().GetResponseText(oFeed.PortalID, oFeed.UserID, "Phone"));
            if (data.Contains("{TITLE}"))
                data = data.Replace("{TITLE}", new DashboardController().GetResponseText(oFeed.PortalID, oFeed.UserID, "Title"));
            if (data.Contains("{ISCONNECTION}"))
                data = data.Replace("{ISCONNECTION}", new DashboardController().CheckUserIsFriendofCurrentuser(oFeed.UserID));
            if (data.Contains("{COMPANYNAME}"))
                data = data.Replace("{COMPANYNAME}", new DashboardController().GetResponseText(oFeed.PortalID, oFeed.UserID, "CompanyName", true));
            if (data.Contains("{UserImage}"))
                data = data.Replace("{UserImage}", oFeed.Profile.PhotoURL);
            return ReplaceTokens(data, new UserInfo(), oFeed);
            //  return ReplaceTokens(data, new UserProfile(), oFeed.Profile);
        }
        public static string ReplaceFeedToken(string data, HomeFeed oFeed)
        {
            if (oFeed == null)
                return "";

            if (oFeed.GetUSer != null)
                data = ReplaceUserInfoToken(data, oFeed.GetUSer);


            return ReplaceTokens(data, new HomeFeed(), oFeed);
        }
        public static string ReplaceFeedToken(string data, UserInfo oUserinfo)
        {
            if (oUserinfo == null)
                return "";
            return ReplaceTokens(data, new UserInfo(), oUserinfo);
        }
        private static string[] PropertiesFromType(object atype)
        {
            if (atype == null) return new string[] { };
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            List<string> propNames = new List<string>();
            foreach (PropertyInfo prp in props)
            {
                propNames.Add(prp.Name);
            }
            return propNames.ToArray();
        }
        private static string ReplaceTokens(string data, object atype, object oinfo)
        {

            if (string.IsNullOrEmpty(data))
                return "";
            if (atype == null) return "";
            Type t = atype.GetType();
            PropertyInfo[] props = t.GetProperties();
            List<string> propNames = new List<string>();

            foreach (PropertyInfo prp in props)
            {
                propNames.Add(prp.Name);



            }

            Type t1 = oinfo.GetType();
            PropertyInfo[] props1 = t1.GetProperties();

            foreach (string str in propNames)
            {


                foreach (PropertyInfo p1 in props1)
                {
                    string tempData = "{" + str.ToLower() + "format}";


                    if (p1.Name.ToString().ToLower() == str.ToLower())
                    {


                        string Value = Convert.ToString(p1.GetValue(oinfo, null));
                        if (data.Contains(tempData))
                        {
                            if (p1.PropertyType == typeof(Int32) || p1.PropertyType == typeof(int) || p1.PropertyType == typeof(Int64))
                            {
                                Value = DisplayCulutureNumeric(Convert.ToInt32(p1.GetValue(oinfo, null)));

                            }
                            else if (p1.PropertyType == typeof(decimal) || p1.PropertyType == typeof(float))
                            {
                                Value = DisplayCulutureNumeric(Convert.ToDecimal(p1.GetValue(oinfo, null)));

                            }
                            else if (p1.PropertyType == typeof(DateTime))
                            {

                                Value = DisplayCulutureDateTime(Convert.ToDateTime(p1.GetValue(oinfo, null)));

                            }

                            data = data.Replace(tempData, Value).Replace(tempData.ToUpper(), Value).Replace(tempData, Value);

                        }

                        data = data.Replace("{" + str.ToLower() + "}", Value).Replace("{" + str.ToUpper() + "}", Value).Replace("{" + str + "}", Value);
                    }

                }
            }
            return data;
        }

    }
}