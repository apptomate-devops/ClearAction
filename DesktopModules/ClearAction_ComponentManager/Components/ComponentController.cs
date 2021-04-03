using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using ClearAction.Modules.ComponentManager.Data;
using DotNetNuke.Common.Utilities;

using System.Linq;
namespace ClearAction.Modules.ComponentManager.Components
{
    //uncomment the interfaces to add the support.
    public class ComponentController //: IPortable, ISearchable, IUpgradeable
    {

        public void AssignToUser(int ComponentID, int ItemID, int UserID)
        {
            DataProvider.Instance().AssignItemToUser(ComponentID, ItemID, UserID);
        }

        /// <summary>
        /// to assign forum/blog or solve space to a user
        /// </summary>
        /// <param name="ComponentID">Component ID: Forum-1,Blog-2,Solve-Space-3</param>
        /// <param name="ItemID">Item ID: ForumTopicId, BlogID, SolveSpaceID</param>
        /// <param name="UserID">Assigned To DNN UserID</param>
        /// <param name="AssignedBy">Assigned by DNN UserID</param>
        public void AssignToUser(int ComponentID, int ItemID, int UserID, int AssignedBy)
        {
            DataProvider.Instance().AssignItemToUser(ComponentID, ItemID, UserID, AssignedBy);
        }

        public void DeAssign(int ComponentID, int UserID, int ItemID)
        {
            DataProvider.Instance().DeAssignComponent(ComponentID, UserID, ItemID);
        }

        public void RemoveFromMyVault(int ComponentID, int UserID, int ItemID)
        {
            DataProvider.Instance().RemoveFromMyVault(ComponentID, UserID, ItemID);
        }
        public List<CA_BlogPostInfo> ListBlogsWithStatus(int UserID, int CatID, string SearchKey, int PI, int PS)
        {
            return CBO.FillCollection<CA_BlogPostInfo>(DataProvider.Instance().ListBlogsWithStatus(UserID, CatID, SearchKey, PI, PS));
        }

        public List<CA_TopicsInfo> ListForumsWithStatus(int UserID, int CatID, string SearchKey, int PI, int PS)
        {
            return CBO.FillCollection<CA_TopicsInfo>(DataProvider.Instance().ListForumsWithStatus(UserID, CatID, SearchKey, PI, PS));
        }

        public List<CA_ComponentInfo> ListComponentMaster()
        {
            return CBO.FillCollection<CA_ComponentInfo>(DataProvider.Instance().ListComponentMaster());
        }

        /// <summary>
        /// DNN User ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ItemID">ID of Forum/Blog</param>
        /// <param name="ComponentTypeID">For forum-1 , blog-2</param>
        public void SaveUserVisit(int UserID, int ItemID, int ComponentTypeID)
        {
            DataProvider.Instance().SaveUserVisit(UserID, ItemID, ComponentTypeID);
        }

        #region "Taxonomy terms methods"

        public List<CA_TaxonomyTermInfo> ListTaxonomyTerms(int PI, int PS)
        {
            return CBO.FillCollection<CA_TaxonomyTermInfo>(DataProvider.Instance().ListTaxonomyTerms(PI, PS));
        }

        public void DeleteTaxonomyTerm(int TermID)
        {
            DataProvider.Instance().DeleteTaxonomyTerm(TermID);
        }

        public void AddTaxonomyTerm(string ParentTerm, string Term)
        {
            DataProvider.Instance().AddTaxonomyTerm(ParentTerm, Term);
        }

        public List<CA_TaxonomyTermInfo> ListAllTaxonomyTerms()
        {
            return CBO.FillCollection<CA_TaxonomyTermInfo>(DataProvider.Instance().ListAllTaxonomyTerms());
        }

        public void AddRelatedTags(int RelatedTagID, int TermID, int UserID, string Tags)
        {
            DataProvider.Instance().AddRelatedTags(RelatedTagID, TermID, UserID, Tags);
        }

        public List<CA_RelatedTagsInfo> GetRelatedTags(int TermID)
        {
            return CBO.FillCollection<CA_RelatedTagsInfo>(DataProvider.Instance().GetRelatedTags(TermID));
        }
        public List<CA_RelatedTagsInfo> GetRelatedTagList(string tag)
        {
            return CBO.FillCollection<CA_RelatedTagsInfo>(DataProvider.Instance().GetRelatedTagList(tag));
        }

        #endregion


        



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

        //List<ClearAction_ComponentManagerInfo> colClearAction_ComponentManagers = GetClearAction_ComponentManagers(ModuleID);
        //if (colClearAction_ComponentManagers.Count != 0)
        //{
        //    strXML += "<ClearAction_ComponentManagers>";

        //    foreach (ClearAction_ComponentManagerInfo objClearAction_ComponentManager in colClearAction_ComponentManagers)
        //    {
        //        strXML += "<ClearAction_ComponentManager>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objClearAction_ComponentManager.Content) + "</content>";
        //        strXML += "</ClearAction_ComponentManager>";
        //    }
        //    strXML += "</ClearAction_ComponentManagers>";
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
        //XmlNode xmlClearAction_ComponentManagers = DotNetNuke.Common.Globals.GetContent(Content, "ClearAction_ComponentManagers");
        //foreach (XmlNode xmlClearAction_ComponentManager in xmlClearAction_ComponentManagers.SelectNodes("ClearAction_ComponentManager"))
        //{
        //    ClearAction_ComponentManagerInfo objClearAction_ComponentManager = new ClearAction_ComponentManagerInfo();
        //    objClearAction_ComponentManager.ModuleId = ModuleID;
        //    objClearAction_ComponentManager.Content = xmlClearAction_ComponentManager.SelectSingleNode("content").InnerText;
        //    objClearAction_ComponentManager.CreatedByUser = UserID;
        //    AddClearAction_ComponentManager(objClearAction_ComponentManager);
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

        //List<ClearAction_ComponentManagerInfo> colClearAction_ComponentManagers = GetClearAction_ComponentManagers(ModInfo.ModuleID);

        //foreach (ClearAction_ComponentManagerInfo objClearAction_ComponentManager in colClearAction_ComponentManagers)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objClearAction_ComponentManager.Content, objClearAction_ComponentManager.CreatedByUser, objClearAction_ComponentManager.CreatedDate, ModInfo.ModuleID, objClearAction_ComponentManager.ItemId.ToString(), objClearAction_ComponentManager.Content, "ItemId=" + objClearAction_ComponentManager.ItemId.ToString());
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

    }
    public class CA_UserInfo
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

    public class CA_BlogPostInfo
    {
        public string Title { get; set; }

        public int ContentItemId { get; set; }

        public int CategoryID { get; set; }

        public bool IsAssigned { get; set; }
        public int TotalRecords { get; set; }
    }

    public class CA_TopicsInfo
    {
        public string Title { get; set; }

        public int ContentId { get; set; }

        public int CategoryID { get; set; }

        public bool IsAssigned { get; set; }
        public int TotalRecords { get; set; }
    }

    public class CA_ComponentInfo
    {
        public int ComponentID { get; set; }

        public string ComponentName { get; set; }
    }


    public class CA_TaxonomyTermInfo
    {
        public int TermID { get; set; }

        public int VocabularyID { get; set; }

        public int ParentTermID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public int Weight { get; set; }

        public int TermLeft { get; set; }

        public int TermRight { get; set; }

        public string ParentName { get; set; }

        public int TotalRecords { get; set; }

    }

    public class CA_RelatedTagsInfo
    {
        public string Tags { get; set; }
        public string Name { get; set; }
        public int SubTagID { get; set; }
        public int ParentTagID { get; set; }
        public int RelatedGlobalCategoryID { get; set; }

        public string NTerm { get; set; }

        public int TermID { get; set; }
    }



    public class CA_GlobalComp
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
        public bool IsActive { get; set; }

       
        public int TotalRecords { get; set; }


    }

}
