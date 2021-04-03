using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ClearAction.Modules.SolveSpaces.Components;
using System.Web.Configuration;
using DotNetNuke.Entities.Users;
using System.Web.Script.Serialization;


namespace ClearAction.Modules.SolveSpaces
{
    /// <summary>
    /// To manage the ajax call for solve spaces related methods
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class RequestHandler : System.Web.Services.WebService
    {

        /// <summary>
        /// To return the list of solve-spaces based on given Category-ID and Status( Done/In-Progress/To-Do/Whats-New/All)
        /// </summary>
        /// <param name="CatID">ID of Category</param>
        /// <param name="pOption">Status-DONE / In-Progress / To Do/ Whats New / All </param>
        /// <returns></returns>
        [WebMethod]
        public List<SolveSpaceInfo> ListSolveSpaces(int UserID,int CatID, string pOption, string SearchTerm,int LoggedInUser,int SortingOrder)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
          //  IQueryable<SolveSpaceInfo> t = default(IQueryable<SolveSpaceInfo>);
            // List<SolveSpaceInfo> lstSolveSpaces = oCtrl.ListSolveSpaces(UserID, CatID, pOption, SearchTerm, LoggedInUser,0);
            IQueryable<SolveSpaceInfo> t = oCtrl.ListSolveSpaces(UserID, CatID, pOption, SearchTerm, LoggedInUser, 0).AsQueryable<SolveSpaceInfo>();
            
            if (SortingOrder != -1)
            {
                switch (SortingOrder)
                {
                    case 3: // Recent to former
                        t = t.OrderByDescending(x => x.LastUpdatedOn);
                        break;
                    case 1: // Alpha a-z
                        t = t.OrderBy(x => x.Title);
                        break;
                    case 2: // Alpha z-a
                        t = t.OrderByDescending(x => x.Title);
                        break;
                    case 4: // Former to recent
                        t = t.OrderBy(x => x.LastUpdatedOn); break;
                    case 5: // Active today
                        t = t.Where(x => x.LastUpdatedOn.Date == System.DateTime.Now.Date);
                        break;
                    case 6: // Active last 7 days
                        t = t.Where(x => x.LastUpdatedOn.Date > System.DateTime.Now.AddDays(-7).Date).Where(x => x.LastUpdatedOn.Date <= System.DateTime.Now.Date);
                        break;
                    case 7: // Active this month
                        t = t.Where(x => x.LastUpdatedOn.Date > System.DateTime.Now.AddDays(-30).Date).Where(x => x.LastUpdatedOn.Date <= System.DateTime.Now.Date);
                        break;
                }
            }
            return t.ToList();
        }
        public void testmethod()
        {
            
        }

        [WebMethod]
        public List<SolveSpaceInfo> ListSolveSpacesEdit(int UserID, int CatID, string pOption, string SearchTerm, int LoggedInUser,int IncludeDeleted)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
            List<SolveSpaceInfo> lstSolveSpaces = oCtrl.ListSolveSpaces(UserID, CatID, pOption, SearchTerm, LoggedInUser, 0, true);
            return lstSolveSpaces;
        }
        /// <summary>
        /// Returns Top 5 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [WebMethod]
        public List<SolveSpaceInfo> ListRSolveSpaces(int UserID)
        {
            testmethod();
            int MyExchange_SS_TopN = 5;
            try{
                string strMyExchange_SS_TopN = WebConfigurationManager.AppSettings["MyExchange_TopN_SS"];
                MyExchange_SS_TopN = int.Parse(strMyExchange_SS_TopN);
            }
            catch(Exception ex){
                //do nothing
            }
            
            SolveSpaceController oCtrl = new SolveSpaceController();
            List<SolveSpaceInfo> lstSolveSpaces = oCtrl.ListRecommendedSolveSpaces(UserID, MyExchange_SS_TopN);
            return lstSolveSpaces;
        }

        [WebMethod]
        public List<SolveSpaceInfo> ListRSolveSpacesSearch(int UserID,string search)
        {
            testmethod();
            int MyExchange_SS_TopN = 1000;
         
            SolveSpaceController oCtrl = new SolveSpaceController();
            List<SolveSpaceInfo> lstSolveSpaces = oCtrl.ListRecommendedSolveSpacesSearch(UserID, MyExchange_SS_TopN,search);
            return lstSolveSpaces;
        }


        /// <summary>
        /// To return the list of all globall categories
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<GlobalCategoryInfo> ListCategories()
        {
            GlobalCategoryController oCtlr = new GlobalCategoryController();
            List<GlobalCategoryInfo> lstCategories = oCtlr.ListGlobalCategory();
            return lstCategories;
        }

       [WebMethod]
       public int CreateNewSolveSpace(string Title, string ShortDescription, string CatIDs, int DurationInMin, int TotalSteps, string TabLink)
       {
           SolveSpaceController oCtrl = new SolveSpaceController();
            
           try
           {
                // Create related forum topic
                int TopicID = CreateRelatedTopic(-1,Title, ShortDescription);

               // Create Solve Space information first
               int SolveSpaceID = oCtrl.CreateNewSolveSpace(Title, ShortDescription, DurationInMin, TotalSteps, TabLink, TopicID);
               string[] stringSeparators = new string[] { "|" };
               // Split the Categories on Pipe delimeter
               string[] aryCatIDs = CatIDs.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
               // Now Add Solve Space to different categories
               foreach (string sCatID in aryCatIDs)
               {
                   int CatID = int.Parse(sCatID);
                   oCtrl.Add2Category(SolveSpaceID, CatID);
               }
               return SolveSpaceID;
           }
           catch (Exception ex)
           {
               return -1;
           }
       }
        private int CreateRelatedTopic(int TopicID,string Title, string ShortDescription)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
           return  oCtrl.CreateSolveSpaceTopic(TopicID, Title, ShortDescription);
        }
        [WebMethod]
       public void RemoveSolveSpace(int ID)
       {
           SolveSpaceController oCtrl = new SolveSpaceController();
           oCtrl.Delete(ID);
       }
        [WebMethod]
        public void UnDeleteSolveSpace(int ID)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
            oCtrl.UnDelete(ID);
        }
        [WebMethod]
        public void UpdateSolveSpace(int ID, string Title, string ShortDescription, string CatIDs, int Duration, int Steps, string TabLink)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
            SolveSpaceInfo oSSInfo = oCtrl.GetSolveSpacesByID(ID);
            int TopicID = -1;
            if (oSSInfo != null)
            {  // Create related forum topic
                if (oSSInfo.ForumTopicID >0)
                {
                    TopicID = CreateRelatedTopic(oSSInfo.ForumTopicID, Title, ShortDescription);
                }
              else
                {
                    TopicID = CreateRelatedTopic(-1, Title, ShortDescription);
                }
               
            }
            
            //oCtrl.ge
            string[] stringSeparators = new string[] { "|" };
            // split the Categories on Pipe delimeter
            string[] aryCatIDs = CatIDs.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            // first clear the previous entries
            oCtrl.ClearMyCategories(ID);
            // Now Add new user selection for categories
            foreach (string sCatID in aryCatIDs)
            {
                int CatID = int.Parse(sCatID);
                oCtrl.Add2Category(ID, CatID);
            }
            // Update solve space master information
            oCtrl.Update(ID, Title, ShortDescription, Duration, Steps, TabLink,TopicID);
        }

        [WebMethod]
        public List<GlobalCategoryInfo> ListSSCategories(int SolveSpaceID)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
            return oCtrl.GetMyCategories(SolveSpaceID);
        }

        [WebMethod]
        public UserInfo GetUserInfo(int UserID)
        {
            UserController oCtrl = new UserController();
            return oCtrl.GetUser(0, UserID);
        }

        [WebMethod]
        public string GetSSUserInputs(int UserID,int SSID,int StepID)
        {
            SolveSpaceController oCtrl = new SolveSpaceController();
            return new JavaScriptSerializer().Serialize(oCtrl.GetUserResponseOnSS(UserID, SSID, StepID));
        }
    }
}
