using DotNetNuke.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearAction_AutoSchdularTags
{

    public static class Helper
    {

        public static int GetMaxRecord
        {
            get
            {
                int iReturnValue = 20;
                var oConfigEntry = System.Configuration.ConfigurationManager.AppSettings["SchedularCount"].ToString();
                try
                {
                    int.TryParse(Convert.ToString(oConfigEntry),out iReturnValue);
                }
                catch(Exception ex)
                { }

                return iReturnValue;
            }
        }
    }
}