using ClearAction.Modules.ComponentManager.Components;
using System;
using System.Collections.Generic;
using System.Web.Services;
using ClearAction.Modules.SolveSpaces.Components;
using DotNetNuke.Entities.Users;
using System.Collections;
namespace ClearAction.Modules.ComponentManager
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
        /// <summary>
        /// to assign forum/blog or solve space to a user
        /// </summary>
        /// <param name="CID">Component ID: Forum-1,Blog-2,Solve-Space-3,4-CC,5-WCC</param>
        /// <param name="IID">Item ID: ForumTopicId, BlogID, SolveSpaceID,CCID,WCCID</param>
        /// <param name="UID">DNN UserID</param>
        [WebMethod]
        public void AssignItemToUser(int CID, string csvItemID, int UID, string csvUnchecked)
        {
            string[] stringSeparators = new string[] { "|" };

            ComponentController oCtrl = new ComponentController();
            // First Remove all unchecked one and then assign new one
            string[] unCheckIDs = csvUnchecked.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strID in unCheckIDs)
            {
                int ID = int.Parse(strID);
                oCtrl.DeAssign(CID, UID, ID);
            }
            //oCtrl.DeAssign(CID, UID);

            // Assign new selected one
            string[] IDs = csvItemID.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string strID in IDs)
            {
                int ID = int.Parse(strID);
                oCtrl.AssignToUser(CID, ID, UID);
            }
        }

        [WebMethod]
        public void RemoveFromMyVault(int CID, int UID, int IID)
        {
            ComponentController oCtrl = new ComponentController();
            oCtrl.RemoveFromMyVault(CID, UID, IID);
        }

        /// <summary>
        /// To assign forum/blog or solve space to a user
        /// </summary>
        /// <param name="CID">Component ID: Forum-1, Blog-2, Solve-Space-3, Community Call-4, WebCast-5 </param>
        /// <param name="IID">Item ID: ForumTopicId, BlogID, SolveSpaceID, CommunityCallID, WebCastID</param>
        /// <param name="UID">DNN UserID</param>
        [WebMethod]
        public void Assign2Me(int CID, int ItemID, int UID)
        {
            ComponentController oCtrl = new ComponentController();
            oCtrl.AssignToUser(CID, ItemID, UID, UID);
        }

        /// <summary>
        /// Return the list of solve spaces with the status if user is assigned solve-space Yes (1) or No (0)
        /// </summary>
        /// <param name="UserId">DNN UserID</param>
        /// <returns></returns>
        [WebMethod]
        public List<SolveSpaceInfo> ListSolveSpacesWithStatus(int UserId, int CatID)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
            List<SolveSpaceInfo> lstSolveSpace = oCtrl.ListSolveSpacesWithStatus(UserId, CatID);
            return lstSolveSpace;
        }

        /// <summary>
        /// Return the list of solve spaces with the status if user is assigned solve-space Yes (1) or No (0)
        /// </summary>
        /// <param name="UserId">DNN UserID</param>
        /// <returns></returns>
        [WebMethod]
        public List<SolveSpaceInfo> ListSolveSpacesWithStatus(int UserId, int CatID, string SearchKey, int PI, int PS)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
            List<SolveSpaceInfo> lstSolveSpace = oCtrl.ListSolveSpacesWithStatus(UserId, CatID, SearchKey, PI, PS);
            return lstSolveSpace;
        }


        [WebMethod]
        public List<CA_UserInfo> ListUsers()
        {
            DotNetNuke.Entities.Users.UserController oCtrl = new DotNetNuke.Entities.Users.UserController();
            List<CA_UserInfo> lstUsers = new List<CA_UserInfo>();

            ArrayList lstDNNUsers = DotNetNuke.Entities.Users.UserController.GetUsers(0);
            foreach (UserInfo oUser in lstDNNUsers)
            {
                CA_UserInfo oItem = new CA_UserInfo();
                oItem.UserName = oUser.Username;
                oItem.FirstName = oUser.FirstName;
                oItem.LastName = oUser.LastName;
                oItem.UserID = oUser.UserID;
                lstUsers.Add(oItem);
            }
            return lstUsers;
        }

        [WebMethod]
        public List<CA_TopicsInfo> ListForumsWithStatus(int UserID, int CatID, string SearchKey, int PI, int PS)
        {
            ComponentController OCtrl = new ComponentController();
            return OCtrl.ListForumsWithStatus(UserID, CatID, SearchKey, PI, PS);
        }

        [WebMethod]
        public List<CA_BlogPostInfo> ListBlogsWithStatus(int UserID, int CatID, string SearchKey, int PI, int PS)
        {
            ComponentController oCtrl = new ComponentController();
            return oCtrl.ListBlogsWithStatus(UserID, CatID, SearchKey, PI, PS);
        }
        [WebMethod]
        public void SaveUserVisit(int UserID, int ItemID, int ComponentTypeID)
        {
            ComponentController oCtrl = new ComponentController();
            oCtrl.SaveUserVisit(UserID, ItemID, ComponentTypeID);
        }


        /// <summary>
        /// Return the list of global categories
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<GlobalCategoryInfo> ListAllCategories(int PI, int PS)
        {
            GlobalCategoryController oCtrl = new GlobalCategoryController();
            List<GlobalCategoryInfo> lstResult = oCtrl.ListAllCategories(PI, PS);
            return lstResult;
        }


        /// <summary>
        /// Deletes Global Gategory
        /// </summary>
        /// <param name="CID">Category ID</param>
        [WebMethod]
        public void DeleteCategory(int CID)
        {
            GlobalCategoryController oCtrl = new GlobalCategoryController();
            oCtrl.DeleteCategory(CID);
        }



        /// <summary>
        /// Add/Updates Global Category
        /// </summary>
        /// <param name="CID">Category ID</param>
        /// <param name="name">Category name</param>
        /// <param name="isActive">IsActive parameter</param>
        /// <param name="CompID">Component ID</param>
        [WebMethod]
        public void UpdateCategory(int CID, string name, bool isActive, int CompID)
        {
            GlobalCategoryController oCtrl = new GlobalCategoryController();
            oCtrl.UpdateCategory(CID, name, isActive, CompID);
        }


        /// <summary>
        /// Return the list of global categories
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<CA_ComponentInfo> ListComponentMaster()
        {
            ComponentController oCtrl = new ComponentController();
            List<CA_ComponentInfo> lstResult = oCtrl.ListComponentMaster();
            return lstResult;
        }



        #region "Taxonomy Terms related Methods"


        /// <summary>
        /// Return the list of taxonomy terms
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<CA_TaxonomyTermInfo> ListTerms(int PI, int PS)
        {
            ComponentController oCtrl = new ComponentController();
            List<CA_TaxonomyTermInfo> lstResult = oCtrl.ListTaxonomyTerms(PI, PS);
            return lstResult;
        }

        /// <summary>
        /// Delete specific Taxonomy term
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public void DeleteTerm(int TermID)
        {
            ComponentController oCtrl = new ComponentController();
            oCtrl.DeleteTaxonomyTerm(TermID);
        }


        /// <summary>
        /// Add New Taxonomy term
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public void AddTerm(string PTerm, string Term)
        {
            ComponentController oCtrl = new ComponentController();
            oCtrl.AddTaxonomyTerm(PTerm, Term);
        }


        /// <summary>
        /// Return the list of all taxonomy terms
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<CA_TaxonomyTermInfo> ListAllTerms()
        {
            ComponentController oCtrl = new ComponentController();
            List<CA_TaxonomyTermInfo> lstResult = oCtrl.ListAllTaxonomyTerms();
            return lstResult;
        }


        /// <summary>
        /// Add New Related Tags
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public void AddRelatedTags(int RelatedTagID, int TermID, int UserID, string Tags)
        {
            ComponentController oCtrl = new ComponentController();
            // oCtrl.AddRelatedTags(RelatedTagID, TermID, UserID, Tags);
        }


        /// <summary>
        /// Get Related Tags
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<CA_RelatedTagsInfo> GetRelatedTags(int TermID)
        {
            ComponentController oCtrl = new ComponentController();
            //List<CA_RelatedTagsInfo> lstResult = oCtrl.GetRelatedTags(TermID);
            //return lstResult;

            return new List<CA_RelatedTagsInfo>();
        }

        /// <summary>
        /// Get Related Tags List
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<CA_RelatedTagsInfo> GetRelatedTagList(string tag)
        {
            ComponentController oCtrl = new ComponentController();
            List<CA_RelatedTagsInfo> lstResult = oCtrl.GetRelatedTagList(tag);
            return lstResult;
        }



        #endregion


        #region "Company Related Methods"

        [WebMethod]
        public List<CA_GlobalComp> ListCompany(int PI, int PS)
        {
           
            var lstResult = new Components.CompanyController().GetListofGlobalCompany(PI, PS);
            

            return lstResult;
        }

        [WebMethod]
        public int DeleteCompany(int CompanyID)
        {
            var oCtrl = new CompanyController();

            return oCtrl.DeleteCompany(CompanyID);
        }


        [WebMethod]
        public object GetCompanybyID(int CompanyID)
        {
            var oCtrl = new CompanyController();
            return oCtrl.GetCompanyByID(CompanyID);
        }


        [WebMethod]
        public CA_GlobalCompany CompanyValidate(string SearchCompany)
        {
            var oCtrl = new CompanyController();
            return oCtrl.SearchCompany(SearchCompany);
        }

        [WebMethod]
        public string AddCompany(int CompanyID, string CompanyName, bool IsChecked)
        {
            var oCompany = new CA_GlobalCompany()
            {
                CompanyId = CompanyID,
                CompanyName = CompanyName,
                IsActive = IsChecked
            };

            var oData = CompanyValidate(CompanyName);
            if (oData != null && (oData.CompanyId != CompanyID))
            {
                return "fail";
            }

            var oCtrl = new CompanyController();
            if (oCompany.CompanyId > 0)
                oCtrl.UpdateCompany(oCompany);
            else
                oCtrl.AddCompany(oCompany);
            return "success";
        }
        #endregion
    }
}


