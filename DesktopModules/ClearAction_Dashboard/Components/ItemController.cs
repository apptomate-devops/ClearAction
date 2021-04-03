/*
' Copyright (c) 2017 ClearAction
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
using System;
using System.Collections.Generic;
using DotNetNuke.Data;
using System.Linq;
using DotNetNuke.Common.Utilities;

namespace ClearAction.Modules.Dashboard.Components
{
    class DBController
    {
        //public void CreateItem(Item t)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        var rep = ctx.GetRepository<Item>();
        //        rep.Insert(t);
        //    }
        //}

        //public void DeleteItem(int itemId, int moduleId)
        //{
        //    var t = GetItem(itemId, moduleId);
        //    DeleteItem(t);
        //}

        //public void DeleteItem(Item t)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        var rep = ctx.GetRepository<Item>();
        //        rep.Delete(t);
        //    }
        //}

        public IQueryable<BlogPosts> GetItems(int moduleId)
        {
            IQueryable<BlogPosts> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<BlogPosts>();
                t = rep.Find("WHERE Published=@0", true).AsQueryable();//.Where(x => x.PublishedOnDate < System.DateTime.Now);
            }
            return t;
        }

        /// <summary>
        /// To get the list of user's blog with Assigned and View Status
        /// </summary>
        /// <param name="UserID">DNN ID for User</param>
        /// <param name="TopN">Top N records</param>
        /// <returns></returns>
        public List<CA_BlogPosts> GetItems(int UserID, int TopN)
        {
            List<CA_BlogPosts> lstUserBlogs = new List<CA_BlogPosts>();
            lstUserBlogs = CBO.FillCollection<CA_BlogPosts>(SqlDataProvider.Instance().ExecuteReader("CA_Blog_GetUserBlogs", UserID, TopN));
            return lstUserBlogs;
        }
        public List<CA_BlogPosts> GetItemsSearch(int UserID, int TopN,string search)
        {
            List<CA_BlogPosts> lstUserBlogs = new List<CA_BlogPosts>();
            lstUserBlogs = CBO.FillCollection<CA_BlogPosts>(SqlDataProvider.Instance().ExecuteReader("CA_Blog_GetUserBlogsSearch", UserID, TopN, search));
            return lstUserBlogs;
        }


        public List<CA_DigitalEvents> CA_DigitalEvents(int ComponentID,int UserID, int TopN)
        {
            List<CA_DigitalEvents> lstUserBlogs = new List<CA_DigitalEvents>();
            lstUserBlogs = CBO.FillCollection<CA_DigitalEvents>(SqlDataProvider.Instance().ExecuteReader("WCC_GetUserDigtalEvents", UserID, ComponentID, TopN));
            return lstUserBlogs;
        }

        public List<CA_DigitalEvents> CA_DigitalEventsSearch(int ComponentID, int UserID, int TopN,string search)
        {
            List<CA_DigitalEvents> lstUserBlogs = new List<CA_DigitalEvents>();
            lstUserBlogs = CBO.FillCollection<CA_DigitalEvents>(SqlDataProvider.Instance().ExecuteReader("WCC_GetUserDigtalEventsSearch", UserID, ComponentID, TopN,search));
            return lstUserBlogs;
        }
        private IQueryable<Topics> GetForumItems(int moduleId)
        {
            IQueryable<Topics> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Topics>();
                t = rep.Find("WHERE IsDeleted=@0 AND IsApproved=@1 AND IsRejected=@2 AND IsArchived=@3 AND IsLocked=@4", false, true, false, false, false).AsQueryable().Where(x => x.GetContent.DateCreated < System.DateTime.Now);
            }
            return t;
        }

        private List<CA_Topics> GetForumItems(int UserID, int TopN)
        {
            List<CA_Topics> lstUserForums = new List<CA_Topics>();

            lstUserForums = CBO.FillCollection<CA_Topics>(SqlDataProvider.Instance().ExecuteReader("CA_Forum_GetUserForums", UserID, TopN));
            return lstUserForums;
        }
        private List<CA_Topics> GetForumItemsSearch(int UserID, int TopN,int authorid)
        {
            List<CA_Topics> lstUserForums = new List<CA_Topics>();

            lstUserForums = CBO.FillCollection<CA_Topics>(SqlDataProvider.Instance().ExecuteReader("CA_Forum_GetUserForumsSearch", UserID, TopN,authorid));
            return lstUserForums;
        }
        public List<CA_BlogPosts> GetAllBlog(int UserID, int TopN)
        {
            return GetItems(UserID, TopN);//.ToList().OrderByDescending(x => x.PublishedOnDate).Take(TopN).ToList();
        }
        public List<CA_BlogPosts> GetAllBlogSearch(int UserID, int TopN,string search)
        {
            return GetItemsSearch(UserID, TopN, search);//.ToList().OrderByDescending(x => x.PublishedOnDate).Take(TopN).ToList();
        }
        public List<CA_DigitalEvents> GetDigitalEvent(int ComponentID,int UserID, int TopN)
        {
            return CA_DigitalEvents(ComponentID, UserID, TopN);//.ToList().OrderByDescending(x => x.PublishedOnDate).Take(TopN).ToList();
        }
        public List<CA_DigitalEvents> GetDigitalEventSearch(int ComponentID, int UserID, int TopN,string search)
        {
            return CA_DigitalEventsSearch(ComponentID, UserID, TopN,search);//.ToList().OrderByDescending(x => x.PublishedOnDate).Take(TopN).ToList();
        }

        public List<CA_Topics> GetForums(int UserID, int TopN)
        {
            return GetForumItems(UserID, TopN);//.OrderByDescending(x => x.GetContent).Take(count).ToList();
        }
        public List<CA_Topics> GetForumsSearch(int UserID, int TopN,int authorid)
        {
            return GetForumItemsSearch(UserID, TopN, authorid);//.OrderByDescending(x => x.GetContent).Take(count).ToList();
        }


        public List<UserRelationshipInfo> GetUserConnections(int UserID)
        {
            List<UserRelationshipInfo> t;

            //using (IDataContext ctx = DataContext.Instance())
            //{
            //    var rep = ctx.GetRepository<UserRelationshipInfo>();
            //    t = rep.Find("CA_GetConnectionsByUserID", UserID).AsQueryable();
            //}

            t = CBO.FillCollection<UserRelationshipInfo>(SqlDataProvider.Instance().ExecuteReader("CA_GetConnectionsByUserID", UserID));
            return t;
        }



        public IQueryable<UserFavoriteInfo> UserFavoriteInfo(int UserID)
        {
           

            IQueryable<UserFavoriteInfo> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<UserFavoriteInfo>();
                t = rep.Find("WHERE cUserId=@0 AND IsFav=@1", UserID,true).AsQueryable();


            }
            return t;
        }

        public List<DTOUser>  GetListOfUserIDSimilarByCompany(int UserID, int QuestionId)
        {

           
           return CBO.FillCollection<DTOUser>(SqlDataProvider.Instance().ExecuteReader("CA_GetConnectionByCompany", UserID, QuestionId));



        }
        public List<DTOResponse> GetQuestionResponseText(int UserID, int QuestionId)
        {


            return CBO.FillCollection<DTOResponse>(SqlDataProvider.Instance().ExecuteReader("CA_GetQuestionResponseByUserID", UserID, QuestionId));



        }
        public string GetCompanyName(int CompanyID)
        {


            string strResponse = string.Empty;
            IQueryable<string> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<string>();

                t = rep.Find("SELECT CompanyName FROM ClearAction_GlobalCompany Where  CompanyId=@0", CompanyID).AsQueryable();

            }
            if (t != null)
                return t.SingleOrDefault();
            return strResponse;



        }

    }
}
