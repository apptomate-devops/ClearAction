using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetNuke.Modules.ActiveForums
{
    public class LikesController
    {
        public List<Likes> GetForPost(int postId)
        {
            List<Likes> likes = new List<Likes>();
            var odata = GetLikesByPost(postId);
            if (likes != null)
                likes = odata.Where(x => x.Checked == true).ToList();
            return likes;
        }
        private IQueryable<Likes> GetLikesByPost(int postid)
        {
            IQueryable<Likes> likes;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Likes>();
                likes = rep.Find("WHERE PostId = @0", postid).AsQueryable();
            }
            return likes;
        }
        public List<Likes> ContentDisLikes(int postId)
        {
            List<Likes> likes = new List<Likes>();
            var odata = GetLikesByPost(postId);
            if (likes != null)
                likes = odata.Where(x => x.Checked == false).ToList();
            return likes;


        }

        public string GetForPostCount(int postId, bool IsActive)
        {
            List<Likes> likes =IsActive? GetForPost(postId): ContentDisLikes(postId);

            return likes == null ? "0" : likes.Count.ToString();
        }


        public Boolean GetForPostCurrentUser(int contentId, int userId)
        {
            var like = GetForPost(contentId).Where(x => x.UserId == userId).SingleOrDefault();
            if (like == null)
                return false;
            return like.Checked;

        }


        public Likes CheckUserLikeOrDislikePost(int contentId, int userId)
        {
            var like = GetLikesByPost(contentId).Where(x => x.UserId == userId).SingleOrDefault();
            if (like == null)
                return null;
            return like;

        }
        public Boolean Like(int contentId, int userId)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Likes>();
                var like = rep.Find("Where PostId = @0 AND UserId = @1", contentId, userId).FirstOrDefault();

                if (like != null)
                {
                    if (like.Checked)
                        like.Checked = false;
                    else
                        like.Checked = true;
                    rep.Update(like);
                }
                else
                {
                    like = new Likes();
                    like.PostId = contentId;
                    like.UserId = userId;
                    like.Checked = true;
                    rep.Insert(like);
                }
                return like.Checked;
            }


        }
    }
}
