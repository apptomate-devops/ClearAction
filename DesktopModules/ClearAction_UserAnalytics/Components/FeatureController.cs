using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using ClearAction.Modules.UserAnalytics.Data;
using DotNetNuke.Common.Utilities;

namespace ClearAction.Modules.UserAnalytics.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for ClearAction_UserAnalytics
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<ClearAction_UserAnalyticsInfo> colClearAction_UserAnalyticss = GetClearAction_UserAnalyticss(ModuleID);
        //if (colClearAction_UserAnalyticss.Count != 0)
        //{
        //    strXML += "<ClearAction_UserAnalyticss>";

        //    foreach (ClearAction_UserAnalyticsInfo objClearAction_UserAnalytics in colClearAction_UserAnalyticss)
        //    {
        //        strXML += "<ClearAction_UserAnalytics>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objClearAction_UserAnalytics.Content) + "</content>";
        //        strXML += "</ClearAction_UserAnalytics>";
        //    }
        //    strXML += "</ClearAction_UserAnalyticss>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlClearAction_UserAnalyticss = DotNetNuke.Common.Globals.GetContent(Content, "ClearAction_UserAnalyticss");
        //foreach (XmlNode xmlClearAction_UserAnalytics in xmlClearAction_UserAnalyticss.SelectNodes("ClearAction_UserAnalytics"))
        //{
        //    ClearAction_UserAnalyticsInfo objClearAction_UserAnalytics = new ClearAction_UserAnalyticsInfo();
        //    objClearAction_UserAnalytics.ModuleId = ModuleID;
        //    objClearAction_UserAnalytics.Content = xmlClearAction_UserAnalytics.SelectSingleNode("content").InnerText;
        //    objClearAction_UserAnalytics.CreatedByUser = UserID;
        //    AddClearAction_UserAnalytics(objClearAction_UserAnalytics);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<ClearAction_UserAnalyticsInfo> colClearAction_UserAnalyticss = GetClearAction_UserAnalyticss(ModInfo.ModuleID);

        //foreach (ClearAction_UserAnalyticsInfo objClearAction_UserAnalytics in colClearAction_UserAnalyticss)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objClearAction_UserAnalytics.Content, objClearAction_UserAnalytics.CreatedByUser, objClearAction_UserAnalytics.CreatedDate, ModInfo.ModuleID, objClearAction_UserAnalytics.ItemId.ToString(), objClearAction_UserAnalytics.Content, "ItemId=" + objClearAction_UserAnalytics.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

        public CA_AnalyticsInfo GetSolveSpaceAnalytics(int UserID)
        {
            return CBO.FillObject<CA_AnalyticsInfo>(DataProvider.Instance().GetSolveSpaceAnalytics(UserID));
        }


        public CA_AnalyticsInfo GetUserAnalyticalDataByComponent(int UserID, int ComponentTypeID)
        {
            return CBO.FillObject<CA_AnalyticsInfo>(DataProvider.Instance().GetUserAnalyticalData(UserID, ComponentTypeID));
        }

        public CA_AnalyticsInfo GetUserActvityData(int UserID)
        {
            return CBO.FillObject<CA_AnalyticsInfo>(DataProvider.Instance().GetUserActvityData(UserID));
        }
    }

    public class CA_AnalyticsInfo
    {
        public int Total { get; set; }
        public int Completed { get; set; }
        public int Interactions { get; set; }
        public int BlogComments { get; set; }
        public int CCJoined { get; set; }
        public int WCJoined { get; set; }
        public int PostCreated { get; set; }
    }

}
