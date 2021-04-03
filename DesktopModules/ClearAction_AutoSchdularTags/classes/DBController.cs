using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using DotNetNuke.Services.Scheduling;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Data.PetaPoco;
using DotNetNuke.Data;

namespace ClearAction_AutoSchdularTags
{
    public class DBController
    {

        private string GetConnectionstring
        {
            get
            {
                return DotNetNuke.Common.Utilities.Config.GetConnectionString();
            }
        }



        public int AddToDatabase(List<GlobalCategoryRelation> lstCategory, int MasterComponentID, int iUser)
        {

            ///Now check the number of blog per category and which one need to assign to user.
            /// Sort basis of Category weight
            lstCategory = lstCategory.OrderBy(x => x.iBlogCount).ToList();

            List<int> blogItemList = new List<int>();
            int iTotalLimit = Helper.GetMaxRecord; //Limit can be changed from 2 to 20 based on configuration, Testing purpose : 2



            //Loop through as how many need to assign
            for (int icounter = 0; icounter < lstCategory.Count; icounter++)
            {


                //Make sure atleast one of the cateogry get selected without duplicate entry
                foreach (int iRecorid in lstCategory[icounter].BlogContentID)
                {

                    if (!blogItemList.Contains(iRecorid) && iTotalLimit >= blogItemList.Count)
                    {
                        blogItemList.Add(iRecorid);

                        break;
                    }
                }
            }



            //}
            //Add this information to Component table

            foreach (int iContentItemid in blogItemList)
            {
                PetaPocoHelper.ExecuteNonQuery(GetConnectionstring, System.Data.CommandType.StoredProcedure, "CA_AssignItemToUser", MasterComponentID, iContentItemid, iUser, iUser);
            }
            return blogItemList.Count > 0 ? blogItemList.Count : 0;
        }



        /// <summary>
        /// Get The Blogs based on user Tags
        /// </summary>
        /// <param name="iUser"></param>
        /// <returns></returns>
        public List<BlogInfo> GetUserBlogPerference(int iUser)
        {


            var idatareader = PetaPocoHelper.ExecuteReader(GetConnectionstring, System.Data.CommandType.Text, string.Format("SELECT ContentItemid FROM [dbo].ClearAction_GetTagsBlog({0}) ORDER BY ContentItemid DESC", iUser));
            var oBlogList = DotNetNuke.Common.Utilities.CBO.FillCollection<BlogInfo>(idatareader);

            return oBlogList;

        }

        /// <summary>
        /// Get The Forum based on user Tags
        /// </summary>
        /// <param name="iUser"></param>
        /// <returns></returns>
        public List<ForumInfo> GetUserForumPerference(int iUser)
        {


            var idatareader = PetaPocoHelper.ExecuteReader(GetConnectionstring, System.Data.CommandType.Text, string.Format("SELECT * FROM [dbo].ClearAction_GetTagsForum({0}) ORDER BY TopicId DESC", iUser));
            var oForumlist = DotNetNuke.Common.Utilities.CBO.FillCollection<ForumInfo>(idatareader);

            return oForumlist;

        }


        /// <summary>
        /// Get The Forum based on user Tags
        /// </summary>
        /// <param name="iUser"></param>
        /// <returns></returns>
        public List<GlobalCategory> GetProfileCategoryByUser(int iUser)
        {


            var idatareader = PetaPocoHelper.ExecuteReader(GetConnectionstring, System.Data.CommandType.StoredProcedure, "CA_GetProfileCategoryByUser", iUser);
            var oForumlist = DotNetNuke.Common.Utilities.CBO.FillCollection<GlobalCategory>(idatareader);

            return oForumlist;

        }


        /// <summary>
        /// Get The Forum based on user Tags
        /// </summary>
        /// <param name="iUser"></param>
        /// <returns></returns>
        public List<SolveSpaceInfo> GetSolveSpaceByUserProfile(int iUser)
        {


            var idatareader = PetaPocoHelper.ExecuteReader(GetConnectionstring, System.Data.CommandType.StoredProcedure, "CA_GetSolveSpaceByProfileUserID", iUser);
            var oForumlist = DotNetNuke.Common.Utilities.CBO.FillCollection<SolveSpaceInfo>(idatareader);

            return oForumlist;

        }


        //Get the associated categoyr with the blog
        public IQueryable<GlobalCategory> GetGlobalCategoryBlog(int BlogContentID)
        {
            IQueryable<GlobalCategory> t;
            using (IDataContext ctx = DataContext.Instance())
            {


                //  var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteQuery<GlobalCategory>(System.Data.CommandType.StoredProcedure, "CA_GetBlogCategoryRelationByBlogID", BlogContentID).AsQueryable();


            }
            return t.OrderBy(x => x.OptionOrder);


        }

        /// <summary>
        /// Get The associated category with Forum
        /// </summary>
        /// <param name="TopicID"></param>
        /// <returns></returns>
        public IQueryable<GlobalCategory> GetGlobalCategoryForum(int TopicID)
        {
            IQueryable<GlobalCategory> t;
            using (IDataContext ctx = DataContext.Instance())
            {


                //  var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteQuery<GlobalCategory>(System.Data.CommandType.StoredProcedure, "CA_GetForumCategoryRelationByTopicID", TopicID).AsQueryable();


            }
            return t.OrderBy(x => x.OptionOrder);


        }


        /// <summary>
        /// Get The associated category with Forum
        /// </summary>
        /// <param name="TopicID"></param>
        /// <returns></returns>
        public IQueryable<GlobalCategory> GetCategoryBySolveSpaceID(int SolveSpaceID)
        {
            IQueryable<GlobalCategory> t;
            using (IDataContext ctx = DataContext.Instance())
            {


                //  var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteQuery<GlobalCategory>(System.Data.CommandType.StoredProcedure, "CA_GetSolveSpaceCategoryByCategoryID", SolveSpaceID).AsQueryable();


            }
            return t.OrderBy(x => x.OptionOrder);


        }

        /// <summary>
        /// Get The associated category with Forum
        /// </summary>
        /// <param name="TopicID"></param>
        /// <returns></returns>
        public IQueryable<SolveSpaceInfo> GetSolveSpaceByUserProfilePerference(int iUser)
        {
            IQueryable<SolveSpaceInfo> t;
            using (IDataContext ctx = DataContext.Instance())
            {


                //  var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteQuery<SolveSpaceInfo>(System.Data.CommandType.StoredProcedure, "CA_GetSolveSpaceByProfileUserID", iUser).AsQueryable();


            }
            return t.OrderBy(x => x.SolveSpaceID);


        }



        //public List<EmailSchedular> GetUnSendEmail()
        //{
        //    IQueryable<EmailSchedular> t;
        //    using (IDataContext ctx = DataContext.Instance())
        //    {


        //      var rep = ctx.GetRepository<EmailSchedular>();
        //        t = rep.Find("Where IsSent=@0", false).AsQueryable();


        //    }
        //    return t.OrderBy(x => x.EmailSchedularId).ToList();

        //}
        //public void UpdateEmailSchedular(EmailSchedular oemail)
        //{
        //    using (IDataContext ctx = DataContext.Instance())
        //    {


        //        var rep = ctx.GetRepository<EmailSchedular>();
        //        rep.Update(oemail);


        //    }
        //}

    }
}