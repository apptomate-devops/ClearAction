using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ClearAction.Modules.WebCast.Components
{
    public class Util
    {
        public static string PostKey = "WccPostKey";
        public static string CommunityCalls = "CommunityCall";

        public static string WebCastConversion = "WebCastConversion";
        public static string ModuleInstance = "ModuleInstance";
        public static string PageSizeKey = "PageSizeKey";
        public static string MAX_SELFASSIGNED_EVENT = "MAX_SELFASSIGNED_EVENT";

        public static int WebCastID = 5;
        public static int CommunityID = 4;

        public static string ReplaceToken_WccPost(string data, Components.WCC_PostInfo oPost)
        {
            if (oPost == null)
                return data;


            data = ReplaceTokens(data, new Components.WCC_PostInfo(), oPost);

            return data;
        }
        public static string ReplaceToken_Attach(string data, Components.AttachmentInfo attachmentInfo)
        {
            if (attachmentInfo == null)
                return data;


            data = ReplaceTokens(data, new Components.AttachmentInfo(), attachmentInfo);

            return data;
        }
        public static string GetCurrencySymbol(decimal currency)
        {
            return String.Format("{0:C}", Convert.ToString(currency));
        }


        public static string DisplayCulutureNumeric(decimal oType)
        {
            return oType != -1 ? string.Format(new CultureInfo("en-US"), "{0:c}", oType) : "";

        }
        public static string DisplayCulutureTime(DateTime dt)
        {
            return dt.ToString("h:mm tt");
        }

        public static string DisplayCulutureDateTime(DateTime dt)
        {

            return String.Format("{0:MM/dd/yyyy}", dt);
        }
        public static string DisplayCulutureDateTime(DateTime dt, bool IsCST)
        {
            //TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            //DateTime newDT = TimeZoneInfo.ConvertTimeFromUtc(dt, targetZone);

            return String.Format("{0:MM/dd/yyyy}", ConvertToETDate(dt));
        }
        public static DateTime ConvertToCSTDate(DateTime dt)
        {
            TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime newDT = TimeZoneInfo.ConvertTimeFromUtc(dt, targetZone);

            return newDT;
        }

        public static DateTime ConvertToETDate(DateTime dt)
        {
            TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime newDT = TimeZoneInfo.ConvertTimeFromUtc(dt, targetZone);

            return newDT;
        }

        public  static string DisplayCultureDateTime(DateTime dt, DateTime time)
        {
            return DisplayCulutureDateTime(dt) + ", " + DisplayCulutureTime(time);
        }

        public static string HumanFriendlyDate(DateTime newDate)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - newDate.Ticks);
            double delta = ts.TotalSeconds;
            if (delta <= 1)
            {
                return "second ago";
            }

            if (delta < 60)
            {
                return string.Format("{0} seconds", ts.Seconds);
            }

            if (delta < 120)
            {
                return string.Format("{0} mins", ts.Minutes);
            }

            if (delta < (45 * 60))
            {
                return string.Format("{0} mins", ts.Minutes);
            }

            if (delta < (90 * 60))
            {
                return string.Format("{0} Hour Ago", ts.Hours);
            }

            if (delta < (24 * 60 * 60))
            {
                return string.Format("{0} Hours Ago", ts.Hours);
            }

            if (delta < (48 * 60 * 60))
            {
                return string.Format("{0} day Ago", ts.Days);
            }

            if (delta < (72 * 60 * 60))
            {
                return string.Format("{0} days Ago", ts.Days);
            }

            if (delta < Convert.ToDouble(new TimeSpan(24 * 32, 0, 0).TotalSeconds))
            {
                return string.Format("{0} Month Ago", System.Math.Ceiling(Convert.ToDecimal(ts.Days / 30)));
            }

            if (delta < Convert.ToDouble(new TimeSpan(((24 * 30) * 11), 0, 0).TotalSeconds))
            {
                return string.Format("{0} Months Ago", Math.Ceiling(Convert.ToDecimal(ts.Days / 30)));
            }

            if (delta < Convert.ToDouble(new TimeSpan(((24 * 30) * 18), 0, 0).TotalSeconds))
            {
                return string.Format("1 Year Ago");
            }

            return string.Format("{0} Years", Math.Ceiling(Convert.ToDecimal(ts.Days / 365)));
        }

        public static string DisplayCulutureDate(DateTime dt)
        {
            return String.Format("{0:MMM d, yyyy}", dt);
        }
        public static string DisplayCulutureNumeric(int oType)
        {
            return oType != -1 ? string.Format(new CultureInfo("en-US"), "{0:c}", oType) : "";

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