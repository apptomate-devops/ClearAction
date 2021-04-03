using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ClearAction.Modules.UserAnalytics.Components;
namespace ClearAction.Modules.UserAnalytics
{
    /// <summary>
    /// Summary description for rh
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class rh : System.Web.Services.WebService
    {

        [WebMethod]
        public CA_AnalyticsInfo GeSolveSpaceAnalytics(int UserID)
        {
            FeatureController oCtrl = new FeatureController();
            return oCtrl.GetSolveSpaceAnalytics(UserID);
        }

        [WebMethod]
        public CA_AnalyticsInfo GeComponentAnalytics(int UserID,int ComponentTypeID)
        {
            FeatureController oCtrl = new FeatureController();
            return oCtrl.GetUserAnalyticalDataByComponent(UserID, ComponentTypeID);
        }

        [WebMethod]
        public CA_AnalyticsInfo GetUserActvityData(int UserID)
        {
            FeatureController oCtr = new FeatureController();
            CA_AnalyticsInfo rValue= oCtr.GetUserActvityData(UserID);
            rValue.Interactions += rValue.PostCreated;
            return rValue;
        }
    }
}
