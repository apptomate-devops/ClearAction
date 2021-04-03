using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System;
using ClearAction.Modules.SolveSpaces.Data;
using DotNetNuke.Common.Utilities;
using System.Data;

namespace ClearAction.Modules.SolveSpaces.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for SolveSpaces
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
    public class SolveSpaceController //: IPortable, ISearchable, IUpgradeable
    {

        public List<SolveSpaceInfo> ListSolveSpaces(int UserID,int CatID,string pOption,string SearchTerm,int LoggedInUser,int UptoNDays)
        {
            return CBO.FillCollection<SolveSpaceInfo>(DataProvider.Instance().ListSolveSpaces(UserID,CatID, pOption, SearchTerm,LoggedInUser,UptoNDays,false));
        }

        public List<SolveSpaceInfo> ListSolveSpaces(int UserID, int CatID, string pOption, string SearchTerm, int LoggedInUser, int UptoNDays,bool IncludeDeleted)
        {
            return CBO.FillCollection<SolveSpaceInfo>(DataProvider.Instance().ListSolveSpaces(UserID, CatID, pOption, SearchTerm, LoggedInUser, UptoNDays,true));
        }
        public List<SolveSpaceInfo> ListRecommendedSolveSpaces(int UserID,int TopN)
        {
            return CBO.FillCollection<SolveSpaceInfo>(DataProvider.Instance().ListRecommendedSolveSpaces(UserID,TopN));
        }
        public List<SolveSpaceInfo> ListRecommendedSolveSpacesSearch(int UserID, int TopN,string search)
        {
            return CBO.FillCollection<SolveSpaceInfo>(DataProvider.Instance().ListRecommendedSolveSpacesSearch(UserID, TopN,search));
        }
        public List<SolveSpaceInfo> ListSolveSpacesWithStatus(int UserID,int CatID)
        {
            return CBO.FillCollection<SolveSpaceInfo>(DataProvider.Instance().ListSolveSpacesWithStatus(UserID,CatID));
        }

        public List<SolveSpaceInfo> ListSolveSpacesWithStatus(int UserID, int CatID, string SearchKey, int PageIndex,int PageSize)
        {
            return CBO.FillCollection<SolveSpaceInfo>(DataProvider.Instance().ListSolveSpacesWithStatus(UserID, CatID,  SearchKey, PageIndex, PageSize));
        }

        public SolveSpaceInfo GetSolveSpacesByID(int SSID)
        {
            return CBO.FillObject<SolveSpaceInfo>(DataProvider.Instance().GetSolveSpaceByID(SSID));
        }

        public int CreateNewSolveSpace(string Title, string ShortDescription,  int DurationInMin, int TotalSteps, string TabLink,int TopicId) {
            return DataProvider.Instance().CreateNew(Title, ShortDescription, DurationInMin, TotalSteps, TabLink, TopicId);
        }

        public void Delete(int SolveSpaceID)
        {
            DataProvider.Instance().ClearCategories(SolveSpaceID);
            DataProvider.Instance().Delete(SolveSpaceID);
        }
        public void UnDelete(int SolveSpaceID)
        {
            DataProvider.Instance().UnDelete(SolveSpaceID);
        }

        public void Update(int ID,string Title, string ShortDescription, int DurationInMin, int TotalSteps, string TabLink,int TopicId)
        {
            DataProvider.Instance().Update(ID, Title, ShortDescription, DurationInMin, TotalSteps, TabLink,TopicId);
        }

        /// <summary>
        /// return the categories to which the solve-space belongs
        /// </summary>
        /// <param name="SolveSpaceID">ID of Solve Space</param>
        /// <returns></returns>
        public List<GlobalCategoryInfo> GetMyCategories(int SolveSpaceID)
        {
            return CBO.FillCollection<GlobalCategoryInfo>(DataProvider.Instance().ListSSCategories(SolveSpaceID));
        }

        public void Add2Category(int SolveSpaceID, int CategoryID)
        {
            DataProvider.Instance().Add2Category(SolveSpaceID, CategoryID);
        }
        public void ClearMyCategories(int SolveSpaceID)
        {
            DataProvider.Instance().ClearCategories(SolveSpaceID);
        }

        public int CreateSolveSpaceTopic(int TopicID, string Title, string Summary)
        {
            return DataProvider.Instance().SaveSolveSpaceTopic(TopicID, Title, Summary);
        }


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

        //List<ClearAction_SolveSpacesInfo> colClearAction_SolveSpacess = GetClearAction_SolveSpacess(ModuleID);
        //if (colClearAction_SolveSpacess.Count != 0)
        //{
        //    strXML += "<ClearAction_SolveSpacess>";

        //    foreach (ClearAction_SolveSpacesInfo objClearAction_SolveSpaces in colClearAction_SolveSpacess)
        //    {
        //        strXML += "<ClearAction_SolveSpaces>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objClearAction_SolveSpaces.Content) + "</content>";
        //        strXML += "</ClearAction_SolveSpaces>";
        //    }
        //    strXML += "</ClearAction_SolveSpacess>";
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
        //XmlNode xmlClearAction_SolveSpacess = DotNetNuke.Common.Globals.GetContent(Content, "ClearAction_SolveSpacess");
        //foreach (XmlNode xmlClearAction_SolveSpaces in xmlClearAction_SolveSpacess.SelectNodes("ClearAction_SolveSpaces"))
        //{
        //    ClearAction_SolveSpacesInfo objClearAction_SolveSpaces = new ClearAction_SolveSpacesInfo();
        //    objClearAction_SolveSpaces.ModuleId = ModuleID;
        //    objClearAction_SolveSpaces.Content = xmlClearAction_SolveSpaces.SelectSingleNode("content").InnerText;
        //    objClearAction_SolveSpaces.CreatedByUser = UserID;
        //    AddClearAction_SolveSpaces(objClearAction_SolveSpaces);
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

        //List<ClearAction_SolveSpacesInfo> colClearAction_SolveSpacess = GetClearAction_SolveSpacess(ModInfo.ModuleID);

        //foreach (ClearAction_SolveSpacesInfo objClearAction_SolveSpaces in colClearAction_SolveSpacess)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objClearAction_SolveSpaces.Content, objClearAction_SolveSpaces.CreatedByUser, objClearAction_SolveSpaces.CreatedDate, ModInfo.ModuleID, objClearAction_SolveSpaces.ItemId.ToString(), objClearAction_SolveSpaces.Content, "ItemId=" + objClearAction_SolveSpaces.ItemId.ToString());
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

        #region User Response related methods
        public List<SSUserResponseInfo> GetUserResponseOnSS(int UserId,int SSID,int StepID)
        {
            return CBO.FillCollection<SSUserResponseInfo>(DataProvider.Instance().GetUserResponses(UserId, SSID,StepID));
        }
        #endregion 

    }

    public class SolveSpaceInfo {
        public int SolveSpaceID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int CategoryID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int DurationInMin { get; set; }
        public int StepCount { get; set; }
        public int TotalSteps { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string TabLink { get; set; }
        public int TotalRecords { get; set; }
        public bool IsSelfAssigned { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsDeleted { get; set; }
        public List<GlobalCategoryInfo> MyCategories
        {
            get
            {
                SolveSpaceController oCtrl = new SolveSpaceController();
                return oCtrl.GetMyCategories(this.SolveSpaceID);
            }
        }
        public int ForumTopicID { get; set; }
    }

    public class SSUserResponseInfo
    {
        public int USERID { get; set; }
        public int SSID { get; set; }
        public string NAME { get; set; }
        public string VALUE { get; set; }
        public int STEPID { get; set; }
    }

    public class GlobalCategoryController
    {
        public List<GlobalCategoryInfo> ListGlobalCategory()
        {
            return CBO.FillCollection<GlobalCategoryInfo>(DataProvider.Instance().ListCategories());
        }

        /* added by kusum : 8-Dec-17*/
        public List<GlobalCategoryInfo> ListAllCategories(int PI, int PS)
        {
            return CBO.FillCollection<GlobalCategoryInfo>(DataProvider.Instance().ListAllCategories(PI, PS));
        }

        /* added by kusum : 8-Dec-17*/
        public void DeleteCategory(int CategoryID)
        {
            DataProvider.Instance().DeleteCategory(CategoryID);
        }

        /* added by kusum : 8-Dec-17*/
        public void UpdateCategory(int CategoryID, string CategoryName, bool isActive, int CompID)
        {
            DataProvider.Instance().UpdateCategory(CategoryID, CategoryName, isActive, CompID);
        }




    }

    public class GlobalCategoryInfo
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public bool IsActive { get; set; }
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public int TotalRecords { get; set; }
    }
}
