using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearAction.Modules.WebCast.Components
{
    public partial class DBController
    {
        #region "Tags Related"

        public IQueryable<Tags> GetTags(bool IsAdmin)
        {
            IQueryable<Tags> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Tags>();
                t = rep.Get().AsQueryable();
            }
            return t.OrderBy(x => x.TagName);
        }
        
        /// <summary>
        /// added by SACHIN on 16-feb-2018
        /// </summary>
        /// <param name="TagID"></param>
        /// <returns></returns>
        public IQueryable<CA_TaxonomyTerms> GetTagList(int TagID)
        {
            IQueryable<CA_TaxonomyTerms> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                t = ctx.ExecuteQuery<CA_TaxonomyTerms>(System.Data.CommandType.StoredProcedure, "CA_GetTags", TagID).AsQueryable<CA_TaxonomyTerms>();
            }
            return t.OrderBy(x => x.Name);
        }
        public void ClearPostTags( int PostID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(System.Data.CommandType.StoredProcedure, "WCC_ClearPostTags", PostID);
            }
        }
        public Tags GetTagById(int TagID)
        {
            IQueryable<Tags> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Tags>();
                return rep.GetById(TagID);
            }
        }

        public void SavePostTag(Tags o)
        {
            var oTempTags = SearchForTags(o.TagName);

            if (oTempTags == null)
            {
                o = AddTag(o);
            }
            else
            {
                o.Items = oTempTags.Items + 1;
                o.TagId = oTempTags.TagId;
                UpdateTag(o);
            }
            int iTagid = o.TagId;

            var osearch = SearchForTagPost(iTagid, o.WCCID);

            if (osearch == null)
            {
                var oPostTags = new Posts_Tags()
                {
                    PostId = o.WCCID,
                    TagId = iTagid
                };

                AddPost_Tag(oPostTags);
            }
        }
        private Tags AddTag(Tags o)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<Tags>();
                    rep.Insert(o);
                }
            }
            catch (Exception exc)
            {
            }
            return o;
        }

        public Tags SearchForTags(string StrTagName)
        {
            IQueryable<Tags> t;
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<Tags>();

                    return rep.Find("Where TagName=@0", StrTagName).SingleOrDefault();

                }
            }
            catch (Exception exc)
            {


            }
            return null;


        }

        private Tags UpdateTag(Tags o)
        {
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<Tags>();

                    rep.Update(o);

                }


            }
            catch (Exception ex)
            {


            }
            return o;

        }
        #endregion

        #region "Tags Post"
        private IQueryable<string> GetTagNamePostID(int PostID)
        {
            
            try
            {
                IQueryable<string> t ;
                using (IDataContext ctx = DataContext.Instance())
                {
                  

                    t = ctx.ExecuteQuery<string>(System.Data.CommandType.StoredProcedure, "WCC_GetTagByPostID", PostID).AsQueryable();
                

                }

                return t;
            }
            catch (Exception ex)
            {


            }
            return null;

        }

        public string GetTagNameByPostID(int PostID)
        {
            IQueryable<string> t = GetTagNamePostID(PostID);
            try
            {


                string strTemp = string.Empty;

                foreach (string strname in t)
                    strTemp += strname + ",";


                return strTemp;


            }
            catch (Exception ex)
            {


            }
            return "";

        }

        private Posts_Tags SearchForTagPost(int TagId, int PostID)
        {
            Posts_Tags t=null;
            try
            {
              
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<Posts_Tags>();

                    t= rep.Find("Where PostId=@0 AND TagId=@1", TagId, PostID).SingleOrDefault();

                }


            }
            catch (Exception ex)
            {


            }
            return t;

        }
        private Posts_Tags AddPost_Tag(Posts_Tags o)
        {
            try
            {

               
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<Posts_Tags>();
                    ctx.ExecuteQuery<int>(System.Data.CommandType.StoredProcedure, "WCC_InsertPosttags", o.PostId, o.TagId);

                }


            }
            catch (Exception ex)
            {


            }
            return o;

        }

        private Posts_Tags UpdatePost_Tag(Posts_Tags o)
        {
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<Posts_Tags>();

                    rep.Update(o);

                }


            }
            catch (Exception ex)
            {


            }
            return o;

        }

        private void DeleteBy_Postid(int PostID)
        {
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<Posts_Tags>();

                    ctx.ExecuteQuery<int>(System.Data.CommandType.Text, "DELETE FROM WCC_Posts_Tags WHERE PostId=@0", PostID);



                }


            }
            catch (Exception ex)
            {


            }


        }
        #endregion


    }
}